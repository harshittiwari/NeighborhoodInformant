using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeighborhoodInformantApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public bool IsLoginSuccessful = false;
        public bool IsNewLoginReq = false;
        public bool IsForgotPassword = false;
        public User user = new User();

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_userName.Text)) 
            {
                MessageBox.Show("username is empty.");
                return;
            }
            if(string.IsNullOrEmpty(tb_Password.Text))
            {
                MessageBox.Show("password is empty.");
                return;
            }
            // verify login credentials here
            IsLoginSuccessful = DataMgr.DataMgr.CheckLogin(tb_userName.Text, tb_Password.Text);

            if (IsLoginSuccessful)
            {
                user.UserName = tb_userName.Text;
                this.Close();
            }
            else
                MessageBox.Show("Wrong username or password.\n Please try again.");
        }

        private void btn_newLogin_Click(object sender, EventArgs e)
        {
            IsNewLoginReq = true;
            this.Close();
        }

        private void btn_forgotPassword_Click(object sender, EventArgs e)
        {
            IsForgotPassword = true;
            this.Close();
        }
    }
}
