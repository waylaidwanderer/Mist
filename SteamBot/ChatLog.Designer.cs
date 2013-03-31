namespace MistClient
{
    partial class ChatLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatLog));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearChatLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.clearChatLogToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 48);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // clearChatLogToolStripMenuItem
            // 
            this.clearChatLogToolStripMenuItem.Name = "clearChatLogToolStripMenuItem";
            this.clearChatLogToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearChatLogToolStripMenuItem.Text = "Clear Chat Log";
            this.clearChatLogToolStripMenuItem.Click += new System.EventHandler(this.clearChatLogToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.Enter += new System.EventHandler(this.label1_Enter);
            // 
            // textBox1
            // 
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.CustomBackground = false;
            this.textBox1.CustomForeColor = false;
            this.textBox1.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.textBox1.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.textBox1.Location = new System.Drawing.Point(23, 63);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.SelectedText = "";
            this.textBox1.Size = new System.Drawing.Size(497, 394);
            this.textBox1.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.StyleManager = this.metroStyleManager1;
            this.textBox1.TabIndex = 0;
            this.textBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.textBox1.UseStyleColors = false;
            this.textBox1.UseSystemPasswordChar = false;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.OwnerUserControl = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // ChatLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 480);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ChatLog";
            this.ShadowType = MetroFramework.Forms.ShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Chat Log Between You and User";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearChatLogToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}