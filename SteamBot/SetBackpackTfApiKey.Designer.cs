namespace MistClient
{
    partial class SetBackpackTfApiKey
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
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.button_cancel = new MetroFramework.Controls.MetroButton();
            this.button_ok = new MetroFramework.Controls.MetroButton();
            this.text_apikey = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(204, 111);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_cancel.StyleManager = this.metroStyleManager1;
            this.button_cancel.TabIndex = 7;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(123, 111);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_ok.StyleManager = this.metroStyleManager1;
            this.button_ok.TabIndex = 6;
            this.button_ok.Text = "OK";
            this.button_ok.Theme = MetroFramework.MetroThemeStyle.Light;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // text_apikey
            // 
            this.text_apikey.Location = new System.Drawing.Point(23, 82);
            this.text_apikey.MaxLength = 32767;
            this.text_apikey.Name = "text_apikey";
            this.text_apikey.PasswordChar = '\0';
            this.text_apikey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.text_apikey.SelectedText = "";
            this.text_apikey.Size = new System.Drawing.Size(256, 23);
            this.text_apikey.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_apikey.StyleManager = this.metroStyleManager1;
            this.text_apikey.TabIndex = 5;
            this.text_apikey.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(256, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = this.metroStyleManager1;
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Enter the API key provided by backpack.tf:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // SetBackpackTfApiKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 154);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.text_apikey);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetBackpackTfApiKey";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Text = "Set backpack.tf API Key";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton button_cancel;
        private MetroFramework.Controls.MetroButton button_ok;
        private MetroFramework.Controls.MetroTextBox text_apikey;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}