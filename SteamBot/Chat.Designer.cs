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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChatTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // ChatTabControl
            // 
            this.ChatTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatTabControl.ContextMenuStrip = this.contextMenuStrip1;
            this.ChatTabControl.CustomBackground = false;
            this.ChatTabControl.FontSize = MetroFramework.MetroTabControlSize.Medium;
            this.ChatTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Light;
            this.ChatTabControl.Location = new System.Drawing.Point(23, 63);
            this.ChatTabControl.Name = "ChatTabControl";
            this.ChatTabControl.Size = new System.Drawing.Size(513, 373);
            this.ChatTabControl.Style = MetroFramework.MetroColorStyle.Blue;
            this.ChatTabControl.StyleManager = this.metroStyleManager1;
            this.ChatTabControl.TabIndex = 3;
            this.ChatTabControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ChatTabControl.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChatTabControl.UseStyleColors = false;
            this.ChatTabControl.Click += new System.EventHandler(this.ChatTabControl_Click);
            this.ChatTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChatTabControl_MouseUp);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.OwnerUserControl = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 459);
            this.Controls.Add(this.ChatTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Chat";
            this.ShadowType = MetroFramework.Forms.ShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Chat";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Activated += new System.EventHandler(this.Chat_Activated);
            this.Deactivate += new System.EventHandler(this.Chat_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chat_FormClosed);
            this.Load += new System.EventHandler(this.Chat_Load);
            this.Enter += new System.EventHandler(this.Chat_Enter);
            this.Leave += new System.EventHandler(this.Chat_Leave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeChatToolStripMenuItem;
        public MetroFramework.Controls.MetroTabControl ChatTabControl;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}