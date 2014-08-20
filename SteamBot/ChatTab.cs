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
using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Runtime.InteropServices;

namespace MistClient
{
    public partial class ChatTab : MetroUserControl
    {
        ulong userSteamId;
        Bot bot;
        public bool otherSentTrade = false;
        int tradeMode = 1;
        public uint tradeID;
        public byte[] AvatarHash { get; set; } // checking if update is necessary
        public Chat Chat;
        public TabPage tab;
        string prevName;
        string prevStatus;
        bool inGame = false;

        public ChatTab(Chat chat, Bot bot, ulong sid)
        {
            InitializeComponent();
            this.Chat = chat;
            this.userSteamId = sid;
            this.bot = bot;
            this.StyleManager.OnThemeChanged += metroStyleManager1_OnThemeChanged;
            this.StyleManager.Theme = Friends.GlobalStyleManager.Theme;
            this.StyleManager.Style = Friends.GlobalStyleManager.Style;
            Util.LoadTheme(null, this.Controls, this);
            this.Theme = Friends.GlobalStyleManager.Theme;
            this.StyleManager.Style = Friends.GlobalStyleManager.Style;
            metroStyleManager1_OnThemeChanged(null, EventArgs.Empty);
            try
            {
                this.steam_name.Text = prevName = bot.SteamFriends.GetFriendPersonaName(sid);
                this.steam_status.Text = prevStatus = bot.SteamFriends.GetFriendPersonaState(sid).ToString();
            }
            catch
            {

            }
            this.chat_status.Text = "";
            SteamKit2.SteamID SteamID = sid;
            try
            {
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
            catch
            {

            }
            new System.Threading.Thread(GetChatLog).Start();
            checkrep.RunWorkerAsync();
            status_update.RunWorkerAsync();
            text_input.Focus();   
        }

        void metroStyleManager1_OnThemeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.StyleManager.Theme == MetroFramework.MetroThemeStyle.Dark)
                {
                    var backColor = Color.FromArgb(50, 50, 50);
                    panel1.BackColor = backColor;
                    text_log.BackColor = backColor;
                    panel2.BackColor = backColor;
                    chat_status.BackColor = backColor;
                    text_log.ForeColor = Color.Silver;
                    text_log.SelectionColor = Color.Silver;
                    text_input.BackColor = backColor;
                    text_input.ForeColor = Color.Silver;
                }
                else
                {
                    panel1.BackColor = Color.WhiteSmoke;
                    text_log.BackColor = Color.WhiteSmoke;
                    panel2.BackColor = Color.WhiteSmoke;
                    chat_status.BackColor = Color.WhiteSmoke;
                    text_log.ForeColor = Color.Black;
                    text_log.SelectionColor = Color.Black;
                    text_input.BackColor = Color.WhiteSmoke;
                    text_input.ForeColor = Color.Black;
                }
            }
            catch
            {

            }            
        }

        public void GetChatLog()
        {
            var userAccountId = new SteamKit2.SteamID(userSteamId).AccountID;
            var botAccountId = bot.SteamUser.SteamID.AccountID;
            var url = "http://steamcommunity.com/chat/chatlog/" + userAccountId;
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("sessionid", bot.sessionId);
            var response = SteamTrade.SteamWeb.Fetch(url, "POST", data, bot.botCookies, true, "http://steamcommunity.com/chat/");
            try
            {
                List<string> history = new List<string>();
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                foreach (var item in json)
                {
                    ulong accountId = item.m_unAccountID;
                    ulong timestamp = item.m_tsTimestamp;
                    string message = item.m_strMessage;
                    DateTime time = UnixTimeStampToDateTime(timestamp);
                    if (accountId == userAccountId)
                    {
                        string date = "[" + time + "] ";
                        string name = bot.SteamFriends.GetFriendPersonaName(userSteamId) + ": ";
                        message = date + name + message + "\r\n";
                        history.Add(message);
                    }
                    else if (accountId == botAccountId)
                    {
                        string date = "[" + time + "] ";
                        string name = Bot.DisplayName + ": ";
                        message = date + name + message + "\r\n";
                        history.Add(message);                           
                    }
                }
                string historyMessage = "";
                foreach (var line in history)
                    historyMessage += line;
                text_log.Invoke((Action)(() =>
                {
                    var prevColor = text_log.SelectionColor;                    
                    text_log.Select(0, 0);
                    text_log.AppendText(historyMessage);
                    text_log.SelectionColor = Color.Gray;
                    text_log.Select(historyMessage.Length, text_log.Text.Length);
                    text_log.SelectionColor = prevColor;
                    text_log.ScrollToCaret();
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private DateTime UnixTimeStampToDateTime(ulong unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        [DllImport("user32")]
        private static extern bool HideCaret(IntPtr hWnd);
        public void HideCaret(RichTextBox textBox)
        {
            HideCaret(textBox.Handle);
        }

        public bool IsInGame()
        {
            try
            {
                string gameName = bot.SteamFriends.GetFriendGamePlayedName(userSteamId);
                if (!string.IsNullOrEmpty(gameName))
                    steam_status.Text += " (In-Game: " + gameName + ")";
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
                return bot.SteamFriends.GetFriendPersonaState(userSteamId) != SteamKit2.EPersonaState.Offline;
            }
            catch { return false; }
        }

        Bitmap GetHolder()
        {
            if (IsInGame())
            {
                inGame = true;
                return MistClient.Properties.Resources.IconIngame;
            }

            if (IsOnline())
            {
                inGame = false;
                return MistClient.Properties.Resources.IconOnline;
            }

            return MistClient.Properties.Resources.IconOffline;
        }

        Bitmap ComposeAvatar(string path)
        {
            Bitmap holder = GetHolder();
            Bitmap avatar = Util.GetAvatar(path);

            Graphics gfx = null;
            try
            {
                gfx = Graphics.FromImage(holder);
                gfx.DrawImage(avatar, new Rectangle(2, 2, avatarBox.Width - 4, avatarBox.Height - 4));
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
                    text_log.AppendText("[" + DateTime.Now + "] " + steam_name.Text + " has requested to trade with you.\r\n");
                    if (Friends.keepLog)
                        AppendLog(userSteamId, "[" + DateTime.Now + "] " + steam_name.Text + " has requested to trade with you.\r\n");
                    text_log.ScrollToCaret();
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
                    int duration = 3;
                    FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                    FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                    string title = steam_name.Text;
                    Notification toastNotification = new Notification(title, Util.GetColorFromPersonaState(bot, userSteamId), "has requested to trade with you.", duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox, new Action(() =>
                    {
                        Friends.chat.BringToFront();
                    }));
                    toastNotification.Show();
                    button_trade.Text = "Accept Trade Request";
                    break;
            }
        }

        public void UpdateButton(string message)
        {
            button_trade.Text = message;
            button_trade.Enabled = false;
        }

        public void UpdateChat(string date, string name, string message)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (text_log.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                text_log.Invoke((Action)(() => UpdateChat(date, name, message)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            Color prevColor = text_log.SelectionColor;
            text_log.SelectionColor = Color.RoyalBlue;
            if (inGame)
            {
                text_log.SelectionColor = Color.Green;
            }
            text_log.AppendText(date + name);
            text_log.SelectionColor = prevColor;
            message = message + "\r\n";
            text_log.AppendText(message);
            text_log.ScrollToCaret();
            chat_status.Text = "Last message received: " + DateTime.Now;
            if (Friends.keepLog)
                AppendLog(userSteamId, date + name + message);            
            if (!Chat.hasFocus)
            {
                Chat.Flash();
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

        public void UpdateChat(string text, bool notify)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (text_log.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                text_log.Invoke((Action)(() => UpdateChat(text, notify)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            text_log.AppendText(text);
            text_log.ScrollToCaret();            
            if (Friends.keepLog)
                AppendLog(userSteamId, text);
            if (notify)
            {
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
                bot.SteamFriends.SendChatMessage(userSteamId, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                Color prevColor = text_log.SelectionColor;
                text_log.SelectionColor = Color.RoyalBlue;
                if (bot.SteamFriends.GetFriendGamePlayed(bot.SteamUser.SteamID) != 0)
                {
                    text_log.SelectionColor = Color.Green;
                }
                string date = "[" + DateTime.Now + "] ";
                string name = Bot.DisplayName + ": ";
                text_log.AppendText(date + name);
                text_log.SelectionColor = Color.DarkGray;
                string message = text_input.Text + "\r\n";
                text_log.AppendText(message);
                text_log.ScrollToCaret();
                text_log.SelectionColor = prevColor;
                if (Friends.keepLog)
                    AppendLog(userSteamId, date + name + message);
                clear();
            }
        }
        
        private void text_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
            {
                text_input.SelectAll();
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (text_input.Text != "")
                {
                    e.Handled = true;
                    bot.SteamFriends.SendChatMessage(userSteamId, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                    Color prevColor = text_log.SelectionColor;
                    text_log.SelectionColor = Color.RoyalBlue;
                    if (bot.SteamFriends.GetFriendGamePlayed(bot.SteamUser.SteamID) != 0)
                    {
                        text_log.SelectionColor = Color.Green;
                    }                    
                    string date = "[" + DateTime.Now + "] ";
                    string name = Bot.DisplayName + ": ";
                    text_log.AppendText(date + name);
                    text_log.SelectionColor = Color.DarkGray;
                    string message = text_input.Text + "\r\n";
                    text_log.AppendText(message);
                    text_log.ScrollToCaret();
                    text_log.SelectionColor = prevColor;
                    if (Friends.keepLog)
                        AppendLog(userSteamId, date + name + message);
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
                    bot.SteamTrade.Trade(userSteamId);
                    tradeMode = 2;
                    button_trade.Text = "Cancel Trade Request";
                    UpdateChat("[" + DateTime.Now + "] You have sent " + steam_name.Text + " a trade request.\r\n", false);
                    if (Friends.keepLog)
                        AppendLog(userSteamId, "[" + DateTime.Now + "] You have sent " + steam_name.Text + " a trade request.\r\n");
                    break;
                case 2: // User sent trade request - "Cancel trade request"
                    bot.SteamTrade.CancelTrade(userSteamId);
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
                    foreach (var item in tab.Controls)
                    {
                        Friends.chat.chatTab = (ChatTab)item;
                    }
                    Friends.chat.page = tab;
                }
            }
            this.Font = new Font(this.Font, FontStyle.Regular);
            Friends.chat.Text = steam_name.Text + " - Chat";
        }

        private void ChatTab_Leave(object sender, EventArgs e)
        {
        }

        private void showBackpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Friend's backpack
            ShowBackpack showBP = new ShowBackpack(bot, userSteamId);
            showBP.Show();
            showBP.Activate();
        }

        private void steamRepStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chat.ViewSteamRepStatus(userSteamId);
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.ShowSteamProfile(bot, userSteamId);
        }

        private void ChatTab_Load(object sender, EventArgs e)
        {
        }

        private void steam_name_Click(object sender, EventArgs e)
        {
            steam_name.Focus();
        }

        public static void AppendLog(ulong sid, string message)
        {
            string LogDirectory = Path.Combine(Application.StartupPath, "logs");
            if (!Directory.Exists(LogDirectory))
            {
                try
                {
                    Directory.CreateDirectory(LogDirectory); // try making the log directory
                    Console.WriteLine("Creating log directory.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to create log directory.\n{0}", ex.ToString());
                    return;
                }
            }
            string file = Path.Combine(Application.StartupPath, "logs", sid.ToString() + ".txt");
            File.AppendAllText(file, message);
        }

        private void checkrep_DoWork(object sender, DoWorkEventArgs e)
        {
            var status = Util.GetSteamRepStatus(this.userSteamId, true);
            if (status == "None" || status == "")
            {
                // No special rep
                this.Invoke((Action)(() =>
                {
                    string date = "[" + DateTime.Now + "] ";                    
                    Color prevColor = text_log.SelectionColor;
                    text_log.SelectionColor = Color.RoyalBlue;
                    text_log.AppendText(date);
                    text_log.SelectionColor = prevColor;
                    string message = "[SteamRep] This user has no special reputation. Remember to always be cautious when trading.\r\n";
                    text_log.AppendText(message);
                    text_log.ScrollToCaret();
                    text_log.Select(text_log.Text.Length, 0);
                        text_log.SelectionColor = prevColor;
                }));
            }
            else
            {
                // Special rep
                this.Invoke((Action)(() =>
                {                    
                    if (status.Contains("SCAMMER"))
                    {
                        string date = "[" + DateTime.Now + "] ";
                        Color prevColor = text_log.SelectionColor;
                        text_log.SelectionColor = Color.RoyalBlue;
                        text_log.AppendText(date);
                        text_log.SelectionColor = Color.Red;
                        string message = "[SteamRep] WARNING: This user has been marked as a scammer on SteamRep with the following tags: "
                            + status + ". Be careful!\r\n";
                        text_log.AppendText(message);
                        text_log.ScrollToCaret();
                        text_log.Select(text_log.Text.Length, 0);
                        text_log.SelectionColor = prevColor;
                    }
                    else
                    {
                        string date = "[" + DateTime.Now + "] ";
                        Color prevColor = text_log.SelectionColor;
                        text_log.SelectionColor = Color.RoyalBlue;
                        text_log.AppendText(date);
                        text_log.SelectionColor = Color.Green;
                        string message = "[SteamRep] This user has special reputation, with tags: "
                            + status + ".\r\n";
                        text_log.AppendText(message);
                        text_log.ScrollToCaret();
                        text_log.Select(text_log.Text.Length, 0);
                        text_log.SelectionColor = prevColor;
                    }
                }));
            }
        }

        private void status_update_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!status_update.CancellationPending)
            {
                try
                {
                    this.steam_name.Text = bot.SteamFriends.GetFriendPersonaName(userSteamId);
                    this.steam_status.Text = bot.SteamFriends.GetFriendPersonaState(userSteamId).ToString();
                    SteamKit2.SteamID SteamID = userSteamId;
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
                    if (this.steam_status.Text != prevStatus)
                    {
                        UpdateChat("[" + DateTime.Now + "] " + steam_name.Text + " is now " + steam_status.Text + ".\r\n", false);
                        if (Friends.keepLog)
                            AppendLog(userSteamId, "[" + DateTime.Now + "] " + steam_name.Text + " is now " + steam_status.Text + ".\r\n");
                        prevStatus = this.steam_status.Text;
                    }
                    if (this.steam_name.Text != prevName)
                    {
                        UpdateChat("[" + DateTime.Now + "] " + prevName + " has changed their name to " + steam_name.Text + ".\r\n", false);
                        if (Friends.keepLog)
                            AppendLog(userSteamId, "[" + DateTime.Now + "] " + prevName + " has changed their name to " + steam_name.Text + ".\r\n");
                        ListFriends.UpdateName(userSteamId, steam_name.Text);
                        prevName = this.steam_name.Text;
                    }
                    System.Threading.Thread.Sleep(2000);
                }
                catch
                {

                }
            }
        }

        private void viewChatLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = bot.SteamFriends.GetFriendPersonaName(userSteamId);
            string logDir = Path.Combine(Application.StartupPath, "logs");
            string file = Path.Combine(logDir, userSteamId.ToString() + ".txt");
            if (!File.Exists(file))
            {
                ChatLog chatLog = new ChatLog(selected, userSteamId.ToString());
                chatLog.Show();
                chatLog.Activate();
            }
            else
            {
                string[] log = Util.ReadAllLines(file);
                StringBuilder sb = new StringBuilder();
                foreach (string line in log)
                {
                    sb.Append(line + Environment.NewLine);
                }
                ChatLog chatLog = new ChatLog(selected, userSteamId.ToString(), sb.ToString());
                chatLog.Show();
                chatLog.Activate();
            }
        }

        private void text_log_Click(object sender, EventArgs e)
        {
            HideCaret(text_log.Handle);
        }

        private void text_log_DoubleClick(object sender, EventArgs e)
        {
            HideCaret(text_log.Handle);
        }

        private void removeFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = bot.SteamFriends.GetFriendPersonaName(userSteamId);
            var dr = MetroFramework.MetroMessageBox.Show(this, "Are you sure you want to remove " + name + "?",
                    "Remove Friend",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                ListFriends.Remove(userSteamId);
                bot.friends.Remove(userSteamId);
                bot.SteamFriends.RemoveFriend(userSteamId);
                UpdateChat("[" + DateTime.Now + "] You have removed " + steam_name.Text + " from your friends list.\r\n", false);
                if (Friends.keepLog)
                    AppendLog(userSteamId, "[" + DateTime.Now + "] You have removed " + steam_name.Text + " from your friends list.\r\n");
                MetroFramework.MetroMessageBox.Show(this, "You have removed " + name + ".",
                        "Remove Friend",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void text_log_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", e.LinkText);
        }
    }
}
