namespace MistClient
{
    partial class ProfileName
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileName));
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.label_profile = new MetroFramework.Controls.MetroLabel();
            this.text_newprofile = new MetroFramework.Controls.MetroTextBox();
            this.button_ok = new MetroFramework.Controls.MetroButton();
            this.button_cancel = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // label_profile
            // 
            this.label_profile.AutoSize = true;
            this.label_profile.Location = new System.Drawing.Point(23, 60);
            this.label_profile.Name = "label_profile";
            this.label_profile.Size = new System.Drawing.Size(142, 19);
            this.label_profile.Style = MetroFramework.MetroColorStyle.Blue;
            this.label_profile.StyleManager = this.metroStyleManager1;
            this.label_profile.TabIndex = 3;
            this.label_profile.Text = "Change Display Name:";
            this.label_profile.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // text_newprofile
            // 
            this.text_newprofile.Location = new System.Drawing.Point(23, 82);
            this.text_newprofile.MaxLength = 32;
            this.text_newprofile.Name = "text_newprofile";
            this.text_newprofile.PasswordChar = '\0';
            this.text_newprofile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_newprofile.SelectedText = "";
            this.text_newprofile.Size = new System.Drawing.Size(229, 23);
            this.text_newprofile.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_newprofile.StyleManager = this.metroStyleManager1;
            this.text_newprofile.TabIndex = 0;
            this.text_newprofile.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(96, 111);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_ok.StyleManager = this.metroStyleManager1;
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(177, 111);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_cancel.StyleManager = this.metroStyleManager1;
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // ProfileName
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(275, 157);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.text_newprofile);
            this.Controls.Add(this.label_profile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileName";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Change Display Name";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroLabel label_profile;
        private MetroFramework.Controls.MetroTextBox text_newprofile;
        private MetroFramework.Controls.MetroButton button_cancel;
        private MetroFramework.Controls.MetroButton button_ok;
    }
}