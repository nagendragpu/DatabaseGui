using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace MySQL_Sim
{
    public partial class frmAgg : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;
        public frmAgg()
        {
            InitializeComponent();
        }

        private void frmAgg_Load(object sender, EventArgs e)
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
                    ComboBox3.Items.Clear();
                    comboBox4.Items.Clear();
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
                        ComboBox3.Items.Add(rs.GetValue(0).ToString());
                        comboBox4.Items.Add(rs.GetValue(0).ToString());
                        
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

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                //TextBox2.ReadOnly = false;
                //TextBox2.Focus();
                comboBox4.Enabled = true;
            }
            else
            {
                //TextBox2.ReadOnly = true;
                //TextBox2.Focus();
                comboBox4.Enabled = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                TextBox3.ReadOnly = false;
                TextBox3.Focus();
                
            }
            else
            {
                TextBox3.ReadOnly = true;
                //TextBox3.Focus();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
                string qry="";
                qry = qry+ "select " + ComboBox2.SelectedItem.ToString() + "(" + ComboBox3.SelectedItem.ToString() + ") from " + ComboBox1.SelectedItem.ToString();
                if (CheckBox1.Checked == true/*&& TextBox2.Text != ""*/)
                {
                    qry = qry + " group by " + comboBox4.SelectedItem.ToString(); 
                        //TextBox2.Text;
                }
                if (CheckBox2.Checked == true /*&& TextBox3.Text != ""*/)
                {
                    qry = qry + " having " + TextBox3.Text;
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
               qry = "";
               cn.Close();  

            }
            catch
            {
                MessageBox.Show("Error in fetching Data from Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
