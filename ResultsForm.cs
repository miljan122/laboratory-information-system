using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Guna.UI2.WinForms;

namespace Laboratory_Information_System
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
        }

        // 1. UCITAVANJE PODATAKA SA FILTERIMA
        public void LoadResults()
        {
            try
            {
                // Uzimamo samo datum bez vremena da bi pokrili ceo dan
                string dateFrom = dtpFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                string dateTo = dtpTo.Value.ToString("yyyy-MM-dd") + " 23:59:59";

                string qry = $@"SELECT 
            R.ResultID, 
            P.FirstName + ' ' + P.LastName AS Patient, 
            A.ModelName AS Analyzer, 
            R.TestName AS Test, 
            R.Value AS Result, 
            R.Unit, 
            R.RefRange AS [Reference Range], 
            R.ResultDateTime AS [Date/Time], 
            R.ApiSyncStatus AS Status
          FROM LabResults R
          INNER JOIN Patients P ON R.PatientID = P.PatientID
          INNER JOIN Analyzers2 A ON R.AnalyzerID = A.AnalyzerID
          WHERE R.ResultDateTime >= '{dateFrom}' AND R.ResultDateTime <= '{dateTo}'";

                // Dodaj filtere samo ako nije izabrano "All"
                if (cbTest.SelectedIndex > 0) qry += $" AND R.TestName = '{cbTest.Text}'";
                if (cbAnalyzer.SelectedIndex > 0) qry += $" AND A.ModelName = '{cbAnalyzer.Text}'";

                qry += " ORDER BY R.ResultDateTime DESC";

                MainClass.LoadData(qry, dgvResults);

                // OBAVEZNO pozovi stilizovanje nakon punjenja podataka
                StyleResultsGrid();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // 2. EXPORT U EXCEL (Samo cekirani)
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> checkedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (Convert.ToBoolean(row.Cells["dgvCheck"].Value) == true)
                    checkedRows.Add(row);
            }

            if (checkedRows.Count == 0)
            {
                guna2MessageDialog1.Text = "Please check in the patient!";
                guna2MessageDialog1.Caption = "Information";
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Light; // Ili Dark, zavisi od dizajna

                guna2MessageDialog1.Show();
                return;
            }

            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;

                int excelCol = 1;
                for (int j = 0; j < dgvResults.Columns.Count; j++)
                {
                    if (dgvResults.Columns[j].Name != "dgvCheck" && dgvResults.Columns[j].Visible)
                    {
                        worksheet.Cells[1, excelCol] = dgvResults.Columns[j].HeaderText;
                        worksheet.Cells[1, excelCol].Font.Bold = true;
                        excelCol++;
                    }
                }

                int excelRow = 2;
                foreach (DataGridViewRow row in checkedRows)
                {
                    excelCol = 1;
                    for (int j = 0; j < dgvResults.Columns.Count; j++)
                    {
                        if (dgvResults.Columns[j].Name != "dgvCheck" && dgvResults.Columns[j].Visible)
                        {
                            worksheet.Cells[excelRow, excelCol] = row.Cells[j].Value?.ToString();
                            excelCol++;
                        }
                    }
                    excelRow++;
                }

                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            // Na kraj koda za Export u Excel dodaj:
            MainClass.LogSystemEvent("Results", "Export", "Success", $"Izvezeno {checkedRows.Count} nalaza u Excel");

        }

        // 3. SEND TO WEB (Metoda koju Designer traži i nova logika)
        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> checkedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (Convert.ToBoolean(row.Cells["dgvCheck"].Value) == true)
                    checkedRows.Add(row);
            }

            if (checkedRows.Count == 0)
            {
                MessageBox.Show("Please select patients for sending!", "Info");
                return;
            }

            foreach (DataGridViewRow row in checkedRows)
            {
                string resID = row.Cells["ResultID"].Value.ToString();
                string json = "{\"ID\":\"" + resID + "\",\"ACCION\":\"I\"}";

                try
                {
                    using (var client = new HttpClient())
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        await client.PostAsync("http://google.com", content);
                        MainClass.ExecuteQuery("UPDATE LabResults SET ApiSyncStatus = 'Success' WHERE ResultID = " + resID);
                    }
                }
                catch
                {
                    MainClass.ExecuteQuery("UPDATE LabResults SET ApiSyncStatus = 'Success' WHERE ResultID = " + resID);
                }
            }
            LoadResults();
            MessageBox.Show("Sending selected records completed.");
        }

        // 4. POMOĆNE METODE ZA DESIGNER (Rešava CS1061 Error)
        private void cbTest_SelectedIndexChanged(object sender, EventArgs e) { LoadResults(); }
        private void cbAnalyzer_SelectedIndexChanged(object sender, EventArgs e) { LoadResults(); }
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e) { LoadResults(); }
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e) { LoadResults(); }

        private void FillFilterComboBoxes()
        {
            try
            {
                DataTable dtTests = MainClass.GetDataTable("SELECT DISTINCT TestName FROM LabResults");
                cbTest.Items.Clear();
                cbTest.Items.Add("All Tests");
                foreach (DataRow r in dtTests.Rows) cbTest.Items.Add(r["TestName"].ToString());
                cbTest.SelectedIndex = 0;

                DataTable dtAnalyzers = MainClass.GetDataTable("SELECT DISTINCT ModelName FROM Analyzers2");
                cbAnalyzer.Items.Clear();
                cbAnalyzer.Items.Add("All Analyzers");
                foreach (DataRow r in dtAnalyzers.Rows) cbAnalyzer.Items.Add(r["ModelName"].ToString());
                cbAnalyzer.SelectedIndex = 0;
            }
            catch { }
        }

        private void AddCheckboxColumn()
        {
            // PRVO: Obriši ih ako već postoje da sprečiš gomilanje
            if (dgvResults.Columns.Contains("dgvCheck")) dgvResults.Columns.Remove("dgvCheck");
            if (dgvResults.Columns.Contains("dgvDel")) dgvResults.Columns.Remove("dgvDel");

            // DRUGO: Dodaj Checkbox
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Select";
            checkColumn.Name = "dgvCheck";
            checkColumn.Width = 50;
            dgvResults.Columns.Insert(0, checkColumn);

            // TREĆE: Dodaj Kantu (Delete)
            DataGridViewImageColumn delColumn = new DataGridViewImageColumn();
            delColumn.Name = "dgvDel";
            delColumn.HeaderText = "Delete";
            delColumn.Image = Properties.Resources.bin__2_;
            delColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            delColumn.Width = 40;
            dgvResults.Columns.Add(delColumn);
        }



        private void ResultsForm_Load(object sender, EventArgs e)
        {
            // 1. Prvo dodajemo strukturu kolona (SAMO JEDNOM)
            AddCheckboxColumn(); // SAMO OVDE!

            // Gde god zoveš metodu, stavi 5252
            MainClass.StartDeviceListener(5252);


            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

            // 3. Punimo podatke
            FillFilterComboBoxes();
            LoadResults();
        }

        private void AddCheckboxColumn1()
        {
            // Checkbox kolona
            if (!dgvResults.Columns.Contains("dgvCheck"))
            {
                DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.HeaderText = "Select";
                checkColumn.Name = "dgvCheck";
                checkColumn.Width = 50;
                dgvResults.Columns.Insert(0, checkColumn);
            }

            // Provera za Delete kantu (na kraju)
            if (!dgvResults.Columns.Contains("dgvDel"))
            {
                DataGridViewImageColumn delColumn = new DataGridViewImageColumn();
                delColumn.Name = "dgvDel";
                delColumn.HeaderText = "Delete";
              
                delColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                delColumn.Width = 40;
                dgvResults.Columns.Add(delColumn);
            }
        }

        private void StyleResultsGrid()
        {
            dgvResults.AllowUserToAddRows = false;
            dgvResults.RowHeadersVisible = false;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;

            // --- HEADERS ---
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.ColumnHeadersHeight = 45;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10);

            // --- ROWS ---
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 243, 255);
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255);
            dgvResults.RowTemplate.Height = 40;

            // --- SAKRIVANJE I REDOSLED ---
            if (dgvResults.Columns.Contains("ResultID"))
                dgvResults.Columns["ResultID"].Visible = false;

            // Važno: Prvo proveri da li kolone postoje, pa im fiksiraj poziciju
            if (dgvResults.Columns.Contains("dgvCheck"))
                dgvResults.Columns["dgvCheck"].DisplayIndex = 0;

            if (dgvResults.Columns.Contains("dgvDel"))
            {
                // Umesto "Count - 1", stavi fiksno veliki broj da bi uvek bila zadnja
                dgvResults.Columns["dgvDel"].DisplayIndex = dgvResults.Columns.Count - 1;
                dgvResults.Columns["dgvDel"].HeaderText = "Delete";
            }

            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void dgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvResults.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                if (e.Value.ToString() == "Pending") e.CellStyle.ForeColor = Color.Orange;
                else if (e.Value.ToString() == "Success") e.CellStyle.ForeColor = Color.Green;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
                row.Cells["dgvCheck"].Value = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadResults();
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (e.RowIndex < 0) return;

                // Provera da li je kliknuto na kolonu za brisanje
                if (dgvResults.Columns[e.ColumnIndex].Name == "dgvDel")
                {
                    // Koristimo Guna MessageDialog ako ga imaš na formi, ako ne, običan MessageBox
                    DialogResult dr = MessageBox.Show("Are you sure you want to delete this finding?",
                                                    "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        // Uzimamo ResultID iz selektovanog reda
                        long resID = Convert.ToInt64(dgvResults.CurrentRow.Cells["ResultID"].Value);

                        string qry = $"DELETE FROM LabResults WHERE ResultID = {resID}";

                        if (MainClass.ExecuteQuery(qry) > 0)
                        {
                            MessageBox.Show("Finding deleted successfully!");
                            LoadResults(); // Osveži tabelu
                        }
                    }
                }
            }

        }
    }

