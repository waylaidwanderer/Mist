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
            try
            {
                base.Dispose(disposing);
            }
            catch
            {

            }
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
            this.contextMenuStrip1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewChatLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamRepStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.checkrep = new System.ComponentModel.BackgroundWorker();
            this.text_log = new System.Windows.Forms.RichTextBox();
            this.status_update = new System.ComponentModel.BackgroundWorker();
            this.button_trade = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.button_send = new MetroFramework.Controls.MetroButton();
            this.steam_name = new MetroFramework.Controls.MetroLabel();
            this.steam_status = new MetroFramework.Controls.MetroLabel();
            this.chat_status = new MetroFramework.Controls.MetroLabel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // text_input
            // 
            this.text_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_input.Location = new System.Drawing.Point(0, 309);
            this.text_input.MaxLength = 2048;
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(218, 49);
            this.text_input.TabIndex = 0;
            this.text_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_input_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewProfileToolStripMenuItem,
            this.showBackpackToolStripMenuItem,
            this.viewChatLogToolStripMenuItem,
            this.steamRepStatusToolStripMenuItem,
            this.removeFriendToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 114);
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
            this.showBackpackToolStripMenuItem.Text = "View Backpack";
            this.showBackpackToolStripMenuItem.Click += new System.EventHandler(this.showBackpackToolStripMenuItem_Click);
            // 
            // viewChatLogToolStripMenuItem
            // 
            this.viewChatLogToolStripMenuItem.Name = "viewChatLogToolStripMenuItem";
            this.viewChatLogToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.viewChatLogToolStripMenuItem.Text = "View Chat Log";
            this.viewChatLogToolStripMenuItem.Click += new System.EventHandler(this.viewChatLogToolStripMenuItem_Click);
            // 
            // steamRepStatusToolStripMenuItem
            // 
            this.steamRepStatusToolStripMenuItem.Name = "steamRepStatusToolStripMenuItem";
            this.steamRepStatusToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.steamRepStatusToolStripMenuItem.Text = "SteamRep Status";
            this.steamRepStatusToolStripMenuItem.Click += new System.EventHandler(this.steamRepStatusToolStripMenuItem_Click);
            // 
            // removeFriendToolStripMenuItem
            // 
            this.removeFriendToolStripMenuItem.Name = "removeFriendToolStripMenuItem";
            this.removeFriendToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.removeFriendToolStripMenuItem.Text = "Remove Friend";
            this.removeFriendToolStripMenuItem.Click += new System.EventHandler(this.removeFriendToolStripMenuItem_Click);
            // 
            // avatarBox
            // 
            this.avatarBox.BackColor = System.Drawing.Color.Transparent;
            this.avatarBox.ContextMenuStrip = this.contextMenuStrip1;
            this.avatarBox.Location = new System.Drawing.Point(3, 5);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(40, 40);
            this.avatarBox.TabIndex = 10;
            this.avatarBox.TabStop = false;
            // 
            // checkrep
            // 
            this.checkrep.DoWork += new System.ComponentModel.DoWorkEventHandler(this.checkrep_DoWork);
            // 
            // text_log
            // 
            this.text_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_log.BackColor = System.Drawing.SystemColors.Window;
            this.text_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_log.Location = new System.Drawing.Point(0, 51);
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(369, 252);
            this.text_log.TabIndex = 3;
            this.text_log.Text = "";
            this.text_log.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.text_log_LinkClicked);
            this.text_log.Click += new System.EventHandler(this.text_log_Click);
            this.text_log.DoubleClick += new System.EventHandler(this.text_log_DoubleClick);
            // 
            // status_update
            // 
            this.status_update.DoWork += new System.ComponentModel.DoWorkEventHandler(this.status_update_DoWork);
            // 
            // button_trade
            // 
            this.button_trade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_trade.Location = new System.Drawing.Point(224, 3);
            this.button_trade.Name = "button_trade";
            this.button_trade.Size = new System.Drawing.Size(143, 42);
            this.button_trade.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_trade.TabIndex = 2;
            this.button_trade.Text = "Invite to Trade";
            this.button_trade.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_trade.UseSelectable = true;
            this.button_trade.Click += new System.EventHandler(this.button_trade_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(224, 309);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(145, 50);
            this.button_send.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_send.TabIndex = 1;
            this.button_send.Text = "Send";
            this.button_send.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_send.UseSelectable = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // steam_name
            // 
            this.steam_name.AutoSize = true;
            this.steam_name.ContextMenuStrip = this.contextMenuStrip1;
            this.steam_name.Location = new System.Drawing.Point(47, 5);
            this.steam_name.Name = "steam_name";
            this.steam_name.Size = new System.Drawing.Size(83, 19);
            this.steam_name.Style = MetroFramework.MetroColorStyle.Blue;
            this.steam_name.TabIndex = 14;
            this.steam_name.Text = "steam_name";
            this.steam_name.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.steam_name.Click += new System.EventHandler(this.steam_name_Click);
            // 
            // steam_status
            // 
            this.steam_status.AutoSize = true;
            this.steam_status.ContextMenuStrip = this.contextMenuStrip1;
            this.steam_status.Location = new System.Drawing.Point(47, 24);
            this.steam_status.Name = "steam_status";
            this.steam_status.Size = new System.Drawing.Size(82, 19);
            this.steam_status.Style = MetroFramework.MetroColorStyle.Blue;
            this.steam_status.TabIndex = 15;
            this.steam_status.Text = "steam_status";
            this.steam_status.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // chat_status
            // 
            this.chat_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chat_status.AutoSize = true;
            this.chat_status.Location = new System.Drawing.Point(0, 363);
            this.chat_status.Name = "chat_status";
            this.chat_status.Size = new System.Drawing.Size(71, 19);
            this.chat_status.Style = MetroFramework.MetroColorStyle.Blue;
            this.chat_status.TabIndex = 16;
            this.chat_status.Text = "chat_status";
            this.chat_status.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // ChatTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chat_status);
            this.Controls.Add(this.steam_status);
            this.Controls.Add(this.steam_name);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.button_trade);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.avatarBox);
            this.Controls.Add(this.text_input);
            this.MinimumSize = new System.Drawing.Size(286, 156);
            this.Name = "ChatTab";
            this.Size = new System.Drawing.Size(370, 387);
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UseStyleColors = true;
            this.Load += new System.EventHandler(this.ChatTab_Load);
            this.Enter += new System.EventHandler(this.ChatTab_Enter);
            this.Leave += new System.EventHandler(this.ChatTab_Leave);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_input;
        private MetroFramework.Controls.MetroContextMenu contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showBackpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem steamRepStatusToolStripMenuItem;
        public System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.ToolStripMenuItem viewProfileToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker checkrep;
        private System.Windows.Forms.RichTextBox text_log;
        private System.ComponentModel.BackgroundWorker status_update;
        private System.Windows.Forms.ToolStripMenuItem viewChatLogToolStripMenuItem;
        private MetroFramework.Controls.MetroButton button_trade;
        private MetroFramework.Controls.MetroButton button_send;
        private MetroFramework.Controls.MetroLabel steam_name;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        public MetroFramework.Controls.MetroLabel chat_status;
        public MetroFramework.Controls.MetroLabel steam_status;
        private System.Windows.Forms.ToolStripMenuItem removeFriendToolStripMenuItem;

    }
}
