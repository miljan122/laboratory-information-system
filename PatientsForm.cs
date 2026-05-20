using Guna.UI2.WinForms;
using System;
using System.Collections;
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
    public partial class PatientsForm : Form
    {
        public PatientsForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            NewPatients tbs = new NewPatients();
            MainClass.BlurBackground(tbs);
            LoadData();
        }
        private void LoadData()
        {
            SqlCommand cmd = new SqlCommand("Select * from Patients", MainClass.con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dgvPatients.DataSource = dt;
        }

        private void StyleDataGridView()
        {
            // 1. ISKLJUČI PRAZAN RED NA DNU
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AutoGenerateColumns = true; // Pustimo ga da povuče iz baze, pa ćemo mi srediti

            // 2. MODERAN DIZAJN (Kao na slici)
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.GridColor = Color.FromArgb(245, 245, 245);
            dgvPatients.RowTemplate.Height = 45;
            dgvPatients.ColumnHeadersHeight = 45;
            dgvPatients.EnableHeadersVisualStyles = false;
            dgvPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(110, 115, 130);
            dgvPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10);
            dgvPatients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 243, 255);
            dgvPatients.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 123, 255);
            dgvPatients.BorderStyle = BorderStyle.None;

            // 3. SAKRIJ KOLONE KOJE SE NE PRIKAZUJU LABORANTU
            if (dgvPatients.Columns.Contains("PatientID")) dgvPatients.Columns["PatientID"].Visible = false;
            if (dgvPatients.Columns.Contains("ExternalID")) dgvPatients.Columns["ExternalID"].Visible = false;

            // 4. FIX REDOSLEDA (Rešenje tvog problema)
            // Prvo kažemo svim kolonama da idu po redu iz baze
            int index = 0;
            string[] order = { "Barcode", "FirstName", "LastName", "Gender", "BirthDate",
                       "NationalID", "Phone", "Email", "CreatedAt", "UpdatedAt",
                       "IsValidated", "SyncStatus", "dgvEDIT", "dgvDELETE" };

            foreach (string colName in order)
            {
                if (dgvPatients.Columns.Contains(colName))
                {
                    dgvPatients.Columns[colName].DisplayIndex = index++;
                }
            }

            // 5. ŠIRINA ZA IKONICE
            if (dgvPatients.Columns.Contains("dgvEDIT"))
            {
                dgvPatients.Columns["dgvEDIT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPatients.Columns["dgvEDIT"].Width = 35;
                dgvPatients.Columns["dgvEDIT"].HeaderText = ""; // Skloni naslov iznad olovke
            }
            if (dgvPatients.Columns.Contains("dgvDELETE"))
            {
                dgvPatients.Columns["dgvDELETE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPatients.Columns["dgvDELETE"].Width = 35;
                dgvPatients.Columns["dgvDELETE"].HeaderText = ""; // Skloni naslov iznad kante
            }

            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }





        private void PatientsForm_Load(object sender, EventArgs e)
        {
            LoadData();
            StyleDataGridView();
        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Provera da li je kliknuto na zaglavlje (Header)
            if (e.RowIndex < 0) return;

            // --- 2. EDIT LOGIKA (dgvEDIT) ---
            if (dgvPatients.Columns[e.ColumnIndex].Name == "dgvEDIT")
            {
                NewPatients fma = new NewPatients();

                // Primarni ID za Update (proveri da li se kolona u bazi zove PatientID)
                fma.id = Convert.ToInt32(dgvPatients.CurrentRow.Cells["PatientID"].Value);

                // Prenos podataka u TextBox-ove (Modifiers na NewPatients moraju biti Public)
                fma.txtBarcode.Text = dgvPatients.CurrentRow.Cells["Barcode"].Value?.ToString();
                fma.txtIme.Text = dgvPatients.CurrentRow.Cells["FirstName"].Value?.ToString();
                fma.txtPrezime.Text = dgvPatients.CurrentRow.Cells["LastName"].Value?.ToString();
                fma.txtJMBG.Text = dgvPatients.CurrentRow.Cells["NationalID"].Value?.ToString();
                fma.txtFon.Text = dgvPatients.CurrentRow.Cells["Phone"].Value?.ToString();
                fma.txtEmail.Text = dgvPatients.CurrentRow.Cells["Email"].Value?.ToString();

                // ComboBox i DateTimePicker
                fma.cmbPol.Text = dgvPatients.CurrentRow.Cells["Gender"].Value?.ToString();

                if (dgvPatients.CurrentRow.Cells["BirthDate"].Value != DBNull.Value)
                {
                    fma.dtDatum.Value = Convert.ToDateTime(dgvPatients.CurrentRow.Cells["BirthDate"].Value);
                }

                // Prikazivanje forme preko MainClass (Blur efekt)
                MainClass.BlurBackground(fma);

                // Osvežavanje tabele nakon izmene
                LoadData();
            }

            // --- 3. DELETE LOGIKA (dgvDELETE) ---
            if (dgvPatients.Columns[e.ColumnIndex].Name == "dgvDELETE")
            {
                // Konfiguracija dijaloga
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                // Prikaz pitanja (koristimo direktno Show metodu)
                DialogResult result = guna2MessageDialog1.Show("Are you sure you want to delete this patient?");

                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvPatients.CurrentRow.Cells["PatientID"].Value);
                    string qry = "DELETE FROM Patients WHERE PatientID = @id";

                    try
                    {
                        if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();

                        SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        // Potvrda o uspehu
                        guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                        guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                        guna2MessageDialog1.Show("Patient deleted successfully.");

                        // Ponovno učitavanje podataka
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database Error: " + ex.Message);
                    }
                    finally
                    {
                        MainClass.con.Close();
                    }
                }
            }
        }
    }
}

