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
    public partial class frmAlterTable : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;
        public frmAlterTable()
        {
            InitializeComponent();
        }

        private void frmAlterTable_Load(object sender, EventArgs e)
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult a;
                a = MessageBox.Show("Do you wish to Alter Table ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "alter table " + ComboBox2.Text + " add " + TextBox2.Text + " " + ComboBox1.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox1.Text = "alter table " + ComboBox2.Text + " add " + TextBox2.Text + " " + ComboBox1.Text;
                    MessageBox.Show("Table altered successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error in Altering Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult a;
                a = MessageBox.Show("Do you wish to Alter Table ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "alter table " + ComboBox2.Text + " drop " + TextBox2.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox1.Text = "alter table " + ComboBox2.Text + " add " + TextBox2.Text + " " + ComboBox1.Text;
                    MessageBox.Show("Table altered successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error in Altering Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                SpeechLib.SpVoice v = new SpeechLib.SpVoice();
                v.Speak(TextBox1.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }
    }
}
