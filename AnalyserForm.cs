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
    public partial class AnalyserForm : Form
    {
        public AnalyserForm()
        {
            InitializeComponent();
        }

        public void LoadAnalyzerCards()
        {
            flpMain.Controls.Clear();

            try
            {
                string qry = "SELECT * FROM Analyzers2 WHERE IsActive = 1";

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

                using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        ucAnalyzerCard card = new ucAnalyzerCard();

                        card.lblID.Text = "ID: " + row["AnalyzerID"].ToString();
                        card.lblModel.Text = row["ModelName"].ToString().ToUpper();

                        string status = row["Status"]?.ToString() ?? "Offline";
                        card.lblStatus.Text = status;

                        // Boja statusa pri učitavanju
                        if (status.ToLower() == "online" || status.ToLower() == "connected")
                            card.lblStatus.ForeColor = Color.FromArgb(40, 167, 69);
                        else
                            card.lblStatus.ForeColor = Color.FromArgb(220, 53, 69);

                        string connType = row["ConnectionType"].ToString();
                        card.lblConnection.Text = connType;

                        if (connType == "TCP/IP")
                        {
                            card.lblTitleAddress.Text = "IP Address";
                            card.lblTitlePort.Text = "TCP Port";
                            card.lblAddress.Text = row["IPAddress"].ToString();
                            card.lblPort.Text = row["PortNumber"].ToString();
                        }
                        else // RS232
                        {
                            card.lblTitleAddress.Text = "COM Port";
                            card.lblTitlePort.Text = "Baud Rate";
                            card.lblAddress.Text = row["ComPort"].ToString();
                            card.lblPort.Text = row["BaudRate"].ToString();
                        }

                        // Zadnji podaci
                        if (row["CreatedAt"] != DBNull.Value)
                            card.lblLastData.Text = Convert.ToDateTime(row["CreatedAt"]).ToString("dd/MM/yyyy HH:mm:ss");
                        else
                            card.lblLastData.Text = "No data received";

                        flpMain.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating cards: " + ex.Message);
            }
            finally
            {
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }
            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            AddAnalysers ad = new AddAnalysers();
            MainClass.BlurBackground(ad);
            LoadAnalyzerCards();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AnalyserList ad = new AnalyserList();
            MainClass.BlurBackground(ad);
        }

        private void AnalyserForm_Load(object sender, EventArgs e)
        {
            LoadAnalyzerCards();
        }
    }
}
