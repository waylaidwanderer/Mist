namespace MistClient
{
    partial class Chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.ChatTabControl = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatTabControl
            // 
            this.ChatTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatTabControl.ContextMenuStrip = this.contextMenuStrip1;
            this.ChatTabControl.Location = new System.Drawing.Point(12, 12);
            this.ChatTabControl.Name = "ChatTabControl";
            this.ChatTabControl.SelectedIndex = 0;
            this.ChatTabControl.Size = new System.Drawing.Size(537, 435);
            this.ChatTabControl.TabIndex = 2;
            this.ChatTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChatTabControl_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeChatToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 26);
            // 
            // closeChatToolStripMenuItem
            // 
            this.closeChatToolStripMenuItem.Name = "closeChatToolStripMenuItem";
            this.closeChatToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.closeChatToolStripMenuItem.Text = "Close Current Chat";
            this.closeChatToolStripMenuItem.Click += new System.EventHandler(this.closeChatToolStripMenuItem_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 459);
            this.Controls.Add(this.ChatTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Chat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.Activated += new System.EventHandler(this.Chat_Activated);
            this.Deactivate += new System.EventHandler(this.Chat_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chat_FormClosed);
            this.Load += new System.EventHandler(this.Chat_Load);
            this.Enter += new System.EventHandler(this.Chat_Enter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl ChatTabControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeChatToolStripMenuItem;
    }
}