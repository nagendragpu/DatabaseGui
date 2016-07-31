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
    public partial class frmCreateTable : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        int f;
        public frmCreateTable()
        {
            InitializeComponent();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            f = 1;
            Button5.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (f == 1)
            {
                ListBox1.Items.Add(TextBox2.Text + " " + ComboBox1.Text + " Primary Key");
                f = 0;
            }
            else
            {
                ListBox1.Items.Add(TextBox2.Text + " " + ComboBox1.Text);
            }
            TextBox2.Text = "";
            ComboBox1.SelectedIndex = -1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Remove(ListBox1.SelectedItem);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text == "")
            {
                MessageBox.Show("Please enter Table Name","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                TextBox1.Focus();
            }
            else if(ListBox1.Items.Count <= 0)
            {
                MessageBox.Show("Please enter Field","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                string str="";
                int i=0;
                str = "create table " + TextBox1.Text + "(";
                for(i = 0;i<ListBox1.Items.Count;i++)
                {
                    ListBox1.SelectedIndex = i;
                    str = str + ListBox1.Text + ",";
                }
                int plc=0;
                plc = str.LastIndexOf(",");
                str = str.Remove(plc, 1);
                str = str + ")";

                try
                {
                    cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox3.Text = str;
                    MessageBox.Show("Table created Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBox1.Text = "";
                    ListBox1.Items.Clear();
                }
                catch
                {
                    MessageBox.Show("Error in creating Tables","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != "")
            {
                SpeechLib.SpVoice v = new SpeechLib.SpVoice();
                v.Speak(TextBox3.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void frmCreateTable_Load(object sender, EventArgs e)
        {
            f = 0;
        }
    }
}
