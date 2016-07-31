using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SpeechLib;

namespace MySQL_Sim
{
    public partial class frmCreateDatabase : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        public frmCreateDatabase()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";

                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "create database " + textBox2.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox1.Text = "create database " + textBox2.Text;
                    MessageBox.Show("Database Created Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                catch
                {
                    MessageBox.Show("Error in Creating Database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter Database Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
        }

        
    }
}
