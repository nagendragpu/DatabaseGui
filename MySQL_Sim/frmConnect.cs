using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySQL_Sim
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                MessageBox.Show("Please Enter Server Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBox1.Focus();
            }
            else if (TextBox2.Text == "")
            {
                MessageBox.Show("Please Enter Database Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBox2.Focus();
            }
            else if (TextBox3.Text == "")
            {
                MessageBox.Show("Please Enter Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBox3.Focus();
            }
            else if (TextBox4.Text == "")
            {
                MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBox4.Focus();
            }
            else
            {
                Class1.ServerName = TextBox1.Text;
                Class1.DatabaseName = TextBox2.Text;
                Class1.Username = TextBox3.Text;
                Class1.Password = TextBox4.Text;
                this.Close();
            }
        }
    }
}
