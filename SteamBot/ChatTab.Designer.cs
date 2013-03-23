namespace MistClient
{
    partial class ChatTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.text_input = new System.Windows.Forms.TextBox();
            this.text_log = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.chat_status = new System.Windows.Forms.Label();
            this.button_trade = new System.Windows.Forms.Button();
            this.steam_name = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamRepStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steam_status = new System.Windows.Forms.Label();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // text_input
            // 
            this.text_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_input.Location = new System.Drawing.Point(0, 303);
            this.text_input.MaxLength = 2048;
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(218, 61);
            this.text_input.TabIndex = 0;
            this.text_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_input_KeyPress);
            // 
            // text_log
            // 
            this.text_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_log.BackColor = System.Drawing.SystemColors.Window;
            this.text_log.Location = new System.Drawing.Point(-2, 51);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(369, 246);
            this.text_log.TabIndex = 3;
            this.text_log.Click += new System.EventHandler(this.text_log_Click);
            this.text_log.DoubleClick += new System.EventHandler(this.text_log_DoubleClick);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(225, 303);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(145, 62);
            this.button_send.TabIndex = 1;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // chat_status
            // 
            this.chat_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chat_status.AutoSize = true;
            this.chat_status.Location = new System.Drawing.Point(-3, 367);
            this.chat_status.Name = "chat_status";
            this.chat_status.Size = new System.Drawing.Size(62, 13);
            this.chat_status.TabIndex = 6;
            this.chat_status.Text = "chat_status";
            // 
            // button_trade
            // 
            this.button_trade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_trade.Location = new System.Drawing.Point(225, 0);
            this.button_trade.Name = "button_trade";
            this.button_trade.Size = new System.Drawing.Size(144, 45);
            this.button_trade.TabIndex = 2;
            this.button_trade.Text = "Invite to Trade";
            this.button_trade.UseVisualStyleBackColor = true;
            this.button_trade.Click += new System.EventHandler(this.button_trade_Click);
            // 
            // steam_name
            // 
            this.steam_name.AutoSize = true;
            this.steam_name.ContextMenuStrip = this.contextMenuStrip1;
            this.steam_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steam_name.Location = new System.Drawing.Point(46, 5);
            this.steam_name.Name = "steam_name";
            this.steam_name.Size = new System.Drawing.Size(96, 16);
            this.steam_name.TabIndex = 8;
            this.steam_name.Text = "steam_name";
            this.steam_name.Click += new System.EventHandler(this.steam_name_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewProfileToolStripMenuItem,
            this.showBackpackToolStripMenuItem,
            this.steamRepStatusToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 70);
            // 
            // viewProfileToolStripMenuItem
            // 
            this.viewProfileToolStripMenuItem.Name = "viewProfileToolStripMenuItem";
            this.viewProfileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.viewProfileToolStripMenuItem.Text = "View Profile";
            this.viewProfileToolStripMenuItem.Click += new System.EventHandler(this.viewProfileToolStripMenuItem_Click);
            // 
            // showBackpackToolStripMenuItem
            // 
            this.showBackpackToolStripMenuItem.Name = "showBackpackToolStripMenuItem";
            this.showBackpackToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showBackpackToolStripMenuItem.Text = "Show Backpack";
            this.showBackpackToolStripMenuItem.Click += new System.EventHandler(this.showBackpackToolStripMenuItem_Click);
            // 
            // steamRepStatusToolStripMenuItem
            // 
            this.steamRepStatusToolStripMenuItem.Name = "steamRepStatusToolStripMenuItem";
            this.steamRepStatusToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.steamRepStatusToolStripMenuItem.Text = "SteamRep Status";
            this.steamRepStatusToolStripMenuItem.Click += new System.EventHandler(this.steamRepStatusToolStripMenuItem_Click);
            // 
            // steam_status
            // 
            this.steam_status.AutoSize = true;
            this.steam_status.ContextMenuStrip = this.contextMenuStrip1;
            this.steam_status.Location = new System.Drawing.Point(47, 21);
            this.steam_status.Name = "steam_status";
            this.steam_status.Size = new System.Drawing.Size(69, 13);
            this.steam_status.TabIndex = 9;
            this.steam_status.Text = "steam_status";
            // 
            // avatarBox
            // 
            this.avatarBox.ContextMenuStrip = this.contextMenuStrip1;
            this.avatarBox.Location = new System.Drawing.Point(1, 5);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(40, 40);
            this.avatarBox.TabIndex = 10;
            this.avatarBox.TabStop = false;
            // 
            // ChatTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.avatarBox);
            this.Controls.Add(this.steam_status);
            this.Controls.Add(this.steam_name);
            this.Controls.Add(this.button_trade);
            this.Controls.Add(this.chat_status);
            this.Controls.Add(this.text_input);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.button_send);
            this.MinimumSize = new System.Drawing.Size(286, 156);
            this.Name = "ChatTab";
            this.Size = new System.Drawing.Size(370, 380);
            this.Load += new System.EventHandler(this.ChatTab_Load);
            this.Enter += new System.EventHandler(this.ChatTab_Enter);
            this.Leave += new System.EventHandler(this.ChatTab_Leave);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Button button_send;
        public System.Windows.Forms.TextBox text_log;
        private System.Windows.Forms.Button button_trade;
        private System.Windows.Forms.Label steam_name;
        public System.Windows.Forms.Label chat_status;
        public System.Windows.Forms.Label steam_status;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showBackpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem steamRepStatusToolStripMenuItem;
        public System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.ToolStripMenuItem viewProfileToolStripMenuItem;

    }
}
