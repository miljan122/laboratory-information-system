using Guna.UI2.WinForms;
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
    public partial class AddAnalysers : Form
    {
        public AddAnalysers()
        {
            InitializeComponent();
        }
        public int id = 0;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // 1. Validacija
            if (string.IsNullOrWhiteSpace(txtModel.Text) || string.IsNullOrWhiteSpace(cmbType.Text))
            {
                MessageBox.Show("Please enter model and connection type.");
                return;
            }

            try
            {
                if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();

                // 2. Definisanje upita
                string qry = (id == 0)
                    ? @"INSERT INTO Analyzers2 (ModelName, Manufacturer, ConnectionType, IPAddress, PortNumber, ComPort, BaudRate, Status) 
                VALUES (@model, @manuf, @type, @ip, @port, @com, @baud, 'Offline')"
                    : @"UPDATE Analyzers2 SET ModelName=@model, Manufacturer=@manuf, ConnectionType=@type, 
                IPAddress=@ip, PortNumber=@port, ComPort=@com, BaudRate=@baud WHERE AnalyzerID=@id";

                using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
                {
                    // Ovi parametri MORAJU biti ovde, izvan svih IF blokova da bi se uvek poslali
                    if (id != 0) cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@model", txtModel.Text.Trim());
                    cmd.Parameters.AddWithValue("@manuf", txtManufacturer.Text.Trim()); // OVO JE KLJUČ
                    cmd.Parameters.AddWithValue("@type", cmbType.Text);

                    // 3. Logika za specifična polja (TCP vs RS232)
                    if (cmbType.Text == "TCP/IP")
                    {
                        cmd.Parameters.AddWithValue("@ip", txtIP.Text.Trim());
                        cmd.Parameters.AddWithValue("@port", int.TryParse(txtPort.Text, out int p) ? p : (object)DBNull.Value);

                        // Moramo poslati NULL za RS232 polja da bi INSERT radio
                        cmd.Parameters.AddWithValue("@com", DBNull.Value);
                        cmd.Parameters.AddWithValue("@baud", DBNull.Value);
                    }
                    else // RS232
                    {
                        // Moramo poslati NULL za TCP polja
                        cmd.Parameters.AddWithValue("@ip", DBNull.Value);
                        cmd.Parameters.AddWithValue("@port", DBNull.Value);

                        cmd.Parameters.AddWithValue("@com", cmbCOM.Text);
                        cmd.Parameters.AddWithValue("@baud", int.TryParse(cmbBaud.Text, out int b) ? b : (object)DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                    // Nakon cmd.ExecuteNonQuery() dodaj:
                    MainClass.LogSystemEvent("Settings", "Analyzer Setup", "Success", $"Device added or modified: {txtModel.Text}");

                    MessageBox.Show("Device successfully saved!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving: " + ex.Message);
            }
            finally
            {
                MainClass.con.Close();
            }
        }

            private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFieldVisibility();
        }
        private void UpdateFieldVisibility()
        {
            if (cmbType.SelectedItem == null) return;

            string selectedType = cmbType.SelectedItem.ToString();
            bool isTcp = (selectedType == "TCP/IP");
            bool isRs232 = (selectedType == "RS232");

            // OVO UVEK MORA BITI TRUE
            txtManufacturer.Visible = true;
            lblManufacturer.Visible = true; // Proveri kako ti se zove labela za Manufacturer

            // TCP/IP polja
            txtIP.Visible = isTcp;
            txtPort.Visible = isTcp;
            lblIP.Visible = isTcp;
            lblPort.Visible = isTcp;

            // RS232 polja
            cmbCOM.Visible = isRs232;
            cmbBaud.Visible = isRs232;
            lblCOM.Visible = isRs232;
            lblBaud.Visible = isRs232;
        }


        private void AddAnalysers_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. POPUNJAVANJE COM PORTOVA
                cmbCOM.Items.Clear();

                // Dodajemo standardne portove ručno (da bi mogao da biraš i one koji trenutno nisu povezani)
                string[] standardPorts = { "COM1", "COM2", "COM3", "COM4", "COM5" };
                foreach (string p in standardPorts)
                {
                    cmbCOM.Items.Add(p);
                }

                // Dodajemo portove koje sistem stvarno detektuje (ako su drugačiji od standardnih)
                string[] detectedPorts = System.IO.Ports.SerialPort.GetPortNames();
                foreach (string port in detectedPorts)
                {
                    if (!cmbCOM.Items.Contains(port))
                    {
                        cmbCOM.Items.Add(port);
                    }
                }

                // Postavi podrazumevanu selekciju ako ima stavki
                if (cmbCOM.Items.Count > 0) cmbCOM.SelectedIndex = 0;


                // 2. POPUNJAVANJE BAUD RATE (Standardne brzine komunikacije)
                cmbBaud.Items.Clear();
                string[] baudRates = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
                cmbBaud.Items.AddRange(baudRates);

                // Postavi 9600 kao najčešći standard za medicinske aparate
                cmbBaud.Text = "9600";


                // 3. INICIJALNA VIDLJIVOST POLJA
                // Pozivamo metodu da sakrije/prikaže polja zavisno od toga šta je izabrano u cmbType
                UpdateFieldVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ports: No serial devices found." + ex.Message);
            }

        }

        private void cmbType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateFieldVisibility();
        }
    }
}
