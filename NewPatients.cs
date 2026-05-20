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
    public partial class NewPatients : Form
    {
        public NewPatients()
        {
            InitializeComponent();
        }
        public int id = 0;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "" || txtBarcode.Text == "")
            {
                guna2MessageDialog1.Show("Please enter the barcode and the patient’s name.");
                return;
            }

            try
            {
                string qry = "";

                // Proveravamo da li je konekcija otvorena pre nego što krenemo
                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }

                if (id == 0) // AKO JE ID 0 -> RADIMO INSERT
                {
                    qry = @"INSERT INTO Patients (Barcode, FirstName, LastName, NationalID, Gender, BirthDate, Phone, Email) 
                    VALUES (@Barcode, @FirstName, @LastName, @NationalID, @Gender, @BirthDate, @Phone, @Email)";
                }
                else // AKO ID NIJE 0 -> RADIMO UPDATE
                {
                    qry = @"UPDATE Patients SET 
                    Barcode = @Barcode, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    NationalID = @NationalID, 
                    Gender = @Gender, 
                    BirthDate = @BirthDate, 
                    Phone = @Phone, 
                    Email = @Email 
                    WHERE PatientID = @id";
                }

                SqlCommand cmd = new SqlCommand(qry, MainClass.con);

                // Dodavanje parametara (ID je potreban samo za Update, ali ne smeta ni kod Inserta)
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                cmd.Parameters.AddWithValue("@FirstName", txtIme.Text);
                cmd.Parameters.AddWithValue("@LastName", txtPrezime.Text);
                cmd.Parameters.AddWithValue("@NationalID", txtJMBG.Text);
                cmd.Parameters.AddWithValue("@Gender", cmbPol.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dtDatum.Value); // Koristi Picker, ne DateTime.Now
                cmd.Parameters.AddWithValue("@Phone", txtFon.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                // Izvršavanje upita
                cmd.ExecuteNonQuery();

                guna2MessageDialog1.Show("Successfully saved.");

                // Resetujemo ID i zatvaramo formu
                id = 0;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bag in Database: " + ex.Message);
            }
            finally
            {
                // Uvek zatvori konekciju na kraju
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }
            }
        }

        private void NewPatients_Load(object sender, EventArgs e)
        {
            if (id == 0)
            {
                // Generiše barkod npr: 2605121045 (Godina, mesec, dan, sat, minut)
                txtBarcode.Text = DateTime.Now.ToString("yyMMddHHmm");
            }
        }
    }
}
    

