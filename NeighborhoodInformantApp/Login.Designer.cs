namespace NeighborhoodInformantApp
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_newLogin = new System.Windows.Forms.Button();
            this.btn_forgotPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username :";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(80, 37);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(100, 20);
            this.tb_userName.TabIndex = 1;
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(80, 63);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(100, 20);
            this.tb_Password.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password :";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(80, 90);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_newLogin
            // 
            this.btn_newLogin.Location = new System.Drawing.Point(80, 119);
            this.btn_newLogin.Name = "btn_newLogin";
            this.btn_newLogin.Size = new System.Drawing.Size(100, 23);
            this.btn_newLogin.TabIndex = 5;
            this.btn_newLogin.Text = "Create new login";
            this.btn_newLogin.UseVisualStyleBackColor = true;
            this.btn_newLogin.Click += new System.EventHandler(this.btn_newLogin_Click);
            // 
            // btn_forgotPassword
            // 
            this.btn_forgotPassword.Location = new System.Drawing.Point(80, 149);
            this.btn_forgotPassword.Name = "btn_forgotPassword";
            this.btn_forgotPassword.Size = new System.Drawing.Size(100, 23);
            this.btn_forgotPassword.TabIndex = 6;
            this.btn_forgotPassword.Text = "Forgot Password";
            this.btn_forgotPassword.UseVisualStyleBackColor = true;
            this.btn_forgotPassword.Click += new System.EventHandler(this.btn_forgotPassword_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_forgotPassword);
            this.Controls.Add(this.btn_newLogin);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_userName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_newLogin;
        private System.Windows.Forms.Button btn_forgotPassword;
    }
}