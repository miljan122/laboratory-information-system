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
    public partial class frmMain : Form
    {
    
        static frmMain _obj;

        public static frmMain Instance
        {
            get { return _obj; } // Samo vrati postojeći, nemoj praviti 'new' ovde
        }

        public frmMain()
        {
            InitializeComponent();
            _obj = this; // Postavi 'obj' na ovu instancu čim se napravi
        }
        public void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetSidebarButtonsStyle();
        }

        private void SetSidebarButtonsStyle()
        {
            foreach (Control c in leftPanel.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2Button btn)
                {
                    // 1. Isključi fokus (da nema onih isprekidanih tačkica oko teksta)
                    btn.TabStop = false;

                    // 2. Osnovna podešavanja (Geometrija)
                    btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    btn.BorderThickness = 0; // Ovo sklanja običan okvir oko celog dugmeta
                    btn.CustomBorderThickness = new Padding(4, 0, 0, 0); // Samo linija levo

                    // 3. Boje - Normalno stanje
                    btn.FillColor = Color.White;
                    btn.ForeColor = Color.DimGray;
                    btn.CustomBorderColor = Color.Transparent; // Linija je tu, ali je providna

                    // 4. Boje - Kliknuto stanje (CheckedState)
                    // OVDE NE DODELJUJEMO BorderThickness jer ne postoji u CheckedState
                    btn.CheckedState.FillColor = Color.FromArgb(240, 247, 255);
                    btn.CheckedState.ForeColor = Color.FromArgb(0, 123, 255);
                    btn.CheckedState.CustomBorderColor = Color.FromArgb(0, 123, 255); // Postaje plava kad klikneš
                }
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new Dashboard());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddControls(new PatientsForm());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddControls(new AnalyserForm());
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            AddControls(new ResultsForm());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AddControls(new DataTransfer());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AddControls(new Logs());
        }
    }

   
}
