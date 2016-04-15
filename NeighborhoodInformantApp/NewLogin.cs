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
    public partial class NewLogin : Form
    {
        bool forgotPassword;
        public NewLogin(bool forgotPassword=false)
        {
            InitializeComponent();
            this.forgotPassword = forgotPassword;

            if(forgotPassword)
            {
                label2.Visible = false;
                label3.Visible = false;
                tb_password.Visible = false;
                tb_password2.Visible = false;
                btn_validate.Visible = true;
                btn_createLogin.Visible = false;
            }
        }

        public User user = new User();
        private void btn_createLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_userName.Text))
            {
                MessageBox.Show("username is empty.");
                return;
            }
            if (string.IsNullOrEmpty(tb_password.Text))
            {
                MessageBox.Show("password is empty.");
                return;
            }
            if (string.IsNullOrEmpty(tb_password2.Text))
            {
                MessageBox.Show("Repeated Password is empty.");
                return;
            }
            if (tb_password.Text != tb_password2.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }
            if(cb_sq.SelectedIndex < 0)
            {
                MessageBox.Show("Security question not selected");
                return;
            }
            if (string.IsNullOrEmpty(tb_sa.Text))
            {
                MessageBox.Show("Security answer is empty.");
                return;
            }
            // validate other stuff here
            if(DataMgr.DataMgr.IsUserExists(tb_userName.Text))
            {
                MessageBox.Show("UserName already exists.");
                return;
            }
            // add condition here if everything is fine

            DataMgr.DataMgr.CreateNewUser(tb_userName.Text, tb_password.Text, cb_sq.SelectedItem.ToString(), tb_sa.Text);

            this.user.UserName = tb_userName.Text;
            this.Close();
        }

        private void btn_validate_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(tb_userName.Text))
            {
                MessageBox.Show("username is empty.");
                return;
            }
            if (cb_sq.SelectedIndex < 0)
            {
                MessageBox.Show("Security question not selected");
                return;
            }
            if (string.IsNullOrEmpty(tb_sa.Text))
            {
                MessageBox.Show("Security answer is empty.");
                return;
            }
            // check if user in db
            if (!DataMgr.DataMgr.IsUserExists(tb_userName.Text))
            {
                MessageBox.Show("Username does not exist.");
                return;
            }
            // check if sq and sa match
            if (!DataMgr.DataMgr.Validate(tb_userName.Text, cb_sq.SelectedItem.ToString(), tb_sa.Text))
            {
                MessageBox.Show("Security question and answer do not match.");
                return;
            }

            btn_save.Visible = true;
            tb_password2.Visible = true;
            tb_password.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            cb_sq.Visible = false;
            tb_sa.Visible = false;
            tb_userName.Enabled = false;
            btn_validate.Visible = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_password.Text))
            {
                MessageBox.Show("password is empty.");
                return;
            }
            if (string.IsNullOrEmpty(tb_password2.Text))
            {
                MessageBox.Show("Repeated Password is empty.");
                return;
            }
            if (tb_password.Text != tb_password2.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            DataMgr.DataMgr.UpdatePassWord(tb_userName.Text, tb_password.Text);
            this.user.UserName = tb_userName.Text;
            this.Close();
        }
    }
}
