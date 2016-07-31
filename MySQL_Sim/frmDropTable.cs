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
    public partial class frmDropTable : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;
        public frmDropTable()
        {
            InitializeComponent();
        }

        private void frmDropTable_Load(object sender, EventArgs e)
        {
            try
            {
                cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "show full tables where table_type='base table';";
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ComboBox2.Items.Add(rs.GetValue(0).ToString());
                }
                rs.Close();
                cmd.Dispose();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Error in fetching Tables", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox2.SelectedIndex != -1)
                {
                    ListBox1.Items.Clear();
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox2.SelectedItem.ToString();
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        ListBox1.Items.Add(rs.GetValue(0).ToString());
                    }
                    rs.Close();
                    cmd.Dispose();
                    cn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error in fetching Tables", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult a;
                a = MessageBox.Show("Do you wish to Drop Table ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "drop table " + ComboBox2.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox1.Text = "drop table " + ComboBox2.Text;
                    MessageBox.Show("Table Dropped successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboBox2.Items.Remove(ComboBox2.SelectedItem);
                    ListBox1.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Error in Altering Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void Button3_Click(object sender, EventArgs e)
        //{
        //    if (TextBox1.Text != "")
        //    {
        //        SpeechLib.SpVoice v = new SpeechLib.SpVoice();
        //        v.Speak(TextBox1.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
        //    }
        //}
    }
}
