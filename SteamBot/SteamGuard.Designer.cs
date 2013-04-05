namespace MistClient
{
    partial class SteamGuard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamGuard));
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.text_auth = new MetroFramework.Controls.MetroTextBox();
            this.button_ok = new MetroFramework.Controls.MetroButton();
            this.button_cancel = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 38);
            this.label1.Style = MetroFramework.MetroColorStyle.Blue;
            this.label1.StyleManager = this.metroStyleManager1;
            this.label1.TabIndex = 4;
            this.label1.Text = "This account is protected by Steam Guard.\r\nEnter the authentication code sent to " +
    "the proper email:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // text_auth
            // 
            this.text_auth.Location = new System.Drawing.Point(64, 101);
            this.text_auth.MaxLength = 32767;
            this.text_auth.Name = "text_auth";
            this.text_auth.PasswordChar = '\0';
            this.text_auth.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_auth.SelectedText = "";
            this.text_auth.Size = new System.Drawing.Size(247, 23);
            this.text_auth.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_auth.StyleManager = this.metroStyleManager1;
            this.text_auth.TabIndex = 0;
            this.text_auth.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(109, 130);
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
            this.button_cancel.Location = new System.Drawing.Point(190, 130);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_cancel.StyleManager = this.metroStyleManager1;
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // SteamGuard
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(375, 177);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.text_auth);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SteamGuard";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "SteamGuard";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroTextBox text_auth;
        private MetroFramework.Controls.MetroButton button_ok;
        private MetroFramework.Controls.MetroButton button_cancel;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}