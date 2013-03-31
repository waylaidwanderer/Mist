namespace MistClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.updatechecker = new System.ComponentModel.BackgroundWorker();
            this.text_username = new MetroFramework.Controls.MetroTextBox();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.text_password = new MetroFramework.Controls.MetroTextBox();
            this.text_api = new MetroFramework.Controls.MetroTextBox();
            this.check_remember = new MetroFramework.Controls.MetroCheckBox();
            this.button_cancel = new MetroFramework.Controls.MetroButton();
            this.button_login = new MetroFramework.Controls.MetroButton();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.label4 = new MetroFramework.Controls.MetroLabel();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.label_status = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // updatechecker
            // 
            this.updatechecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updatechecker_DoWork);
            // 
            // text_username
            // 
            this.text_username.CustomBackground = false;
            this.text_username.CustomForeColor = false;
            this.text_username.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.text_username.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.text_username.Location = new System.Drawing.Point(155, 160);
            this.text_username.MaxLength = 32767;
            this.text_username.Multiline = false;
            this.text_username.Name = "text_username";
            this.text_username.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_username.SelectedText = "";
            this.text_username.Size = new System.Drawing.Size(128, 20);
            this.text_username.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_username.StyleManager = this.metroStyleManager1;
            this.text_username.TabIndex = 0;
            this.text_username.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.text_username.UseStyleColors = false;
            this.text_username.UseSystemPasswordChar = false;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.OwnerUserControl = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // text_password
            // 
            this.text_password.CustomBackground = false;
            this.text_password.CustomForeColor = false;
            this.text_password.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.text_password.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.text_password.Location = new System.Drawing.Point(155, 186);
            this.text_password.MaxLength = 32767;
            this.text_password.Multiline = false;
            this.text_password.Name = "text_password";
            this.text_password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_password.SelectedText = "";
            this.text_password.Size = new System.Drawing.Size(128, 20);
            this.text_password.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_password.StyleManager = this.metroStyleManager1;
            this.text_password.TabIndex = 1;
            this.text_password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.text_password.UseStyleColors = false;
            this.text_password.UseSystemPasswordChar = true;
            this.text_password.TextChanged += new System.EventHandler(this.text_password_TextChanged);
            // 
            // text_api
            // 
            this.text_api.CustomBackground = false;
            this.text_api.CustomForeColor = false;
            this.text_api.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.text_api.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.text_api.Location = new System.Drawing.Point(155, 212);
            this.text_api.MaxLength = 32767;
            this.text_api.Multiline = false;
            this.text_api.Name = "text_api";
            this.text_api.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_api.SelectedText = "";
            this.text_api.Size = new System.Drawing.Size(128, 20);
            this.text_api.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_api.StyleManager = this.metroStyleManager1;
            this.text_api.TabIndex = 2;
            this.text_api.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.text_api.UseStyleColors = false;
            this.text_api.UseSystemPasswordChar = false;
            // 
            // check_remember
            // 
            this.check_remember.AutoSize = true;
            this.check_remember.CustomBackground = false;
            this.check_remember.CustomForeColor = false;
            this.check_remember.FontSize = MetroFramework.MetroLinkSize.Small;
            this.check_remember.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.check_remember.Location = new System.Drawing.Point(122, 238);
            this.check_remember.Name = "check_remember";
            this.check_remember.Size = new System.Drawing.Size(134, 15);
            this.check_remember.Style = MetroFramework.MetroColorStyle.Blue;
            this.check_remember.StyleManager = this.metroStyleManager1;
            this.check_remember.TabIndex = 3;
            this.check_remember.Text = "Remember Password";
            this.check_remember.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.check_remember.UseStyleColors = false;
            this.check_remember.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.button_cancel.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.button_cancel.Highlight = false;
            this.button_cancel.Location = new System.Drawing.Point(186, 261);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_cancel.StyleManager = this.metroStyleManager1;
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_login
            // 
            this.button_login.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.button_login.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.button_login.Highlight = false;
            this.button_login.Location = new System.Drawing.Point(105, 261);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 23);
            this.button_login.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_login.StyleManager = this.metroStyleManager1;
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CustomBackground = false;
            this.label1.CustomForeColor = false;
            this.label1.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.label1.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.label1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.label1.Location = new System.Drawing.Point(81, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.Style = MetroFramework.MetroColorStyle.Blue;
            this.label1.StyleManager = this.metroStyleManager1;
            this.label1.TabIndex = 18;
            this.label1.Text = "Username";
            this.label1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.label1.UseStyleColors = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.CustomBackground = false;
            this.label2.CustomForeColor = false;
            this.label2.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.label2.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.label2.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.label2.Location = new System.Drawing.Point(86, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.Style = MetroFramework.MetroColorStyle.Blue;
            this.label2.StyleManager = this.metroStyleManager1;
            this.label2.TabIndex = 19;
            this.label2.Text = "Password";
            this.label2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.label2.UseStyleColors = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.CustomBackground = false;
            this.label4.CustomForeColor = false;
            this.label4.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.label4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label4.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.label4.Location = new System.Drawing.Point(93, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 19);
            this.label4.Style = MetroFramework.MetroColorStyle.Blue;
            this.label4.StyleManager = this.metroStyleManager1;
            this.label4.TabIndex = 20;
            this.label4.Text = "API Key";
            this.label4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.label4.UseStyleColors = true;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            this.label4.MouseHover += new System.EventHandler(this.label4_MouseHover);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.CustomBackground = false;
            this.label3.CustomForeColor = false;
            this.label3.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.label3.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.label3.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.label3.Location = new System.Drawing.Point(83, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.Style = MetroFramework.MetroColorStyle.Blue;
            this.label3.StyleManager = this.metroStyleManager1;
            this.label3.TabIndex = 21;
            this.label3.Text = "Status:";
            this.label3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.label3.UseStyleColors = false;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.CustomBackground = false;
            this.label_status.CustomForeColor = false;
            this.label_status.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.label_status.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.label_status.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.label_status.Location = new System.Drawing.Point(128, 287);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(30, 19);
            this.label_status.Style = MetroFramework.MetroColorStyle.Blue;
            this.label_status.StyleManager = this.metroStyleManager1;
            this.label_status.TabIndex = 22;
            this.label_status.Text = "Idle";
            this.label_status.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.label_status.UseStyleColors = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MistClient.Properties.Resources.mist_white;
            this.pictureBox1.Location = new System.Drawing.Point(105, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AcceptButton = this.button_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(367, 326);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.check_remember);
            this.Controls.Add(this.text_api);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_username);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.ShadowType.None;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Login";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker updatechecker;
        private MetroFramework.Controls.MetroTextBox text_username;
        private MetroFramework.Controls.MetroTextBox text_password;
        private MetroFramework.Controls.MetroTextBox text_api;
        private MetroFramework.Controls.MetroCheckBox check_remember;
        private MetroFramework.Controls.MetroButton button_cancel;
        private MetroFramework.Controls.MetroButton button_login;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroLabel label4;
        private MetroFramework.Controls.MetroLabel label3;
        public MetroFramework.Controls.MetroLabel label_status;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}