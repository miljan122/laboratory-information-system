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
    public partial class ucDashboardCard : UserControl
    {
        public ucDashboardCard()
        {
            InitializeComponent();
        }
        public void SetAnalyzerData(string model, string manufacturer, string status, string lastScan)
        {
            lblModel.Text = model.ToUpper();
            lblManufacture.Text = manufacturer;
            lblStatus.Text = status;
            lblLastScan.Text = "Last scan: " + lastScan;

            // Menjanje boje statusa
            if (status.ToLower() == "connected" || status.ToLower() == "online")
                lblStatus.ForeColor = Color.FromArgb(40, 167, 69); // Zelena
            else
                lblStatus.ForeColor = Color.FromArgb(220, 53, 69); // Crvena
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string model = lblModel.Text;
            // MessageBox.Show("Kliknuto na: " + model); // OVO DODAJ ZA TEST

            if (MainForm.Instance != null)
            {
             
            }
        }
        }
}

