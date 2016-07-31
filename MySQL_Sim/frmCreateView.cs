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
    public partial class frmCreateView : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;
        public frmCreateView()
        {
            InitializeComponent();
        }

        private void frmCreateView_Load(object sender, EventArgs e)
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
                    ComboBox1.Items.Add(rs.GetValue(0).ToString());
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox1.SelectedIndex != -1)
                {
                    CheckedListBox1.Items.Clear();
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox1.SelectedItem.ToString();
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        CheckedListBox1.Items.Add(rs.GetValue(0).ToString());
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
            if(TextBox1.Text == "")
            {
                MessageBox.Show("Please enter View Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CheckedListBox1.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Please select Fields to Create View", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string str = "";
                int i = 0;
                str = "create view " + TextBox1.Text + " as select ";
                for(i = 0;i<CheckedListBox1.CheckedItems.Count;i++)
                {
                    str = str + CheckedListBox1.CheckedItems[i].ToString() + ",";
                }
                int plc;
                plc = str.LastIndexOf(",");
                str = str.Remove(plc, 1);
                str = str + " from " + ComboBox1.Text;
                try
                {
                    cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    TextBox2.Text = str;
                    MessageBox.Show("View created Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBox1.Text = "";
                    ComboBox1.SelectedIndex = -1;
                    CheckedListBox1.Items.Clear();
                }
                catch
                {
                    MessageBox.Show("Error in Creating View", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //private void Button3_Click(object sender, EventArgs e)
        //{
        //    if (TextBox2.Text != "")
        //    {
        //        SpeechLib.SpVoice v = new SpeechLib.SpVoice();
        //        v.Speak(TextBox2.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
        //    }
        //}
    }
}
