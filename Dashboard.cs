using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Information_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void DesignTableHeader()
        {
            // 1. Dodavanje kolona (samo definicija headera)
            dgvLatestResults.Columns.Clear();
            dgvLatestResults.Columns.Add("ID", "ID");
            dgvLatestResults.Columns.Add("PatientName", "PATIENT NAME");
            dgvLatestResults.Columns.Add("TestType", "TEST TYPE");
            dgvLatestResults.Columns.Add("Result", "RESULT");
            dgvLatestResults.Columns.Add("Status", "STATUS");
            dgvLatestResults.Columns.Add("Date", "DATE TIME");

            // 2. Osnovni dizajn tabele (pozadina i ivice)
            dgvLatestResults.BackgroundColor = Color.White;
            dgvLatestResults.BorderStyle = BorderStyle.None;
            dgvLatestResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLatestResults.GridColor = Color.FromArgb(245, 245, 245); // Veoma svetla linija
            dgvLatestResults.RowHeadersVisible = false; // Sklanja levu marginu

            // 3. DIZAJN HEADERA (Ovo tražiš)
            dgvLatestResults.ColumnHeadersHeight = 45;
            dgvLatestResults.EnableHeadersVisualStyles = false; // Dozvoljava custom dizajn

            // Boja pozadine headera (Svetlo siva/plavičasta kao na slici)
            dgvLatestResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            // Boja teksta u headeru
            dgvLatestResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130);
            // Font headera
            dgvLatestResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            // Poravnanje
            dgvLatestResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 4. Stil redova (da budu spremni za kasnije)
            dgvLatestResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 247, 255);
            dgvLatestResults.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255);
            dgvLatestResults.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0); // Pomera tekst od ivice

            // 5. Automatsko širenje da popuni panel
            dgvLatestResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadDashboardCards()
        {
            // 1. Čistimo stare kartice pre nego što učitamo nove
            flpAnalyzers.Controls.Clear();

            try
            {
                // 2. Upit uzima samo aktivne aparate
                string qry = "SELECT * FROM Analyzers2 WHERE IsActive = 1";
                DataTable dt = MainClass.GetDataTable(qry);

                foreach (DataRow row in dt.Rows)
                {
                    // 3. Kreiramo novu karticu
                    ucDashboardCard card = new ucDashboardCard();

                    // 4. Pronalazimo zadnji rezultat za taj aparat (da bi popunili lblLastScan)
                    string analyzerID = row["AnalyzerID"].ToString();
                    string lastScanTime = "No data";

                    DataTable dtLast = MainClass.GetDataTable($@"SELECT TOP 1 ResultDateTime 
                                                       FROM LabResults 
                                                       WHERE AnalyzerID = {analyzerID} 
                                                       ORDER BY ResultDateTime DESC");

                    if (dtLast.Rows.Count > 0)
                        lastScanTime = Convert.ToDateTime(dtLast.Rows[0][0]).ToString("dd/MM/yyyy HH:mm");

                    // 5. Pozivamo metodu iz kartice da popuni labele
                    card.SetAnalyzerData(
                        row["ModelName"].ToString(),
                        row["Manufacturer"].ToString(),
                        row["Status"].ToString(),
                        lastScanTime
                    );

                    // 6. Dodajemo karticu u FlowLayoutPanel
                    flpAnalyzers.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Dashboard: " + ex.Message);
            }
        }




        public void LoadLatestResults()
        {
            // SQL upit sa jasnim nazivima kolona (Alias-ima)
            string qry = @"SELECT TOP 5 
          P.FirstName + ' ' + P.LastName AS [PATIENT NAME], 
          R.TestName AS [TEST TYPE], 
          R.Value AS [RESULT], 
          R.Unit AS [UNIT], 
          R.ResultDateTime AS [DATE TIME]
          FROM LabResults R
          INNER JOIN Patients P ON R.PatientID = P.PatientID
          INNER JOIN Analyzers2 A ON R.AnalyzerID = A.AnalyzerID
          ORDER BY R.ResultDateTime DESC";

            MainClass.LoadData(qry, dgvLatestResults);

            // Sada primenjujemo "peglanje" dizajna
            StyleDashboardGrid();
        }


        private void StyleDashboardGrid()
        {
            dgvLatestResults.AllowUserToAddRows = false;
            dgvLatestResults.RowHeadersVisible = false;
            dgvLatestResults.BackgroundColor = Color.White;
            dgvLatestResults.BorderStyle = BorderStyle.None;
            dgvLatestResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLatestResults.GridColor = Color.FromArgb(240, 240, 240);
            dgvLatestResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLatestResults.MultiSelect = false;

            // --- DIZAJN HEADERA ---
            dgvLatestResults.EnableHeadersVisualStyles = false;
            dgvLatestResults.ColumnHeadersHeight = 45;
            dgvLatestResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Svetlo siva pozadina, tamno sivi tekst (Moderni Dashboard stil)
            dgvLatestResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dgvLatestResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130);
            dgvLatestResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9);
            dgvLatestResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- DIZAJN REDOVA ---
            dgvLatestResults.RowTemplate.Height = 45;
            dgvLatestResults.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvLatestResults.DefaultCellStyle.ForeColor = Color.FromArgb(70, 70, 70);
            dgvLatestResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 243, 255);
            dgvLatestResults.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255);

            // Padding da tekst ne udara u ivice
            dgvLatestResults.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);

            // Automatsko širenje
            dgvLatestResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadLatestResults();
            LoadDashboardCards();
           
           
            StyleDashboardGrid();
        }
    }
}
