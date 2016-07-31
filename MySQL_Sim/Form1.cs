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
    public partial class Form1 : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rs;

        MySqlConnection cn1 = new MySqlConnection();
        MySqlCommand cmd1;
        DataSet ds1;
        MySqlDataAdapter adp1;
        MySqlDataReader rs1;

        public Form1()
        {
            InitializeComponent();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnect c = new frmConnect();
            c.ShowDialog();
            if (Class1.ServerName != "")
            {
                Connect_Database();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class1.ServerName = "";
            Class1.DatabaseName = "";
            Class1.Username = "";
            Class1.Password = "";
            Class1.TableName = "";
        }

        public void Connect_Database()
        {
            try
            {
                //Table
                ListBox1.Items.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.Visible = false;
                cn.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "show full tables where table_type='base table';";
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ListBox1.Items.Add(rs.GetValue(0).ToString());
                }
                rs.Close();
                cmd.Dispose();
                cn.Close();
                //View
                ListBox2.Items.Clear();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "show full tables where table_type='view';";
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ListBox2.Items.Add(rs.GetValue(0).ToString());
                }
                rs.Close();
                cmd.Dispose();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Error in connecting with MySQL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Visible = false;
            button1.Visible = false;
            Class1.ServerName = "";
            Class1.DatabaseName = "";
            Class1.Username = "";
            Class1.Password = "";
            Class1.TableName = "";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connect_Database();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = true;
                button1.Visible = true;
                Class1.TableName = ListBox1.SelectedItem.ToString();
                if (cn1.State == ConnectionState.Open)
                {
                    cn1.Close();
                }
                cn1.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn1.Open();
                cmd1 = new MySqlCommand("select * from " + Class1.TableName,cn1);
                adp1 = new MySqlDataAdapter(cmd1);
                ds1 = new DataSet();
                dataGridView1.DataSource = null;
                ds1.Tables.Add(Class1.TableName);
                adp1.Fill(ds1, Class1.TableName);
                dataGridView1.DataSource = ds1;
                dataGridView1.DataMember = Class1.TableName;
                comboBox1.Items.Clear();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "SHOW COLUMNS FROM " + ListBox1.SelectedItem.ToString();
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    comboBox1.Items.Add(rs.GetValue(0).ToString());
                }
                rs.Close();
                cmd.Dispose();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Error in Displaying Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = true;
                button1.Visible = true;
                Class1.TableName = ListBox2.SelectedItem.ToString();
                if (cn1.State == ConnectionState.Open)
                {
                    cn1.Close();
                }
                cn1.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn1.Open();
                cmd1 = new MySqlCommand("select * from " + Class1.TableName, cn1);
                adp1 = new MySqlDataAdapter(cmd1);
                ds1 = new DataSet();
                dataGridView1.DataSource = null;
                ds1.Tables.Add(Class1.TableName);
                adp1.Fill(ds1, Class1.TableName);
                dataGridView1.DataSource = ds1;
                dataGridView1.DataMember = Class1.TableName;

                comboBox1.Items.Clear();
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "SHOW COLUMNS FROM " + ListBox2.SelectedItem.ToString();
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    comboBox1.Items.Add(rs.GetValue(0).ToString());
                }
                rs.Close();
                cmd.Dispose();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("Error in Displaying Table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateTable ct = new frmCreateTable();
            ct.Show();
        }

        private void alterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlterTable at = new frmAlterTable();
            at.Show();
        }

        private void dropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDropTable dt = new frmDropTable();
            dt.Show();
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCreateDatabase cd = new frmCreateDatabase();
            cd.Show();
        }

        private void createToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmCreateView cv = new frmCreateView();
            cv.Show();
        }

        private void dropToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDropView dv = new frmDropView();
            dv.Show();
        }

        private void aggregateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgg ag = new frmAgg();
            ag.Show();
        }

        private void joinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJoins j = new frmJoins();
            j.Show();
        }

        private void subQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSub s = new frmSub();
            s.Show();
        }
        //save button
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommandBuilder cmdbui = new MySqlCommandBuilder(adp1);
            try
            {
                int i;
                i = adp1.Update(ds1,Class1.TableName);
          
                MessageBox.Show(i.ToString() + " Record Affected","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Operation Failed. Check for Primary Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This software is Developed by Nagendra,Ramesh,Sandeep,Kirti", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string fp = Application.ExecutablePath;
            fp = fp.Substring(0, fp.LastIndexOf("\\"));
            fp = fp + "\\Help.html";
            System.Diagnostics.Process.Start("" + fp);
        }

        private void dropToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (cn1.State == ConnectionState.Open)
                {
                    cn1.Close();
                }
                cn1.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn1.Open();
                cmd1 = new MySqlCommand("select * from " + Class1.TableName + " where " + comboBox1.SelectedItem.ToString() + comboBox2.SelectedItem.ToString() + textBox1.Text, cn1);
                adp1 = new MySqlDataAdapter(cmd1);
                ds1 = new DataSet();
                dataGridView1.DataSource = null;
                ds1.Tables.Add(Class1.TableName);
                adp1.Fill(ds1, Class1.TableName);
                dataGridView1.DataSource = ds1;
                dataGridView1.DataMember = Class1.TableName;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                textBox1.Text = "";
                if (cn1.State == ConnectionState.Open)
                {
                    cn1.Close();
                }
                cn1.ConnectionString = "Server=" + Class1.ServerName + ";Database=" + Class1.DatabaseName + ";Uid=" + Class1.Username + ";Pwd=" + Class1.Password + ";";
                cn1.Open();
                cmd1 = new MySqlCommand("select * from " + Class1.TableName, cn1);
                adp1 = new MySqlDataAdapter(cmd1);
                ds1 = new DataSet();
                dataGridView1.DataSource = null;
                ds1.Tables.Add(Class1.TableName);
                adp1.Fill(ds1, Class1.TableName);
                dataGridView1.DataSource = ds1;
                dataGridView1.DataMember = Class1.TableName;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmOthers oth = new frmOthers();
        //    oth.Show();
        //}

   }
}
