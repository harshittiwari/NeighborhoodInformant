namespace NeighborhoodInformantApp
{
    partial class NewLogin
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_password2 = new System.Windows.Forms.TextBox();
            this.btn_createLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_sq = new System.Windows.Forms.ComboBox();
            this.tb_sa = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_validate = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Re-enter Password";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(122, 29);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(100, 20);
            this.tb_userName.TabIndex = 3;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(122, 55);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(100, 20);
            this.tb_password.TabIndex = 4;
            // 
            // tb_password2
            // 
            this.tb_password2.Location = new System.Drawing.Point(122, 81);
            this.tb_password2.Name = "tb_password2";
            this.tb_password2.Size = new System.Drawing.Size(100, 20);
            this.tb_password2.TabIndex = 5;
            // 
            // btn_createLogin
            // 
            this.btn_createLogin.Location = new System.Drawing.Point(122, 160);
            this.btn_createLogin.Name = "btn_createLogin";
            this.btn_createLogin.Size = new System.Drawing.Size(75, 23);
            this.btn_createLogin.TabIndex = 6;
            this.btn_createLogin.Text = "Create Login";
            this.btn_createLogin.UseVisualStyleBackColor = true;
            this.btn_createLogin.Click += new System.EventHandler(this.btn_createLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Security Question";
            // 
            // cb_sq
            // 
            this.cb_sq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_sq.FormattingEnabled = true;
            this.cb_sq.Items.AddRange(new object[] {
            "What is the name of your least favorite child?",
            "In what year did you abandon your dreams?",
            "At what age did your childhood pet run away?",
            "What was the name of your favorite unpaid internship?",
            "What is your ex-wife’s newest last name?",
            "What sports team do you fetishize to avoid meaningful discussion with others?",
            "What is the name of your favorite canceled TV show?",
            "When did you stop trying?",
            "What is the first and last name of your first boyfriend or girlfriend?",
            "Which phone number do you remember most from your childhood?",
            "What was your favorite place to visit as a child?",
            "Who is your favorite actor, musician, or artist?",
            "What is the name of your favorite pet?",
            "In what city were you born?",
            "What high school did you attend?",
            "What is the name of your first school?",
            "What is your favorite movie?",
            "What is your mother\'s maiden name?",
            "What street did you grow up on?",
            "What was the make of your first car?",
            "When is your anniversary?",
            "What is your favorite color?",
            "What is your father\'s middle name?",
            "What is the name of your first grade teacher?",
            "What was your high school mascot?",
            "Which is your favorite web browser?"});
            this.cb_sq.Location = new System.Drawing.Point(122, 107);
            this.cb_sq.Name = "cb_sq";
            this.cb_sq.Size = new System.Drawing.Size(381, 21);
            this.cb_sq.TabIndex = 8;
            // 
            // tb_sa
            // 
            this.tb_sa.Location = new System.Drawing.Point(122, 134);
            this.tb_sa.Name = "tb_sa";
            this.tb_sa.Size = new System.Drawing.Size(100, 20);
            this.tb_sa.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Security Answer";
            // 
            // btn_validate
            // 
            this.btn_validate.Location = new System.Drawing.Point(41, 190);
            this.btn_validate.Name = "btn_validate";
            this.btn_validate.Size = new System.Drawing.Size(75, 23);
            this.btn_validate.TabIndex = 11;
            this.btn_validate.Text = "Validate";
            this.btn_validate.UseVisualStyleBackColor = true;
            this.btn_validate.Visible = false;
            this.btn_validate.Click += new System.EventHandler(this.btn_validate_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(122, 190);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(113, 23);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "Save new password";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // NewLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 261);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_validate);
            this.Controls.Add(this.tb_sa);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_sq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_createLogin);
            this.Controls.Add(this.tb_password2);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewLogin";
            this.Text = "NewLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_password2;
        private System.Windows.Forms.Button btn_createLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_sq;
        private System.Windows.Forms.TextBox tb_sa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_validate;
        private System.Windows.Forms.Button btn_save;
    }
}