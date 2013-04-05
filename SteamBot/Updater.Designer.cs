namespace MistClient
{
    partial class Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.label_newver = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLink1 = new MetroFramework.Controls.MetroLink();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.text_changelog = new MetroFramework.Controls.MetroTextBox();
            this.button_skip = new MetroFramework.Controls.MetroButton();
            this.button_remind = new MetroFramework.Controls.MetroButton();
            this.button_install = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(279, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = this.metroStyleManager1;
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "A new version of Mist is available!";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // label_newver
            // 
            this.label_newver.AutoSize = true;
            this.label_newver.Location = new System.Drawing.Point(23, 85);
            this.label_newver.Name = "label_newver";
            this.label_newver.Size = new System.Drawing.Size(240, 38);
            this.label_newver.Style = MetroFramework.MetroColorStyle.Blue;
            this.label_newver.StyleManager = this.metroStyleManager1;
            this.label_newver.TabIndex = 10;
            this.label_newver.Text = "Mist v9001 is available (you have v0.0.0).\r\nWould you like to download it now?";
            this.label_newver.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 123);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(238, 38);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.StyleManager = this.metroStyleManager1;
            this.metroLabel2.TabIndex = 11;
            this.metroLabel2.Text = "For major releases (X.0.0), you can also\r\ndownload it manually from";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLink1
            // 
            this.metroLink1.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroLink1.Location = new System.Drawing.Point(186, 141);
            this.metroLink1.Name = "metroLink1";
            this.metroLink1.Size = new System.Drawing.Size(79, 20);
            this.metroLink1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLink1.StyleManager = this.metroStyleManager1;
            this.metroLink1.TabIndex = 3;
            this.metroLink1.Text = "here.";
            this.metroLink1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroLink1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLink1.UseStyleColors = true;
            this.metroLink1.Click += new System.EventHandler(this.linkLabel1_LinkClicked);
            this.metroLink1.MouseHover += new System.EventHandler(this.linkLabel1_MouseHover);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.label3.Location = new System.Drawing.Point(24, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.Style = MetroFramework.MetroColorStyle.Blue;
            this.label3.StyleManager = this.metroStyleManager1;
            this.label3.TabIndex = 13;
            this.label3.Text = "Changelog:";
            this.label3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // text_changelog
            // 
            this.text_changelog.Location = new System.Drawing.Point(24, 187);
            this.text_changelog.MaxLength = 32767;
            this.text_changelog.Multiline = true;
            this.text_changelog.Name = "text_changelog";
            this.text_changelog.PasswordChar = '\0';
            this.text_changelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_changelog.SelectedText = "";
            this.text_changelog.Size = new System.Drawing.Size(360, 255);
            this.text_changelog.Style = MetroFramework.MetroColorStyle.Blue;
            this.text_changelog.StyleManager = this.metroStyleManager1;
            this.text_changelog.TabIndex = 14;
            this.text_changelog.Text = "All work and no play makes Johnny a dull boy.";
            this.text_changelog.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.text_changelog.Enter += new System.EventHandler(this.text_changelog_Enter);
            // 
            // button_skip
            // 
            this.button_skip.Location = new System.Drawing.Point(23, 448);
            this.button_skip.Name = "button_skip";
            this.button_skip.Size = new System.Drawing.Size(114, 23);
            this.button_skip.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_skip.StyleManager = this.metroStyleManager1;
            this.button_skip.TabIndex = 0;
            this.button_skip.Text = "Skip this version";
            this.button_skip.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_skip.Click += new System.EventHandler(this.button_skip_Click);
            // 
            // button_remind
            // 
            this.button_remind.Location = new System.Drawing.Point(166, 448);
            this.button_remind.Name = "button_remind";
            this.button_remind.Size = new System.Drawing.Size(107, 23);
            this.button_remind.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_remind.StyleManager = this.metroStyleManager1;
            this.button_remind.TabIndex = 1;
            this.button_remind.Text = "Remind me later";
            this.button_remind.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_remind.Click += new System.EventHandler(this.button_remind_Click);
            // 
            // button_install
            // 
            this.button_install.Location = new System.Drawing.Point(279, 448);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(105, 23);
            this.button_install.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_install.StyleManager = this.metroStyleManager1;
            this.button_install.TabIndex = 2;
            this.button_install.Text = "Install update";
            this.button_install.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_install.Click += new System.EventHandler(this.button_install_Click);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
            this.ClientSize = new System.Drawing.Size(407, 494);
            this.Controls.Add(this.metroLink1);
            this.Controls.Add(this.button_install);
            this.Controls.Add(this.button_remind);
            this.Controls.Add(this.button_skip);
            this.Controls.Add(this.text_changelog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.label_newver);
            this.Controls.Add(this.metroLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "New Update Available";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Updater_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel label_newver;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLink metroLink1;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroTextBox text_changelog;
        private MetroFramework.Controls.MetroButton button_skip;
        private MetroFramework.Controls.MetroButton button_remind;
        private MetroFramework.Controls.MetroButton button_install;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}