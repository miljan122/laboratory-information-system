using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Information_System
{
    public partial class AnalyserList : Form
    {
        public AnalyserList()
        {
            InitializeComponent();
        }

        private void StyleDataGridView()
        {
            // 1. OSNOVNA PODEŠAVANJA I ČIŠĆENJE
            dgvAnalyzers.AllowUserToAddRows = false;
            dgvAnalyzers.RowHeadersVisible = false;
            dgvAnalyzers.BackgroundColor = Color.White;
            dgvAnalyzers.BorderStyle = BorderStyle.None;
            dgvAnalyzers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAnalyzers.GridColor = Color.FromArgb(245, 245, 245); // Veoma svetle linije
            dgvAnalyzers.EnableHeadersVisualStyles = false; // Dozvoljava nam custom dizajn headera

            // 2. DIZAJN HEADERA (ZAGLAVLJA)
            dgvAnalyzers.ColumnHeadersHeight = 45;
            dgvAnalyzers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAnalyzers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252); // Svetlo sivo-plava
            dgvAnalyzers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130); // Moderan sivi tekst
            dgvAnalyzers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10);
            dgvAnalyzers.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 249, 252); // Da se ne menja boja pri kliku

            // 3. DIZAJN REDOVA I ĆELIJA
            dgvAnalyzers.RowTemplate.Height = 50; // Daje više "vazduha" tabeli
            dgvAnalyzers.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvAnalyzers.DefaultCellStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvAnalyzers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 243, 255); // Svetlo plava pozadina pri selekciji
            dgvAnalyzers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255);   // Jaka plava boja slova pri selekciji

            // 4. SAKRIJ KOLONE (Sistemski podaci)
            string[] hiddenColumns = { "AnalyzerID", "IPAddress", "PortNumber", "ComPort", "BaudRate", "IsActive", "CreatedAt" };
            foreach (string col in hiddenColumns)
            {
                if (dgvAnalyzers.Columns.Contains(col))
                    dgvAnalyzers.Columns[col].Visible = false;
            }

            // 5. REDOSLED I IKONICE (Ono što smo ranije podesili)
            int i = 0;
            foreach (DataGridViewColumn col in dgvAnalyzers.Columns)
            {
                if (col.Name != "dgvEDIT" && col.Name != "dgvDELETE")
                {
                    col.DisplayIndex = i++;
                    // Centriraj tekst u svim kolonama osim prve (opciono)
                    if (i > 1) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }

            // Pozicioniranje ikonica skroz desno
            if (dgvAnalyzers.Columns.Contains("dgvEDIT"))
            {
                dgvAnalyzers.Columns["dgvEDIT"].DisplayIndex = i++;
                dgvAnalyzers.Columns["dgvEDIT"].Width = 50;
                dgvAnalyzers.Columns["dgvEDIT"].HeaderText = ""; // Skloni tekst "dgvEDIT" iz headera
                dgvAnalyzers.Columns["dgvEDIT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            if (dgvAnalyzers.Columns.Contains("dgvDELETE"))
            {
                dgvAnalyzers.Columns["dgvDELETE"].DisplayIndex = i++;
                dgvAnalyzers.Columns["dgvDELETE"].Width = 50;
                dgvAnalyzers.Columns["dgvDELETE"].HeaderText = ""; // Skloni tekst "dgvDELETE" iz headera
                dgvAnalyzers.Columns["dgvDELETE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            dgvAnalyzers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        // Ova metoda NE TREBA da ima ništa u zagradi ()
        public void LoadData()
        {
            string qry = @"SELECT AnalyzerID, ModelName, Manufacturer, ConnectionType, 
          IPAddress, PortNumber, ComPort, BaudRate, Status 
          FROM Analyzers2";

            // Prosledi null umesto novog objekta
            MainClass.LoadData(qry, dgvAnalyzers);
            StyleDataGridView();
        }




        private void dgvAnalyzers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // EDIT - Otvaranje forme AddAnalysers sa podacima
            if (dgvAnalyzers.Columns[e.ColumnIndex].Name == "dgvEDIT")
            {
                AddAnalysers frm = new AddAnalysers();
                frm.id = Convert.ToInt32(dgvAnalyzers.CurrentRow.Cells["AnalyzerID"].Value);

                // 2. Osnovni podaci
                frm.txtModel.Text = dgvAnalyzers.CurrentRow.Cells["ModelName"].Value?.ToString();
                frm.txtManufacturer.Text = dgvAnalyzers.CurrentRow.Cells["Manufacturer"].Value?.ToString();
                frm.cmbType.Text = dgvAnalyzers.CurrentRow.Cells["ConnectionType"].Value?.ToString();

                // 3. POPUNJAVANJE OSTALIH POLJA (IP, Port, COM, Baud)
                // Koristimo ?.ToString() da izbegnemo grešku ako je polje u bazi prazno (NULL)

                // Mrežni parametri
                frm.txtIP.Text = dgvAnalyzers.CurrentRow.Cells["IPAddress"].Value?.ToString();
                frm.txtPort.Text = dgvAnalyzers.CurrentRow.Cells["PortNumber"].Value?.ToString();

                // Serijski parametri
                frm.cmbCOM.Text = dgvAnalyzers.CurrentRow.Cells["ComPort"].Value?.ToString();
                frm.cmbBaud.Text = dgvAnalyzers.CurrentRow.Cells["BaudRate"].Value?.ToString();

                // Pozivamo metodu da odmah sakrije/prikaže ispravna polja na formi
                // (Ovo je važno da bi laborant video samo ono što mu treba čim se forma otvori)
                MainClass.BlurBackground(frm);
            }

            // DELETE - Brisanje aparata
            // DELETE - Brisanje aparata
            if (dgvAnalyzers.Columns[e.ColumnIndex].Name == "dgvDELETE")
            {
                if (MessageBox.Show("Ovaj aparat ima snimljene rezultate. Brisanjem aparata obrisaćete i SVE njegove rezultate iz baze! Da li ste sigurni?",
                    "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dgvAnalyzers.CurrentRow.Cells["AnalyzerID"].Value);

                        // Prvo brišemo rezultate (dete), pa onda aparat (roditelj)
                        // Koristimo jedan upit da bi bilo brže i sigurnije
                        string qry = $@"DELETE FROM LabResults WHERE AnalyzerID = {id};
                            DELETE FROM Analyzers2 WHERE AnalyzerID = {id};";

                        if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();

                        SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                        cmd.ExecuteNonQuery();

                        MainClass.con.Close();

                        MessageBox.Show("The device and all its results have been successfully deleted.");
                        LoadData(); // Osvežava tabelu
                    }
                    catch (Exception ex)
                    {
                        MainClass.con.Close();
                        MessageBox.Show("Error while deleting: " + ex.Message);
                    }
                }
            }
        }

            private void AnalyserList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
