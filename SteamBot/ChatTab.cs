using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using MistClient;
using SteamBot;
using ToastNotifications;

namespace MistClient
{
    public partial class ChatTab : UserControl
    {
        ulong sid;
        Bot bot;
        public bool otherSentTrade = false;
        int tradeMode = 1;
        public uint tradeID;
        public byte[] AvatarHash { get; set; } // checking if update is necessary
        public Chat Chat;
        public TabPage tab;

        public ChatTab(Chat chat, Bot bot, ulong sid)
        {
            InitializeComponent();
            this.Chat = chat;
            this.sid = sid;
            this.bot = bot;
            this.steam_name.Text = bot.SteamFriends.GetFriendPersonaName(sid);
            this.steam_status.Text = bot.SteamFriends.GetFriendPersonaState(sid).ToString();
            this.chat_status.Text = "";
            SteamKit2.SteamID SteamID = sid;
            byte[] avatarHash = bot.SteamFriends.GetFriendAvatar(SteamID);
            bool validHash = avatarHash != null && !IsZeros(avatarHash);

            if ((AvatarHash == null && !validHash && avatarBox.Image != null) || (AvatarHash != null && AvatarHash.SequenceEqual(avatarHash)))
            {
                // avatar is already up to date, no operations necessary
            }
            else if (validHash)
            {
                AvatarHash = avatarHash;
                CDNCache.DownloadAvatar(SteamID, avatarHash, AvatarDownloaded);
            }
            else
            {
                AvatarHash = null;
                avatarBox.Image = ComposeAvatar(null);
            }

        }

        public bool IsInGame()
        {
            try
            {
                string gameName = bot.SteamFriends.GetFriendGamePlayedName(sid);
                return !string.IsNullOrEmpty(gameName);
            }
            catch
            {
                return false;
            }
        }

        public bool IsOnline()
        {
            try
            {
                return bot.SteamFriends.GetFriendPersonaState(sid) != SteamKit2.EPersonaState.Offline;
            }
            catch { return false; }
        }

        Bitmap GetHolder()
        {
            if (IsInGame())
                return MistClient.Properties.Resources.IconIngame;

            if (IsOnline())
                return MistClient.Properties.Resources.IconOnline;

            return MistClient.Properties.Resources.IconOffline;
        }
        Bitmap GetAvatar(string path)
        {
            try
            {
                if (path == null)
                    return MistClient.Properties.Resources.IconUnknown;
                return (Bitmap)Bitmap.FromFile(path);
            }
            catch
            {
                return MistClient.Properties.Resources.IconUnknown;
            }
        }

        Bitmap ComposeAvatar(string path)
        {
            Bitmap holder = GetHolder();
            Bitmap avatar = GetAvatar(path);

            Graphics gfx = null;
            try
            {
                gfx = Graphics.FromImage(holder);
                gfx.DrawImage(avatar, new Rectangle(4, 4, avatar.Width, avatar.Height));
            }
            finally
            {
                gfx.Dispose();
            }

            return holder;
        }

        void AvatarDownloaded(AvatarDownloadDetails details)
        {
            try
            {
                if (avatarBox.InvokeRequired)
                {
                    avatarBox.Invoke(new MethodInvoker(() =>
                    {
                        AvatarDownloaded(details);
                    }
                    ));
                }
                else
                {
                    avatarBox.Image = ComposeAvatar((details.Success ? details.Filename : null));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FriendControl", "Unable to compose avatar: {0}", ex.Message);
            }
        }

        public static bool IsZeros(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 0)
                    return false;
            }
            return true;
        }

        public void TradeButtonMode(int _mode, uint _tradeID = 0)
        {
            int mode = _mode;
            tradeID = _tradeID;
            switch (mode)
            {
                case 1: // not in trade - "Invite to trade"
                    tradeMode = 1;
                    button_trade.Text = "Invite to Trade";
                    button_trade.Enabled = true;
                    break;
                case 2: // User sent trade request - "Cancel trade request"
                    tradeMode = 2;
                    button_trade.Text = "Cancel Trade Request";
                    break;
                case 3: // Other sent trade request - "Accept trade request"
                    button_trade.Enabled = true;
                    tradeMode = 3;
                    text_log.AppendText("[" + DateTime.Now + "] - " + steam_name.Text + " has requested to trade with you.\r\n");
                    if (!Chat.hasFocus)
                    {
                        try
                        {
                            string soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                            string soundFile = Path.Combine(soundsFolder + "message.wav");
                            using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFile))
                            {
                                player.Play();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Chat.Flash();
                    }
                    button_trade.Text = "Accept Trade Request";
                    break;
            }
        }

        public void UpdateButton(string message)
        {
            button_trade.Text = message;
            button_trade.Enabled = false;
        }

        public void UpdateChat(string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (text_log.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                text_log.Invoke((Action)(() => UpdateChat(text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            Chat.chatTab.text_log.AppendText(text);
            Chat.chatTab.text_log.ScrollToCaret();
            Chat.Flash();
            if (!Chat.hasFocus)
            {
                try
                {
                    string soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                    string soundFile = Path.Combine(soundsFolder + "message.wav");
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFile))
                    {
                        player.Play();
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Chat.Flash();
            }
        }

        public void UpdateStatus(string status)
        {
            if (chat_status.InvokeRequired)
            {
                chat_status.Invoke((Action)(() => UpdateStatus(status)));
                return;
            }
            chat_status.Text = status;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if (text_input.Text != "")
            {
                bot.SteamFriends.SendChatMessage(sid, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                text_log.AppendText("[" + DateTime.Now + "] - " + Bot.displayName + ": " + text_input.Text + "\r\n");
                clear();
            }
        }

        
        private void text_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (text_input.Text != "")
                {
                    e.Handled = true;
                    bot.SteamFriends.SendChatMessage(sid, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                    text_log.AppendText("[" + DateTime.Now + "] - " + Bot.displayName + ": " + text_input.Text + "\r\n");
                    clear();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        void clear()
        {
            text_input.Select(0, 0);
            text_input.Clear();
        }

        private void button_trade_Click(object sender, EventArgs e)
        {
            tradeClicked();
        }

        public void tradeClicked()
        {
            switch (tradeMode)
            {
                case 1: // not in trade - "Invite to trade"
                    bot.SteamTrade.Trade(sid);
                    tradeMode = 2;
                    button_trade.Text = "Cancel Trade Request";
                    UpdateChat("[" + DateTime.Now + "] You have sent " + steam_name.Text + " a trade request.\r\n");
                    break;
                case 2: // User sent trade request - "Cancel trade request"
                    bot.SteamTrade.CancelTrade(sid);
                    tradeMode = 1;
                    button_trade.Text = "Invite to Trade";
                    break;
                case 3: // Other sent trade request - "Accept trade request"
                    bot.SteamTrade.RespondToTrade(tradeID, true);
                    button_trade.Text = "Accepted Trade Request";
                    button_trade.Enabled = false;
                    break;
            }
        }

        private void ChatTab_Enter(object sender, EventArgs e)
        {
            foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
            {
                if (tab.Text == steam_name.Text)
                {
                    Console.WriteLine("Looking at " + tab.Text);
                    foreach (var item in tab.Controls)
                    {
                        Friends.chat.chatTab = (ChatTab)item;
                    }
                }
            }
        }

        private void ChatTab_Leave(object sender, EventArgs e)
        {
        }

        private void showBackpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Friend's backpack
            ShowBackpack showBP = new ShowBackpack(bot, sid);
            showBP.Show();
            showBP.Activate();
        }

        private void steamRepStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://steamrep.com/api/beta/reputation/" + sid;
                string response = Friends.HTTPRequest(url);
                if (response != "")
                {
                    string status = Friends.ParseBetween(response, "<reputation>", "</reputation>");
                    if (status == "")
                    {
                        MessageBox.Show("User has no special reputation.",
                        "SteamRep Status",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show(status,
                        "SteamRep Status",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
