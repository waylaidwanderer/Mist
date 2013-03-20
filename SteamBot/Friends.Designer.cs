namespace MistClient
{
    partial class Friends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Friends));
            this.steam_name = new System.Windows.Forms.Label();
            this.steam_status = new System.Windows.Forms.Label();
            this.menu_friend = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteToTradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewGameInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blockFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.showBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamRepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_status = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.awayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookingToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookingToTradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snoozeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.showBackpackToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeProfileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label_addfriend2 = new System.Windows.Forms.Label();
            this.label_addfriend = new System.Windows.Forms.Label();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.column_friend = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_status = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.friends_list = new BrightIdeasSoftware.ObjectListView();
            this.menu_friend.SuspendLayout();
            this.menu_status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friends_list)).BeginInit();
            this.SuspendLayout();
            // 
            // steam_name
            // 
            this.steam_name.AutoSize = true;
            this.steam_name.Location = new System.Drawing.Point(58, 12);
            this.steam_name.Name = "steam_name";
            this.steam_name.Size = new System.Drawing.Size(67, 13);
            this.steam_name.TabIndex = 1;
            this.steam_name.Text = "steam_name";
            // 
            // steam_status
            // 
            this.steam_status.AutoSize = true;
            this.steam_status.Location = new System.Drawing.Point(58, 25);
            this.steam_status.Name = "steam_status";
            this.steam_status.Size = new System.Drawing.Size(37, 13);
            this.steam_status.TabIndex = 2;
            this.steam_status.Text = "Online";
            // 
            // menu_friend
            // 
            this.menu_friend.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openChatToolStripMenuItem,
            this.inviteToTradeToolStripMenuItem,
            this.viewGameInfoToolStripMenuItem,
            this.removeFriendToolStripMenuItem,
            this.blockFriendToolStripMenuItem,
            this.toolStripMenuItem2,
            this.showBackpackToolStripMenuItem,
            this.steamRepToolStripMenuItem});
            this.menu_friend.Name = "menu_friend";
            this.menu_friend.Size = new System.Drawing.Size(163, 164);
            // 
            // openChatToolStripMenuItem
            // 
            this.openChatToolStripMenuItem.Name = "openChatToolStripMenuItem";
            this.openChatToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openChatToolStripMenuItem.Text = "Open Chat";
            this.openChatToolStripMenuItem.Click += new System.EventHandler(this.openChatToolStripMenuItem_Click);
            // 
            // inviteToTradeToolStripMenuItem
            // 
            this.inviteToTradeToolStripMenuItem.Name = "inviteToTradeToolStripMenuItem";
            this.inviteToTradeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.inviteToTradeToolStripMenuItem.Text = "Invite To Trade";
            this.inviteToTradeToolStripMenuItem.Click += new System.EventHandler(this.inviteToTradeToolStripMenuItem_Click);
            // 
            // viewGameInfoToolStripMenuItem
            // 
            this.viewGameInfoToolStripMenuItem.Enabled = false;
            this.viewGameInfoToolStripMenuItem.Name = "viewGameInfoToolStripMenuItem";
            this.viewGameInfoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.viewGameInfoToolStripMenuItem.Text = "View Game Info";
            this.viewGameInfoToolStripMenuItem.Click += new System.EventHandler(this.viewGameInfoToolStripMenuItem_Click);
            // 
            // removeFriendToolStripMenuItem
            // 
            this.removeFriendToolStripMenuItem.Name = "removeFriendToolStripMenuItem";
            this.removeFriendToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.removeFriendToolStripMenuItem.Text = "Remove Friend";
            this.removeFriendToolStripMenuItem.Click += new System.EventHandler(this.removeFriendToolStripMenuItem_Click);
            // 
            // blockFriendToolStripMenuItem
            // 
            this.blockFriendToolStripMenuItem.Name = "blockFriendToolStripMenuItem";
            this.blockFriendToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.blockFriendToolStripMenuItem.Text = "Block Friend";
            this.blockFriendToolStripMenuItem.Click += new System.EventHandler(this.blockFriendToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // showBackpackToolStripMenuItem
            // 
            this.showBackpackToolStripMenuItem.Name = "showBackpackToolStripMenuItem";
            this.showBackpackToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showBackpackToolStripMenuItem.Text = "Show Backpack";
            this.showBackpackToolStripMenuItem.Click += new System.EventHandler(this.showBackpackToolStripMenuItem_Click);
            // 
            // steamRepToolStripMenuItem
            // 
            this.steamRepToolStripMenuItem.Name = "steamRepToolStripMenuItem";
            this.steamRepToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.steamRepToolStripMenuItem.Text = "SteamRep Status";
            this.steamRepToolStripMenuItem.Click += new System.EventHandler(this.steamRepToolStripMenuItem_Click);
            // 
            // menu_status
            // 
            this.menu_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.awayToolStripMenuItem,
            this.busyToolStripMenuItem,
            this.lookingToPlayToolStripMenuItem,
            this.lookingToTradeToolStripMenuItem,
            this.snoozeToolStripMenuItem,
            this.offlineToolStripMenuItem,
            this.toolStripMenuItem1,
            this.showBackpackToolStripMenuItem1,
            this.changeProfileNameToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutMistToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.menu_status.Name = "menu_status";
            this.menu_status.Size = new System.Drawing.Size(197, 258);
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // awayToolStripMenuItem
            // 
            this.awayToolStripMenuItem.Name = "awayToolStripMenuItem";
            this.awayToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.awayToolStripMenuItem.Text = "Away";
            this.awayToolStripMenuItem.Click += new System.EventHandler(this.awayToolStripMenuItem_Click);
            // 
            // busyToolStripMenuItem
            // 
            this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
            this.busyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.busyToolStripMenuItem.Text = "Busy";
            this.busyToolStripMenuItem.Click += new System.EventHandler(this.busyToolStripMenuItem_Click);
            // 
            // lookingToPlayToolStripMenuItem
            // 
            this.lookingToPlayToolStripMenuItem.Name = "lookingToPlayToolStripMenuItem";
            this.lookingToPlayToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.lookingToPlayToolStripMenuItem.Text = "Looking to Play";
            this.lookingToPlayToolStripMenuItem.Click += new System.EventHandler(this.lookingToPlayToolStripMenuItem_Click);
            // 
            // lookingToTradeToolStripMenuItem
            // 
            this.lookingToTradeToolStripMenuItem.Name = "lookingToTradeToolStripMenuItem";
            this.lookingToTradeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.lookingToTradeToolStripMenuItem.Text = "Looking to Trade";
            this.lookingToTradeToolStripMenuItem.Click += new System.EventHandler(this.lookingToTradeToolStripMenuItem_Click);
            // 
            // snoozeToolStripMenuItem
            // 
            this.snoozeToolStripMenuItem.Name = "snoozeToolStripMenuItem";
            this.snoozeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.snoozeToolStripMenuItem.Text = "Snooze";
            this.snoozeToolStripMenuItem.Click += new System.EventHandler(this.snoozeToolStripMenuItem_Click);
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            this.offlineToolStripMenuItem.Click += new System.EventHandler(this.offlineToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
            // 
            // showBackpackToolStripMenuItem1
            // 
            this.showBackpackToolStripMenuItem1.Name = "showBackpackToolStripMenuItem1";
            this.showBackpackToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.showBackpackToolStripMenuItem1.Text = "Show Backpack";
            this.showBackpackToolStripMenuItem1.Click += new System.EventHandler(this.showBackpackToolStripMenuItem1_Click);
            // 
            // changeProfileNameToolStripMenuItem
            // 
            this.changeProfileNameToolStripMenuItem.Name = "changeProfileNameToolStripMenuItem";
            this.changeProfileNameToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.changeProfileNameToolStripMenuItem.Text = "Change Profile Name...";
            this.changeProfileNameToolStripMenuItem.Click += new System.EventHandler(this.changeProfileNameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(193, 6);
            // 
            // aboutMistToolStripMenuItem
            // 
            this.aboutMistToolStripMenuItem.Name = "aboutMistToolStripMenuItem";
            this.aboutMistToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutMistToolStripMenuItem.Text = "About Mist";
            this.aboutMistToolStripMenuItem.Click += new System.EventHandler(this.aboutMistToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(143, 13);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(175, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "▼";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // label_addfriend2
            // 
            this.label_addfriend2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_addfriend2.AutoSize = true;
            this.label_addfriend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_addfriend2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_addfriend2.Location = new System.Drawing.Point(10, 427);
            this.label_addfriend2.Name = "label_addfriend2";
            this.label_addfriend2.Size = new System.Drawing.Size(24, 25);
            this.label_addfriend2.TabIndex = 7;
            this.label_addfriend2.Text = "+";
            this.label_addfriend2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_addfriend2.Click += new System.EventHandler(this.label_addfriend2_Click);
            this.label_addfriend2.MouseLeave += new System.EventHandler(this.label_addfriend2_MouseLeave);
            this.label_addfriend2.MouseHover += new System.EventHandler(this.label_addfriend2_MouseHover);
            // 
            // label_addfriend
            // 
            this.label_addfriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_addfriend.AutoSize = true;
            this.label_addfriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_addfriend.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_addfriend.Location = new System.Drawing.Point(31, 432);
            this.label_addfriend.Name = "label_addfriend";
            this.label_addfriend.Size = new System.Drawing.Size(109, 16);
            this.label_addfriend.TabIndex = 8;
            this.label_addfriend.Text = "Add a Friend...";
            this.label_addfriend.Click += new System.EventHandler(this.label_addfriend_Click);
            this.label_addfriend.MouseLeave += new System.EventHandler(this.label_addfriend_MouseLeave);
            this.label_addfriend.MouseHover += new System.EventHandler(this.label_addfriend_MouseHover);
            // 
            // avatarBox
            // 
            this.avatarBox.Location = new System.Drawing.Point(12, 9);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(40, 40);
            this.avatarBox.TabIndex = 11;
            this.avatarBox.TabStop = false;
            // 
            // column_friend
            // 
            this.column_friend.AspectName = "Name";
            this.column_friend.CellPadding = null;
            this.column_friend.FillsFreeSpace = true;
            this.column_friend.Groupable = false;
            this.column_friend.Hideable = false;
            this.column_friend.IsEditable = false;
            this.column_friend.MinimumWidth = 100;
            this.column_friend.Searchable = false;
            this.column_friend.Text = "Friend";
            this.column_friend.UseInitialLetterForGroup = true;
            this.column_friend.Width = 150;
            this.column_friend.WordWrap = true;
            // 
            // column_status
            // 
            this.column_status.AspectName = "Status";
            this.column_status.CellPadding = null;
            this.column_status.Hideable = false;
            this.column_status.IsEditable = false;
            this.column_status.MinimumWidth = 100;
            this.column_status.Searchable = false;
            this.column_status.Sortable = false;
            this.column_status.Text = "Status";
            this.column_status.Width = 100;
            this.column_status.WordWrap = true;
            // 
            // friends_list
            // 
            this.friends_list.AllColumns.Add(this.column_friend);
            this.friends_list.AllColumns.Add(this.column_status);
            this.friends_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.friends_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_friend,
            this.column_status});
            this.friends_list.ContextMenuStrip = this.menu_friend;
            this.friends_list.Location = new System.Drawing.Point(12, 55);
            this.friends_list.MultiSelect = false;
            this.friends_list.Name = "friends_list";
            this.friends_list.SelectAllOnControlA = false;
            this.friends_list.Size = new System.Drawing.Size(304, 369);
            this.friends_list.TabIndex = 4;
            this.friends_list.UseCompatibleStateImageBehavior = false;
            this.friends_list.View = System.Windows.Forms.View.Details;
            this.friends_list.BeforeSearching += new System.EventHandler<BrightIdeasSoftware.BeforeSearchingEventArgs>(this.friends_list_BeforeSearching);
            this.friends_list.ItemActivate += new System.EventHandler(this.friends_list_ItemActivate);
            // 
            // Friends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 461);
            this.Controls.Add(this.avatarBox);
            this.Controls.Add(this.label_addfriend);
            this.Controls.Add(this.label_addfriend2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.friends_list);
            this.Controls.Add(this.steam_status);
            this.Controls.Add(this.steam_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Friends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Friends";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Friends_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Friends_FormClosed);
            this.Load += new System.EventHandler(this.Friends_Load);
            this.menu_friend.ResumeLayout(false);
            this.menu_status.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friends_list)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label steam_name;
        public System.Windows.Forms.Label steam_status;
        private System.Windows.Forms.ContextMenuStrip menu_status;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem awayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookingToPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookingToTradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snoozeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changeProfileNameToolStripMenuItem;
        private System.Windows.Forms.Label label_addfriend2;
        private System.Windows.Forms.Label label_addfriend;
        private System.Windows.Forms.ContextMenuStrip menu_friend;
        private System.Windows.Forms.ToolStripMenuItem showBackpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inviteToTradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFriendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blockFriendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showBackpackToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem steamRepToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutMistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.ToolStripMenuItem viewGameInfoToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn column_friend;
        private BrightIdeasSoftware.OLVColumn column_status;
        public BrightIdeasSoftware.ObjectListView friends_list;
    }
}