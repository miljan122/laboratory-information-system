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
    public partial class ucAnalyzerCard : UserControl
    {
        public ucAnalyzerCard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        // Dodaj 'async' ovde:
        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            btnTestConnection.Text = "Testing...";
            btnTestConnection.Enabled = false;

            bool isConnected = false;

            // Tvoja postojeća logika za čišćenje IP adrese i Porta
            string addressValue = lblAddress.Text.Replace("IP Address", "").Replace("COM Port", "").Replace("IP", "").Replace(":", "").Trim();
            string portValue = lblPort.Text.Replace("TCP Port", "").Replace("Baud Rate", "").Replace("Port", "").Replace(":", "").Trim();

            // Izvršavanje provere
            if (lblConnection.Text == "TCP/IP")
            {
                if (int.TryParse(portValue, out int port))
                {
                    isConnected = await Task.Run(() => MainClass.CheckTcpConnection(addressValue, port));
                }
            }
            else // RS232
            {
                isConnected = await Task.Run(() => MainClass.CheckSerialConnection(addressValue));
            }

            // --- OVDE IDE TAJ DEO KODA ---
            if (isConnected)
            {
                lblStatus.Text = "Connected";
                lblStatus.ForeColor = Color.FromArgb(40, 167, 69); // Zelena

                // OVO JE KLJUČ: Trajno čuvamo u bazi da bi Dashboard video status "Connected"
                string idRaw = lblID.Text.Replace("ID:", "").Trim();
                MainClass.ExecuteQuery($"UPDATE Analyzers2 SET Status = 'Connected' WHERE AnalyzerID = {idRaw}");

                MessageBox.Show($"Connection to {lblModel.Text} Successful!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "Offline";
                lblStatus.ForeColor = Color.FromArgb(220, 53, 69); // Crvena

                // Možeš dodati i ovo da bazu vratiš na Offline ako test ne uspe
                string idRaw = lblID.Text.Replace("ID:", "").Trim();
                MainClass.ExecuteQuery($"UPDATE Analyzers2 SET Status = 'Offline' WHERE AnalyzerID = {idRaw}");

                MessageBox.Show($"Connection to {lblModel.Text} Failed!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnTestConnection.Text = "Test Connection";
            btnTestConnection.Enabled = true;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddAnalysers frm = new AddAnalysers();

            // 1. Čupamo čist ID (ako piše "ID: 5", uzimamo samo "5")
            string idRaw = lblID.Text.Replace("ID:", "").Trim();

            if (int.TryParse(idRaw, out int resultID))
            {
                frm.id = resultID; // Prosleđujemo ID da bi radio UPDATE, a ne INSERT
                frm.txtModel.Text = lblModel.Text;
                frm.cmbType.Text = lblConnection.Text;

                // 2. Čistimo vrednosti od svih mogućih prefiksa i simbola ":"
                string cleanAddr = lblAddress.Text.Replace("IP Address", "").Replace("COM Port", "").Replace(":", "").Trim();
                string cleanPort = lblPort.Text.Replace("TCP Port", "").Replace("Baud Rate", "").Replace(":", "").Trim();

                if (lblConnection.Text == "TCP/IP")
                {
                    frm.txtIP.Text = cleanAddr;
                    frm.txtPort.Text = cleanPort;
                }
                else
                {
                    frm.cmbCOM.Text = cleanAddr;
                    frm.cmbBaud.Text = cleanPort;
                }

                MainClass.BlurBackground(frm);

                // 3. Osvežavanje glavne forme nakon zatvaranja
                if (this.ParentForm is AnalyserForm parent)
                {
                    parent.LoadAnalyzerCards();
                }
            }
        }
    }
    }

