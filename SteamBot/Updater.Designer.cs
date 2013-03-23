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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.label1 = new System.Windows.Forms.Label();
            this.label_newver = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_changelog = new System.Windows.Forms.TextBox();
            this.button_skip = new System.Windows.Forms.Button();
            this.button_remind = new System.Windows.Forms.Button();
            this.button_install = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version of Mist is available!";
            // 
            // label_newver
            // 
            this.label_newver.AutoSize = true;
            this.label_newver.Location = new System.Drawing.Point(9, 35);
            this.label_newver.Name = "label_newver";
            this.label_newver.Size = new System.Drawing.Size(203, 26);
            this.label_newver.TabIndex = 1;
            this.label_newver.Text = "Mist v9.9.9 is available (you have v0.0.0).\r\nWould you like to download it now?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Changelog:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // text_changelog
            // 
            this.text_changelog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_changelog.BackColor = System.Drawing.SystemColors.Window;
            this.text_changelog.Location = new System.Drawing.Point(12, 89);
            this.text_changelog.Multiline = true;
            this.text_changelog.Name = "text_changelog";
            this.text_changelog.ReadOnly = true;
            this.text_changelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_changelog.Size = new System.Drawing.Size(341, 266);
            this.text_changelog.TabIndex = 3;
            this.text_changelog.Text = resources.GetString("text_changelog.Text");
            this.text_changelog.Enter += new System.EventHandler(this.text_changelog_Enter);
            // 
            // button_skip
            // 
            this.button_skip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_skip.Location = new System.Drawing.Point(12, 361);
            this.button_skip.Name = "button_skip";
            this.button_skip.Size = new System.Drawing.Size(95, 23);
            this.button_skip.TabIndex = 4;
            this.button_skip.Text = "Skip this version";
            this.button_skip.UseVisualStyleBackColor = true;
            this.button_skip.Click += new System.EventHandler(this.button_skip_Click);
            // 
            // button_remind
            // 
            this.button_remind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_remind.Location = new System.Drawing.Point(150, 361);
            this.button_remind.Name = "button_remind";
            this.button_remind.Size = new System.Drawing.Size(102, 23);
            this.button_remind.TabIndex = 5;
            this.button_remind.Text = "Remind me later";
            this.button_remind.UseVisualStyleBackColor = true;
            this.button_remind.Click += new System.EventHandler(this.button_remind_Click);
            // 
            // button_install
            // 
            this.button_install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_install.Location = new System.Drawing.Point(258, 361);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(95, 23);
            this.button_install.TabIndex = 6;
            this.button_install.Text = "Install update";
            this.button_install.UseVisualStyleBackColor = true;
            this.button_install.Click += new System.EventHandler(this.button_install_Click);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 393);
            this.Controls.Add(this.button_install);
            this.Controls.Add(this.button_remind);
            this.Controls.Add(this.button_skip);
            this.Controls.Add(this.text_changelog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_newver);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Update Available";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Updater_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_newver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_changelog;
        private System.Windows.Forms.Button button_skip;
        private System.Windows.Forms.Button button_remind;
        private System.Windows.Forms.Button button_install;
    }
}