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
    public partial class frmOthers : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        public frmOthers()
        {
            InitializeComponent();
        }

        private void frmOthers_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult a;
                a = MessageBox.Show("Do you wish to Execute Query ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = textBox1.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();
                    MessageBox.Show("Query Executed successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Execution : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SpeechLib.SpVoice v = new SpeechLib.SpVoice();
                v.Speak(textBox1.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }
    }
}
