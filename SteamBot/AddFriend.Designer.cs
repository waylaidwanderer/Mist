namespace MistClient
{
    partial class AddFriend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFriend));
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.text_profile = new MetroFramework.Controls.MetroTextBox();
            this.button_ok = new MetroFramework.Controls.MetroButton();
            this.button_cancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.OwnerUserControl = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.CustomForeColor = false;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(23, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(300, 38);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = this.metroStyleManager1;
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Enter the SteamID64 of the friend you wish to add\r\n(e.g. 76561197960265728):";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseStyleColors = false;
            // 
            // text_profile
            // 
            this.text_profile.CustomBackground = false;
            this.text_profile.CustomForeColor = false;
            this.text_profile.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.text_profile.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.text_profile.Location = new System.Drawing.Point(23, 107);
            this.text_profile.MaxLength = 32767;
            this.text_profile.Multiline = false;
            this.text_profile.Name = "text_profile";
            this.text_profile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_profile.SelectedText = "";
            this.text_profile.Size = new System.Drawing.Size(300, 23);
            this.text_profile.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_profile.StyleManager = this.metroStyleManager1;
            this.text_profile.TabIndex = 0;
            this.text_profile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.text_profile.UseStyleColors = false;
            this.text_profile.UseSystemPasswordChar = false;
            // 
            // button_ok
            // 
            this.button_ok.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.button_ok.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.button_ok.Highlight = false;
            this.button_ok.Location = new System.Drawing.Point(167, 136);
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
            this.button_cancel.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.button_cancel.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.button_cancel.Highlight = false;
            this.button_cancel.Location = new System.Drawing.Point(248, 136);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_cancel.StyleManager = this.metroStyleManager1;
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // AddFriend
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(353, 182);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.text_profile);
            this.Controls.Add(this.metroLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFriend";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.ShadowType.DropShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Add a Friend";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox text_profile;
        private MetroFramework.Controls.MetroButton button_ok;
        private MetroFramework.Controls.MetroButton button_cancel;
    }
}