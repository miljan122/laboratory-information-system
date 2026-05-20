using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Information_System
{
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            // Ručno povezivanje događaja (ako nisu povezani u Designeru)
            dtFrom.ValueChanged += dtFrom_ValueChanged;
            dtTo.ValueChanged += dtTo_ValueChanged;
            cmbSource.SelectedIndexChanged += cmbSource_SelectedIndexChanged;
            cmbEvent.SelectedIndexChanged += cmbEvent_SelectedIndexChanged;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;

            // 1. Podesi datume (od prvog u mesecu do danas)
            dtFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo.Value = DateTime.Now;

            // 2. Napuni filtere
            cmbSource.Items.Clear();
            cmbSource.Items.AddRange(new string[] { "All Sources", "BK-280", "MS-H655", "U500", "System" });
            cmbSource.SelectedIndex = 0;

            cmbEvent.Items.Clear();
            cmbEvent.Items.AddRange(new string[] { "All Events", "Data Capture", "Export", "Connection", "Data Transfer" });
            cmbEvent.SelectedIndex = 0;

            // 3. Učitaj podatke
            FilterLogs();
        }


        public void FilterLogs()
        {
            try
            {
                // SQL sa LEFT JOIN-om da povučemo Username iz tabele Users
                // Ako je UserID NULL, ISNULL funkcija će ispisati 'System'
                string qry = @"SELECT 
                                L.LogID, 
                                L.LogTime, 
                                ISNULL(U.Username, 'System') AS [User], 
                                L.Source, 
                                L.EventType, 
                                L.Status, 
                                L.Message, 
                                L.RawData 
                              FROM SystemLogs L
                              LEFT JOIN Users U ON L.UserID = U.UserID
                              WHERE L.LogTime >= @from AND L.LogTime <= @to";

                if (cmbSource.SelectedIndex > 0) qry += " AND L.Source = @source";
                if (cmbEvent.SelectedIndex > 0) qry += " AND L.EventType = @event";
                if (!string.IsNullOrWhiteSpace(guna2TextBox1.Text)) qry += " AND L.Message LIKE @search";

                qry += " ORDER BY L.LogTime DESC";

                if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();

                using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
                {
                    cmd.Parameters.AddWithValue("@from", dtFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@to", dtTo.Value.Date.AddDays(1).AddSeconds(-1));

                    if (cmbSource.SelectedIndex > 0) cmd.Parameters.AddWithValue("@source", cmbSource.Text);
                    if (cmbEvent.SelectedIndex > 0) cmd.Parameters.AddWithValue("@event", cmbEvent.Text);
                    if (!string.IsNullOrWhiteSpace(guna2TextBox1.Text)) cmd.Parameters.AddWithValue("@search", "%" + guna2TextBox1.Text + "%");

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    dgvLogs.DataSource = null;
                    dgvLogs.AutoGenerateColumns = true;
                    dgvLogs.DataSource = dt;

                    StyleLogsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                MainClass.con.Close();
            }
        }

        private void StyleLogsGrid()
        {
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Header dizajn
            dgvLogs.ColumnHeadersHeight = 40;
            dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130);
            dgvLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9);

            // Redovi dizajn
            dgvLogs.RowTemplate.Height = 40;
            dgvLogs.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 253, 255);

            // Kolone
            if (dgvLogs.Columns.Count > 0)
            {
                if (dgvLogs.Columns.Contains("LogID")) dgvLogs.Columns["LogID"].Visible = false;
                if (dgvLogs.Columns.Contains("RawData")) dgvLogs.Columns["RawData"].Visible = false;

                if (dgvLogs.Columns.Contains("LogTime")) dgvLogs.Columns["LogTime"].HeaderText = "Date / Time";
                if (dgvLogs.Columns.Contains("User")) dgvLogs.Columns["User"].FillWeight = 70;
                if (dgvLogs.Columns.Contains("EventType")) dgvLogs.Columns["EventType"].HeaderText = "Event";
                if (dgvLogs.Columns.Contains("Message")) dgvLogs.Columns["Message"].FillWeight = 180;

                dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e) => FilterLogs();
        private void dtTo_ValueChanged(object sender, EventArgs e) => FilterLogs();
        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e) => FilterLogs();
        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e) => FilterLogs();
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) => FilterLogs();

        private void dgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLogs.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Error" || status == "Failed") e.CellStyle.ForeColor = Color.Crimson;
                else if (status == "Success") e.CellStyle.ForeColor = Color.SeaGreen;
            }
        }
    }
}
