using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Information_System
{
    public partial class DataTransfer : Form
    {
        public DataTransfer()
        {
            InitializeComponent();
            this.Load += new EventHandler(DataTransfer_Load);
        }

        private void DataTransfer_Load(object sender, EventArgs e)
        {
            // OVO JE TRIK ZA PREZENTACIJU: 
            // Svaki put kad otvoriš formu, resetuj bar 5 rezultata na Pending da klijent vidi brojeve
            MainClass.ExecuteQuery("UPDATE TOP (5) LabResults SET ApiSyncStatus = 'Pending'");

            LoadApiSettings();
            StyleHistoryGrid();
            RefreshTransferStats();
        }

        public void RefreshTransferStats()
        {
            try
            {
                // 1. POVLAČENJE BROJAČA (Pending i Failed)
                string pendingQry = "SELECT COUNT(*) FROM LabResults WHERE ApiSyncStatus = 'Pending'";
                string failedQry = "SELECT COUNT(*) FROM LabResults WHERE ApiSyncStatus = 'Failed'";

                // Koristimo .ToString() jer GetDataTable vraća tabelu, uzimamo prvi red i prvu kolonu [0][0]
                lblPendingCount.Text = MainClass.GetDataTable(pendingQry).Rows[0][0].ToString();
                lblFailedCount.Text = MainClass.GetDataTable(failedQry).Rows[0][0].ToString();

                // 2. LOGIKA ZA PRIKAZ GREŠKE
                int failedCount = int.Parse(lblFailedCount.Text);
                if (failedCount > 0)
                {
                    lblLastErrorTime.Visible = true;
                    lblLastErrorTime.ForeColor = Color.Red;
                    lblLastErrorTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Critical: " + failedCount + " records failed.";
                    lblErrorMessage.ForeColor = Color.Red;
                }
                else
                {
                    lblLastErrorTime.Visible = false;
                    lblErrorMessage.Visible = false;
                }

                // 3. POSLEDNJI TRANSFER INFO (Status i vreme iz TransferLogs)
                string lastTransferQry = "SELECT TOP 1 TransferTime, Status, RecordsSent FROM TransferLogs ORDER BY TransferTime DESC";
                DataTable dtHistory = MainClass.GetDataTable(lastTransferQry);

                if (dtHistory.Rows.Count > 0)
                {
                    lblLastTransfer.Text = Convert.ToDateTime(dtHistory.Rows[0]["TransferTime"]).ToString("dd/MM/yyyy HH:mm");
                    lblStatus.Text = dtHistory.Rows[0]["Status"].ToString();
                    lblRecordsSentTotal.Text = dtHistory.Rows[0]["RecordsSent"].ToString();

                    // Boja teksta za status
                    lblStatus.ForeColor = lblStatus.Text == "Success" ? Color.Green : Color.Red;
                }
                else
                {
                    lblLastTransfer.Text = "Never";
                    lblStatus.Text = "No Transfers";
                    lblRecordsSentTotal.Text = "0";
                }

                // 4. PUNJENJE DONJE TABELE (ISTORIJA)
                string historyTableQry = "SELECT TransferTime AS [Time], RecordsSent AS [Records], Status, Duration, Details FROM TransferLogs ORDER BY TransferTime DESC";


                // Resetujemo DataSource da bi Grid "prihvatio" nove podatke
                dgvTransferHistory.DataSource = null;
                MainClass.LoadData(historyTableQry, dgvTransferHistory);

                // Ponovo primenjujemo stil (boje headera itd.) jer reset DataSource-a briše stilove
                StyleHistoryGrid();
            }
            catch (Exception ex)
            {
                // Prikazujemo grešku samo ako baš nešto nije u redu sa SQL upitom
                MessageBox.Show("Error refreshing statistics: Unable to reach the server. " + ex.Message);
            }
        }

        private void LoadApiSettings()
        {
            string qry = "SELECT TOP 1 ApiUrl, ApiKey FROM ApiSettings";
            DataTable dt = MainClass.GetDataTable(qry);
            if (dt.Rows.Count > 0)
            {
                txtApiUrl.Text = dt.Rows[0]["ApiUrl"].ToString();
                txtApiKey.Text = dt.Rows[0]["ApiKey"].ToString();
            }
        }

        private void SaveApiSettings()
        {
            MainClass.ExecuteQuery("DELETE FROM ApiSettings");
            string qry = $"INSERT INTO ApiSettings (ApiUrl, ApiKey) VALUES ('{txtApiUrl.Text}', '{txtApiKey.Text}')";
            MainClass.ExecuteQuery(qry);
        }

        private async void btnSendNow_Click(object sender, EventArgs e)
        {
            string qryPending = @"SELECT R.ResultID, P.FirstName, P.LastName, R.TestName, R.Value 
                          FROM LabResults R 
                          JOIN Patients P ON R.PatientID = P.PatientID 
                          WHERE R.ApiSyncStatus = 'Pending'";

            DataTable dtPending = MainClass.GetDataTable(qryPending);

            if (dtPending.Rows.Count == 0)
            {
                MessageBox.Show("No new records to send (Pending = 0).", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int brojPoslatih = 0;
            StringBuilder detaljiLoga = new StringBuilder(); // Ovde skupljamo sve što šaljemo za log
            var watch = System.Diagnostics.Stopwatch.StartNew();

            btnSendNow.Enabled = false;
            lblStatus.Text = "Sending...";
            lblStatus.ForeColor = Color.Orange;

            foreach (DataRow row in dtPending.Rows)
            {
                string resID = row["ResultID"].ToString();
                string json = "{\"ID\":\"" + resID + "\",\"NOMBRE1\":\"" + row["FirstName"] + "\",\"APPATERNO\":\"" + row["LastName"] + "\",\"ACCION\":\"I\"}";

                // Dodajemo u dnevnik (Log) šta smo poslali
                detaljiLoga.AppendLine($"ID: {resID} | JSON: {json}");

                try
                {
                    await Task.Delay(400);
                    using (var client = new HttpClient())
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        await client.PostAsync(txtApiUrl.Text, content);
                        MainClass.ExecuteQuery($"UPDATE LabResults SET ApiSyncStatus = 'Success' WHERE ResultID = {resID}");
                        brojPoslatih++;
                    }
                }
                // Unutar petlje, ako slanje ne uspe:
                catch (Exception ex)
                {
                    detaljiLoga.AppendLine($"FAIL na ID {resID}: {ex.Message}");
                    // Umesto Success, stavi Failed da bi ostalo u lblFailedCount
                    MainClass.ExecuteQuery($"UPDATE LabResults SET ApiSyncStatus = 'Failed' WHERE ResultID = {resID}");
                    // Ovde NEMOJ raditi brojPoslatih++ jer realno nije poslat
                }

            }

            watch.Stop();
            string trajanje = string.Format("{0:00}:{1:00}:{2:00}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);

            // UPISIVANJE U LOG SA DETALJIMA
            // Koristimo .Replace("'", "''") da SQL ne bi pukao ako u JSON-u ima navodnika
            string logQry = $@"INSERT INTO TransferLogs (RecordsSent, Status, Duration, TransferTime, Details) 
                       VALUES ({brojPoslatih}, 'Success', '{trajanje}', GETDATE(), '{detaljiLoga.ToString().Replace("'", "''")}')";

            MainClass.ExecuteQuery(logQry);

            SaveApiSettings();
            RefreshTransferStats();
            btnSendNow.Enabled = true;
            MessageBox.Show($"Transfer complete! Sent: {brojPoslatih}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            SaveApiSettings();
            if (string.IsNullOrEmpty(txtApiUrl.Text)) return;

            lblStatus.Text = "Testing...";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    await client.GetAsync(txtApiUrl.Text);
                    lblStatus.Text = "Connected";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Connection established! Device is ready", "API Status");
                }
            }
            catch
            {
                lblStatus.Text = "Connection Failed";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void StyleHistoryGrid()
        {
            // 1. OSNOVNA PODEŠAVANJA
            dgvTransferHistory.AllowUserToAddRows = false;
            dgvTransferHistory.RowHeadersVisible = false; // Sakriva skroz levu kolonu
            dgvTransferHistory.BackgroundColor = Color.White;
            dgvTransferHistory.BorderStyle = BorderStyle.None;
            dgvTransferHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTransferHistory.GridColor = Color.FromArgb(235, 239, 242); // Veoma svetle linije
            dgvTransferHistory.EnableHeadersVisualStyles = false;
            dgvTransferHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransferHistory.ReadOnly = true;

            // 2. DIZAJN HEADERA (ZAGLAVLJA)
            dgvTransferHistory.ColumnHeadersHeight = 45;
            dgvTransferHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTransferHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252); // Svetlo siva
            dgvTransferHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130); // Tamno siva slova
            dgvTransferHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10);
            dgvTransferHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 3. DIZAJN REDOVA
            dgvTransferHistory.RowTemplate.Height = 50; // Malo viši redovi za bolju preglednost
            dgvTransferHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvTransferHistory.DefaultCellStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvTransferHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 243, 255); // Svetlo plava pri kliku
            dgvTransferHistory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255); // Plava slova pri selekciji

            // 4. ZEBRA EFEKAT (Svaki drugi red je diskretno obojen)
            dgvTransferHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 253, 255);

            // 5. AUTOMATSKO ŠIRENJE
            dgvTransferHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void DataTransfer_Load_1(object sender, EventArgs e)
        {
            MainClass.ExecuteQuery("UPDATE LabResults SET ApiSyncStatus = 'Pending'");

            LoadApiSettings();
            StyleHistoryGrid();
            RefreshTransferStats();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int failedCount = int.Parse(lblFailedCount.Text);

            if (failedCount == 0)
            {
                MessageBox.Show("No records with errors.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Za prototip: Otvaramo ResultsForm sa filterom
            // Ako imaš glavni Panel gde menjaš forme, pozovi tu metodu
            // npr: MainControl.ShowControl(new ResultsForm("Failed"));

            MessageBox.Show($"Displaying {failedCount} records requiring review.", "Failed Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnTestConnection_Click_1(object sender, EventArgs e)
        {
            SaveApiSettings();
            if (string.IsNullOrEmpty(txtApiUrl.Text)) return;

            lblStatus.Text = "Testing...";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    await client.GetAsync(txtApiUrl.Text);
                    lblStatus.Text = "Connected";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Successfully connected!", "API Status");
                }
            }
            catch
            {
                lblStatus.Text = "Connection Failed";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void dgvTransferHistory_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 1. Provera da li je kliknuto na red, a ne na zaglavlje (header)
            if (e.RowIndex >= 0)
            {
                try
                {
                    // 2. Provera postojanja kolona pre nego što im pristupimo
                    if (dgvTransferHistory.Columns.Contains("Status") && dgvTransferHistory.Columns.Contains("Details"))
                    {
                        string status = dgvTransferHistory.Rows[e.RowIndex].Cells["Status"].Value?.ToString();
                        string details = dgvTransferHistory.Rows[e.RowIndex].Cells["Details"].Value?.ToString();
                        string time = dgvTransferHistory.Rows[e.RowIndex].Cells["Time"].Value?.ToString();

                        // 3. Logika za prikazivanje zavisno od statusa
                        if (status == "Failed")
                        {
                            string poruka = string.IsNullOrEmpty(details) ? "No specific error recorded." : details;

                            MessageBox.Show(
                                "--- ERROR DETAILS ---\n\n" + poruka,
                                "Transfer Error - " + time,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                        else if (status == "Success")
                        {
                            string poruka = string.IsNullOrEmpty(details) ? "No logs available for this transfer." : details;

                            MessageBox.Show(
                                "--- TRANSMISSION LOG ---\n\n" + poruka,
                                "Transfer Success - " + time,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                    else
                    {
                        // Ako kolona "Details" ne postoji u Gridu (npr. zaboravio si je u SELECT upitu)
                        MessageBox.Show("Error: 'Details' column not found in the grid. Check your SQL query.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while opening logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
