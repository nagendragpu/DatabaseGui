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
    public partial class frmSub : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;
        public frmSub()
        {
            InitializeComponent();
        }

        private void frmSub_Load(object sender, EventArgs e)
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
                    ComboBox3.Items.Add(rs.GetValue(0).ToString());
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
                    ComboBox2.Items.Clear();
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";

                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox1.SelectedItem.ToString();
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        ComboBox2.Items.Add(rs.GetValue(0).ToString());
                    }
                    rs.Close();
                    cmd.Dispose();
                    cn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error in fetching Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox3.SelectedIndex != -1)
                {
                    ComboBox4.Items.Clear();
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";

                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "SHOW COLUMNS FROM " + ComboBox3.SelectedItem.ToString();
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        ComboBox4.Items.Add(rs.GetValue(0).ToString());
                    }
                    rs.Close();
                    cmd.Dispose();
                    cn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error in fetching Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";

                cn.Open();
                string qry = "";
                if (RadioButton1.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " IN (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                else if (RadioButton2.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " = ANY (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                else if (RadioButton3.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " = ALL (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                else if (RadioButton4.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " = (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                else if (RadioButton5.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " > (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                else if (RadioButton6.Checked == true)
                {
                    qry = "select * from " + ComboBox1.SelectedItem.ToString() + " where " + ComboBox2.SelectedItem.ToString() + " < (select " + ComboBox4.SelectedItem.ToString() + " from " + ComboBox3.SelectedItem.ToString();
                }
                if (CheckBox1.Checked == true)
                {
                    qry = qry + " where " + TextBox2.Text + ")";
                }
                else
                {
                    qry = qry + ")";
                }
                MySqlCommand cmd1 = new MySqlCommand(qry, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                DataGridView1.DataSource = null;
                ds1.Tables.Add(ComboBox1.SelectedItem.ToString());
                adp.Fill(ds1, ComboBox1.SelectedItem.ToString());
                DataGridView1.DataSource = ds1;
                DataGridView1.DataMember = ComboBox1.SelectedItem.ToString();
                TextBox1.Text = qry;
            }
            catch
            {
                MessageBox.Show("Error in fetching Data from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                TextBox2.ReadOnly = false;
                TextBox2.Focus();
            }
            else
            {
                TextBox2.ReadOnly = true;
                TextBox2.Focus();
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
