using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MistClient
{
    public partial class Chat : Form
    {
        public ChatTab chatTab;
        SteamBot.Bot bot;
        public static bool hasFocus = false;
        public TabPage page;

        public Chat(SteamBot.Bot bot)
        {
            InitializeComponent();
            this.bot = bot;
            hasFocus = false;
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            hasFocus = false;
        }

        public void UpdateStatus(string status)
        {
            if (chatTab.chat_status.InvokeRequired)
            {
                chatTab.Invoke((Action)(() => UpdateStatus(status)));
                return;
            }
            chatTab.chat_status.Text = status;
        }

        public void AddChat(string name, ulong sid)
        {
            page = new TabPage();
            page.Text = name;
            page.BackColor = System.Drawing.Color.White;
            chatTab = new ChatTab(this, bot, sid);
            chatTab.Dock = DockStyle.Fill;
            chatTab.Padding = new Padding(3, 3, 3, 3);
            page.Controls.Add(chatTab);
            chatTab.tab = page;
            this.ChatTabControl.TabPages.Add(page);
            this.ChatTabControl.SelectTab(page);
            this.Text = this.ChatTabControl.SelectedTab.Text + " - Chat";
        }

        private void Chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Friends.chat_opened = false;
        }

        private void ChatTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.ChatTabControl.SelectedTab.Dispose();
                if (ChatTabControl.TabCount == 0)
                {
                    this.Dispose();
                    Friends.chat_opened = false;
                }
            }
            if (ChatTabControl.TabCount != 0)
                this.Text = this.ChatTabControl.SelectedTab.Text + " - Chat";
        }

        public void Flash()
        {            
            FlashWindow.Start(this);
        }

        private void Chat_Enter(object sender, EventArgs e)
        {
            hasFocus = true;
            FlashWindow.Stop(this);
        }

        private void Chat_Activated(object sender, EventArgs e)
        {
            hasFocus = true;
            FlashWindow.Stop(this);
        }

        private void Chat_Deactivate(object sender, EventArgs e)
        {
            FlashWindow.Stop(this);
            hasFocus = false;
        }

        private void closeChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChatTabControl.SelectedTab.Dispose();
            if (ChatTabControl.TabCount == 0)
            {
                this.Dispose();
                Friends.chat_opened = false;
            }
            if (ChatTabControl.TabCount != 0)
                this.Text = this.ChatTabControl.SelectedTab.Text + " - Chat";
        }
    }
}
