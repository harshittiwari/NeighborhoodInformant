using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NeighborhoodInformantApp
{
    public partial class SendEmail : Form
    {
        private string content;
        public SendEmail(string c)
        {
            InitializeComponent();
            this.content = c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                button2.Enabled = false;
                string msg = "";
                if (string.IsNullOrEmpty(tb_message.Text))
                    msg = content;
                else
                    msg = tb_message.Text + "\n\n" + this.content;
                if (!DataMgr.DataMgr.SendMail(tb_senderEmail.Text, tb_receiver.Text, tb_password.Text, "Houses you might be interested In", msg, lbl_FileName.Text))
                {
                    string alertMsg = "Message Sending failed."
                                    + "\n Following are the common issues in sending the emails:"
                                    + "\n  1. Problem in network connection"
                                    + "\n  2. User account not configured for low security applications to access"
                                    + "\n\n For more info, see Help";
                    MessageBox.Show(alertMsg);
                }
                else
                {
                    MessageBox.Show("Email Sent");
                }
                button2.Enabled = true;
            }
        }

        private bool IsValidate()
        {
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            if (string.IsNullOrEmpty(tb_senderEmail.Text))
            {
                MessageBox.Show("Sender email is empty");
                return false;
            }
            else if (string.IsNullOrEmpty(tb_password.Text))
            {
                MessageBox.Show("Password is empty");
                return false;
            }
            else if (string.IsNullOrEmpty(tb_receiver.Text))
            {
                MessageBox.Show("Receiver email is empty");
                return false;
            }
            else if (!Regex.IsMatch(tb_senderEmail.Text, emailPattern))
            {
                MessageBox.Show("Sender email is not a valid email address");
                return false;
            }
            else if (!tb_senderEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Sender email should be a gmail account");
                return false;
            }
            else if (!Regex.IsMatch(tb_receiver.Text, emailPattern))
            {
                MessageBox.Show("Receiver email is not a valid email address");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); 
            if (result == DialogResult.OK) 
            {
                lbl_FileName.Text = openFileDialog1.FileName;
            }
        }
    }
}
