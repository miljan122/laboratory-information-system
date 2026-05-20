using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MainClass.isValidUser(txtUser.Text, txtPass.Text))
            {
                // OD OVOG TRENUTKA: MainClass.currentUserId više nije 1, 
                // nego je npr. 5 (ID onoga ko se ulogovao)

                MessageBox.Show("Welcome, " + MainClass.USER);
                this.Hide();
                frmMain ds = new frmMain();
                ds.Show();

                // AUTOMATSKI LOG: Čim uđe, upiši u bazu ko se ulogovao
                MainClass.LogSystemEvent("Security", "Login", "Success", "User logged in successfully");
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }
        }
    }
}
