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
    public partial class MainForm : Form
    {
        // 1. STATIČKA INSTANCA - Omogućava ucDashboardCard-u da pristupi ovoj formi
        public static MainForm Instance;

        public MainForm()
        {
            InitializeComponent();
            Instance = this; // Postavljanje instance
        }

        // 2. DOGAĐAJ PRI UČITAVANJU FORME
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Ovde možeš učitati Dashboard po defaultu pri paljenju
            // ShowControl(new DashboardForm()); 
        }

        // 3. METODA ZA PRIKAZ REZULTATA (Poziva je ucDashboardCard)
        public void ShowResultsForAnalyzer(string analyzerName)
        {
            ResultsForm frm = new ResultsForm();

            // 1. Prvo prikaži formu u panelu
            ShowControl(frm);

            // Dajemo Windowsu milisekundu da iscrta formu i napuni ComboBox-ove
            Application.DoEvents();

            // 2. Postavljamo filter na APARAT (cbAnalyzer), a ne na test
            // PROVERI: cbAnalyzer mora biti PUBLIC u ResultsForm.Designer.cs
            if (frm.cbAnalyzer != null)
            {
                // Trim() skida prazna mesta, a ToUpper() osigurava da se poklope velika slova
                frm.cbAnalyzer.Text = analyzerName.Trim();

                // 3. Pokreni upit
                frm.LoadResults();
            }
        }

        // 4. UNIVERZALNA METODA ZA PRIKAZ FORMI/KONTROLA U PANELU
        public void ShowControl(Control ad)
        {
            // Čistimo centralni panel pre dodavanja nove forme
            centerPanel.Controls.Clear();

            // Ako je kontrola koju dodajemo zapravo Forma, sređujemo je da ne bude TopLevel
            if (ad is Form frm)
            {
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
            }

            ad.Dock = DockStyle.Fill;
            centerPanel.Controls.Add(ad);

            // Ako je u pitanju forma, moramo je eksplicitno pokazati
            if (ad is Form f)
            {
                f.Show();
            }
        }

        // --- OVDE MOŽEŠ DODATI EVENTE ZA SIDEBAR DUGMIĆE ---

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // ShowControl(new DashboardForm());
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            ShowControl(new ResultsForm());
        }

        private void btnDataTransfer_Click(object sender, EventArgs e)
        {
            ShowControl(new DataTransfer());
        }
    }
}
