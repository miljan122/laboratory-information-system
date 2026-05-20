using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Laboratory_Information_System
{
    internal class MainClass
    {
        public static readonly string con_string = "Data Source=DESKTOP-FUSGD8B\\SQLEXPRESS;Initial Catalog=LabLink_DB;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection con = new SqlConnection(con_string);

        public static int currentUserId;

      
        public static bool IsListenerRunning = false;

        public static void StartDeviceListener(int port)
        {
            
            if (IsListenerRunning) return;

            Task.Run(() =>
            {
                TcpListener server = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);

            
                    server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    server.Start();
                    IsListenerRunning = true;

                
                    MessageBox.Show("SYSTEM READY! Listening to the device on the port: " + port);

                    while (true)
                    {
                        
                        TcpClient client = server.AcceptTcpClient();

                        Task.Run(() => {
                            try
                            {
                                using (NetworkStream stream = client.GetStream())
                                {
                                    byte[] buffer = new byte[4096];
                                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                                    if (bytesRead > 0)
                                    {
                                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                                        ParseHL7Message(message);
                                    }
                                }
                            }
                            catch { }
                            finally { client.Close(); }
                        });
                    }
                }
                catch (Exception ex)
                {
                    IsListenerRunning = false;
                    // Ako pukne, izbaciće ti tačno zašto (npr. port zauzet)
                    MessageBox.Show("TCP Error: " + ex.Message);
                }
            });
        }



        public static void StartAllSerialPorts()
        {
            // Uzimamo spisak svih portova koje Windows trenutno vidi (COM1, COM2...)
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                // Za svaki pronađeni port pokrećemo listener u posebnom Task-u
                Task.Run(() =>
                {
                    try
                    {
                        SerialPort serial = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);

                        serial.DataReceived += (sender, e) => {
                            try
                            {
                                string data = serial.ReadExisting();
                                if (!string.IsNullOrEmpty(data))
                                {
                                    ParseHL7Message(data);
                                }
                            }
                            catch { /* Greška pri čitanju */ }
                        };

                        if (!serial.IsOpen) serial.Open();
                        Console.WriteLine($"Listening on port: {port}");
                    }
                    catch (Exception ex)
                    {
                        // Verovatno je port već zauzet od strane nekog drugog programa
                        Console.WriteLine($"Port {port} is already in use: {ex.Message}");
                    }
                });
            }
        }

        public static void StartSerialListener(string portName)
        {
            // Kreiramo SerialPort objekat sa parametrima sa slike (9600, 8, None, 1)
            SerialPort serial = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);

            // Događaj koji se okida čim mašina (Hercules) pošalje bilo šta
            serial.DataReceived += (sender, e) => {
                try
                {
                    string data = serial.ReadExisting();
                    // Pozivamo tvoj već gotov HL7 parser da upiše u bazu
                    ParseHL7Message(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Serial port error: " + ex.Message);
                }
            };

            try
            {
                if (!serial.IsOpen) serial.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open port: Port is already in use.: " + ex.Message);
            }
        }


        private static void ParseHL7Message(string msg)
        {
            try
            {
                string[] segments = msg.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string barcode = "";
                string machineName = "";

                List<string> testovi = new List<string>();
                List<string> vrednosti = new List<string>();
                string jedinica = "";
                string opseg = "";

                foreach (string seg in segments)
                {
                    string[] f = seg.Split('|');

                    if (f[0] == "MSH" && f.Length > 2) machineName = f[2].Trim();
                    if (f[0] == "PID" && f.Length > 4) barcode = f[4].Trim();

                    if (f[0] == "OBX" && f.Length > 5) // Smanjio sam uslov na 5 jer su nekad poruke kraće
                    {
                        // --- POPRAVKA ZA TEST NAME ---
                        string fullTest = f.Length > 4 ? f[4] : "Unknown";
                        string naziv = "";

                        // Ako mašina šalje GLU^Glucose, uzimamo Glucose. Ako šalje samo Glucose, uzimamo to.
                        if (fullTest.Contains("^"))
                        {
                            string[] parts = fullTest.Split('^');
                            naziv = parts.Length > 1 ? parts[1] : parts[0];
                        }
                        else
                        {
                            naziv = fullTest;
                        }

                        // Ako je i dalje prazno (npr. mašina poslala ||), stavi bar nešto da se vidi u tabeli
                        if (string.IsNullOrWhiteSpace(naziv)) naziv = "Test-" + f[3];

                        testovi.Add(naziv);
                        vrednosti.Add(f.Length > 5 ? f[5] : "0");

                        if (f.Length > 6) jedinica = f[6];
                        if (f.Length > 7) opseg = f[7];
                    }
                }

                if (!string.IsNullOrEmpty(barcode) && testovi.Count > 0)
                {
                    string finalniTestovi = string.Join(", ", testovi);
                    string finalneVrednosti = string.Join(", ", vrednosti);

                    string sql = $@"
            INSERT INTO LabResults (PatientID, AnalyzerID, TestName, Value, Unit, RefRange, RawMessage, ResultDateTime, ApiSyncStatus)
            SELECT 
                (SELECT TOP 1 PatientID FROM Patients WHERE Barcode = '{barcode}'),
                (SELECT TOP 1 AnalyzerID FROM Analyzers2 WHERE ModelName LIKE '%{machineName}%' OR ModelName LIKE '%BK-280%'), 
                '{finalniTestovi}', 
                '{finalneVrednosti}', 
                '{jedinica}', 
                '{opseg}', 
                '{msg.Replace("'", "''")}', 
                GETDATE(), 
                'Pending'
            WHERE EXISTS (SELECT 1 FROM Patients WHERE Barcode = '{barcode}')";

                    ExecuteQuery(sql);
                }
            }
            catch (Exception ex) { LogSystemEvent("HL7", "Error", "Fail", ex.Message); }
        }









        public static void LogSystemEvent(string source, string eventType, string status, string message, string raw = "")
        {
            try
            {
                string qry = @"INSERT INTO SystemLogs (Source, EventType, Status, Message, RawData, UserID, LogTime) 
                               VALUES (@src, @type, @status, @msg, @raw, @uid, GETDATE())";

                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@src", source);
                cmd.Parameters.AddWithValue("@type", eventType);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@msg", message);
                cmd.Parameters.AddWithValue("@raw", raw);
                cmd.Parameters.AddWithValue("@uid", currentUserId);

                cmd.ExecuteNonQuery();
            }
            catch { }
            finally { con.Close(); }
        }


        public static void LoadData(string qry, DataGridView dgv)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { con.Close(); }
        }
        // VERZIJA 2: Za AnalyserList (gde šalješ 3 parametra sa DataTable)
        public static void LoadData(string qry, DataGridView dgv, DataTable dt = null)
        {
            try
            {
                // Ako nismo prosledili DataTable, napravi novi lokalni
                if (dt == null)
                {
                    dt = new DataTable();
                }

                SqlDataAdapter da = new SqlDataAdapter(qry, con);

                // SqlDataAdapter.Fill ne zahteva Clear ako je dt nov, 
                // ali ako je prosleđen postojeći, čistimo ga.
                if (dt.Rows.Count > 0) dt.Clear();

                da.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading error: " + ex.Message);
            }
        }

        // METODA ZA TCP TEST (Fali ti u ucAnalyzerCard)
        public static bool CheckTcpConnection(string ip, int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(ip, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
                    if (!success) return false;
                    client.EndConnect(result);
                    return true;
                }
            }
            catch { return false; }
        }

        // METODA ZA RS232 TEST (Fali ti u ucAnalyzerCard)
        public static bool CheckSerialConnection(string portName)
        {
            try
            {
                using (SerialPort serial = new SerialPort(portName))
                {
                    serial.Open();
                    bool isOpen = serial.IsOpen;
                    serial.Close();
                    return isOpen;
                }
            }
            catch { return false; }
        }
    



    public static void BlurBackground(Form Model)
        {
            Form b = new Form();
            using (Model)
            {
                b.StartPosition = FormStartPosition.Manual;
                b.FormBorderStyle = FormBorderStyle.None;
                b.Opacity = 0.5d;
                b.BackColor = Color.Black;
                b.Size = frmMain.Instance.Size;
                b.Location = frmMain.Instance.Location;
                b.ShowInTaskbar = false;
                b.Show();
                Model.Owner = b;
                Model.ShowDialog(b);
                b.Dispose();


            }
        }


        public static DataTable GetDataTable(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                // Koristi tvoj postojeći Connection String iz MainClass
                SqlConnection con = new SqlConnection(con_string);
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public static int ExecuteQuery(string qry)
        {
            int res = 0;
            // 'using' blok automatski zatvara i uništava konekciju čak i ako pukne greška
            using (SqlConnection con = new SqlConnection(con_string))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in ExecuteQuery:\n" + ex.Message);
                }
                // Ovde ne moraš pisati con.Close() jer 'using' to radi sam
            }
            return res;
        }




        public static bool isValidUser(string user, string pass)
        {
            bool isValid = false;
            // Bolje je koristiti SELECT * da bi izvukli i UserID
            string qry = @"SELECT * FROM Users WHERE Username = @user AND Password = @pass";

            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                isValid = true;

                // OVO TI JE FALILO: Program sada pamti ID onoga ko se ulogovao!
                currentUserId = Convert.ToInt32(dt.Rows[0]["UserID"]);

                USER = dt.Rows[0]["Username"].ToString();
            }

            return isValid;
        }


        public static string user;

        public static string USER
        {
            get { return user; }

            private set { user = value; }
        }

    }
   

}

