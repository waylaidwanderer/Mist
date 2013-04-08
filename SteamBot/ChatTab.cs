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

namespace MistClient
{
    public partial class ChatTab : MetroUserControl
    {
        ulong sid;
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
            this.sid = sid;
            this.bot = bot;
            Util.LoadTheme(metroStyleManager1);
            this.Theme = Friends.globalStyleManager.Theme;
            this.Style = Friends.globalStyleManager.Style;
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
            checkrep.RunWorkerAsync();
            status_update.RunWorkerAsync();
            text_input.Focus();
        }

        public bool IsInGame()
        {
            try
            {
                string gameName = bot.SteamFriends.GetFriendGamePlayedName(sid);
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
                return bot.SteamFriends.GetFriendPersonaState(sid) != SteamKit2.EPersonaState.Offline;
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
                    text_log.AppendText("[" + DateTime.Now + "] " + steam_name.Text + " has requested to trade with you.\r\n");
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
                    Notification toastNotification = new Notification(title, "has requested to trade with you.", duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox);
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
                AppendLog(sid, date + name + message);
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
                AppendLog(sid, text);
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
                bot.SteamFriends.SendChatMessage(sid, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                Color prevColor = text_log.SelectionColor;
                text_log.SelectionColor = Color.RoyalBlue;
                if (bot.SteamFriends.GetFriendGamePlayed(bot.SteamUser.SteamID) != 0)
                {
                    text_log.SelectionColor = Color.Green;
                }
                string date = "[" + DateTime.Now + "] ";
                string name = Bot.displayName + ": ";
                text_log.AppendText(date + name);
                text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                string message = text_input.Text + "\r\n";
                text_log.AppendText(message);
                text_log.ScrollToCaret();
                text_log.SelectionColor = prevColor;
                if (Friends.keepLog)
                    AppendLog(sid, date + name + message);
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
                    bot.SteamFriends.SendChatMessage(sid, SteamKit2.EChatEntryType.ChatMsg, text_input.Text);
                    Color prevColor = text_log.SelectionColor;
                    text_log.SelectionColor = Color.RoyalBlue;
                    if (bot.SteamFriends.GetFriendGamePlayed(bot.SteamUser.SteamID) != 0)
                    {
                        text_log.SelectionColor = Color.Green;
                    }                    
                    string date = "[" + DateTime.Now + "] ";
                    string name = Bot.displayName + ": ";
                    text_log.AppendText(date + name);
                    text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                    string message = text_input.Text + "\r\n";
                    text_log.AppendText(message);
                    text_log.ScrollToCaret();
                    text_log.SelectionColor = prevColor;
                    if (Friends.keepLog)
                        AppendLog(sid, date + name + message);
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
                    UpdateChat("[" + DateTime.Now + "] You have sent " + steam_name.Text + " a trade request.\r\n", false);
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
            ShowBackpack showBP = new ShowBackpack(bot, sid);
            showBP.Show();
            showBP.Activate();
        }

        private void steamRepStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is a beta feature of SteamRep. Mattie! has informed me that this feature shouldn't be depended on and may die in the future.
            // Don't depend on this.
            try
            {
                string url = "http://steamrep.com/api/beta/reputation/" + sid;
                for (int count = 0; count < 2; count++)
                {
                    string response = Util.HTTPRequest(url);
                    if (response != "")
                    {
                        string status = Util.ParseBetween(response, "<reputation>", "</reputation>");
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
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string base_url = "http://steamcommunity.com/profiles/";
            base_url += sid.ToString();
            System.Diagnostics.Process.Start("explorer.exe", base_url);
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
            string file = Path.Combine(Application.StartupPath, "steamrep.cache");
            if (!File.Exists(file))
            {
                File.Create(file);
            }
            else
            {
                bool found = false;
                //string toFind = "SteamID: 7691248515251; Rep: None; LC: Date.";
                foreach (var line in File.ReadAllLines(file))
                {
                    if (line.Contains(this.sid.ToString()))
                    {
                        found = true;
                        string lastChecked = Util.ParseBetween(line, "Date:", ".");
                        DateTime dateLast = Convert.ToDateTime(lastChecked);
                        DateTime dateNow = DateTime.Now;
                        TimeSpan difference = dateNow - dateLast;
                        if (difference.TotalDays > 1)
                        {
                            // Data last pulled over a day ago, so let's update the SR cache
                            string url = "http://api.steamrep.org/profiles/" + sid;
                            for (int count = 0; count < 5; count++)
                            {
                                string response = Util.HTTPRequest(url);
                                if (response != "")
                                {
                                    string status = Util.ParseBetween(response, "<reputation>", "</reputation>");
                                    if (status == "")
                                    {
                                        // No special rep
                                        UpdateSRCache(sid.ToString(), "None");
                                        this.Invoke((Action)(() =>
                                        {
                                            string date = "[" + DateTime.Now + "] ";
                                            Color prevColor = text_log.SelectionColor;
                                            text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                            text_log.AppendText(date);
                                            text_log.SelectionColor = prevColor;
                                            string message = "[SteamRep] This user has no special reputation. Remember to always be cautious when trading.\r\n";
                                            text_log.AppendText(message);
                                            text_log.ScrollToCaret();
                                        }));
                                    }
                                    else
                                    {
                                        // Special rep
                                        UpdateSRCache(sid.ToString(), status);
                                        this.Invoke((Action)(() =>
                                        {
                                            if (status.Contains("SCAMMER"))
                                            {
                                                string date = "[" + DateTime.Now + "] ";
                                                Color prevColor = text_log.SelectionColor;
                                                text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                                text_log.AppendText(date);
                                                text_log.SelectionColor = Color.Red;
                                                string message = "[SteamRep] WARNING: This user has been marked as a scammer on SteamRep with the following tags: "
                                                    + status + ". Be careful!\r\n";
                                                text_log.AppendText(message);
                                                text_log.ScrollToCaret();
                                                text_log.SelectionColor = prevColor;
                                            }
                                            else
                                            {
                                                string date = "[" + DateTime.Now + "] ";
                                                Color prevColor = text_log.SelectionColor;
                                                text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                                text_log.AppendText(date);
                                                text_log.SelectionColor = Color.Green;
                                                string message = "[SteamRep] This user has special reputation, with tags: "
                                                    + status + ".\r\n";
                                                text_log.AppendText(message);
                                                text_log.ScrollToCaret();
                                                text_log.SelectionColor = prevColor;
                                            }
                                        }));
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            string result = Util.ParseBetween(line, "Rep:", ";");
                            if (result == "None")
                            {
                                this.Invoke((Action)(() =>
                                {
                                    string date = "[" + DateTime.Now + "] ";
                                    Color prevColor = text_log.SelectionColor;
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                    text_log.AppendText(date);
                                    text_log.SelectionColor = prevColor;
                                    string message = "[SteamRep] This user has no special reputation. Remember to always be cautious when trading.\r\n";
                                    text_log.AppendText(message);
                                    text_log.ScrollToCaret();
                                }));
                            }
                            else if (result.Contains("SCAMMER"))
                            {
                                this.Invoke((Action)(() =>
                                {
                                    string date = "[" + DateTime.Now + "] ";
                                    Color prevColor = text_log.SelectionColor;
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                    text_log.AppendText(date);
                                    text_log.SelectionColor = Color.Red;
                                    string message = "[SteamRep] WARNING: This user has been marked as a scammer on SteamRep with the following tags: "
                                        + result + ". Be careful!\r\n";
                                    text_log.AppendText(message);
                                    text_log.ScrollToCaret();
                                    text_log.SelectionColor = prevColor;
                                }));
                            }
                            else
                            {
                                this.Invoke((Action)(() =>
                                {
                                    string date = "[" + DateTime.Now + "] ";
                                    Color prevColor = text_log.SelectionColor;
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                    text_log.AppendText(date);
                                    text_log.SelectionColor = Color.Green;
                                    string message = "[SteamRep] This user has special reputation, with tags: "
                                        + result + ".\r\n";
                                    text_log.AppendText(message);
                                    text_log.ScrollToCaret();
                                    text_log.SelectionColor = prevColor;
                                }));
                            }
                        }
                        break;
                    }
                }
                if (!found)
                {
                    // This is a new user, so we should add them to the cache
                    string url = "http://api.steamrep.org/profiles/" + sid;
                    for (int count = 0; count < 2; count++)
                    {
                        string response = Util.HTTPRequest(url);
                        if (response != "")
                        {
                            string status = Util.ParseBetween(response, "<reputation>", "</reputation>");
                            if (status == "")
                            {
                                // No special rep
                                status = "None";
                                string add = "SteamID:" + sid + ";Rep:" + status + ";Date:" + DateTime.Now + ".\r\n";
                                File.AppendAllText(file, add);
                                this.Invoke((Action)(() =>
                                {
                                    string date = "[" + DateTime.Now + "] ";
                                    Color prevColor = text_log.SelectionColor;
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                    text_log.AppendText(date);
                                    text_log.SelectionColor = prevColor;
                                    string message = "[SteamRep] This user has no special reputation. Remember to always be cautious when trading.\r\n";
                                    text_log.AppendText(message);
                                    text_log.ScrollToCaret();
                                }));
                            }
                            else
                            {
                                // Special rep
                                string add = "SteamID:" + sid + ";Rep:" + status + ";Date:" + DateTime.Now + ".\r\n";
                                File.AppendAllText(file, add);
                                if (status.Contains("SCAMMER"))
                                {
                                    this.Invoke((Action)(() =>
                                    {
                                        string date = "[" + DateTime.Now + "] ";
                                        Color prevColor = text_log.SelectionColor;
                                        text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                        text_log.AppendText(date);
                                        text_log.SelectionColor = Color.Red;
                                        string message = "[SteamRep] WARNING: This user has been marked as a scammer on SteamRep with the following tags: "
                                            + status + ". Be careful!\r\n";
                                        text_log.AppendText(message);
                                        text_log.ScrollToCaret();
                                        text_log.SelectionColor = prevColor;
                                    }));
                                }
                                else
                                {
                                    this.Invoke((Action)(() =>
                                    {
                                        string date = "[" + DateTime.Now + "] ";
                                        Color prevColor = text_log.SelectionColor;
                                        text_log.SelectionColor = ColorTranslator.FromHtml("#2E2E2E");
                                        text_log.AppendText(date);
                                        text_log.SelectionColor = Color.Green;
                                        string message = "[SteamRep] This user has special reputation, with tags: "
                                            + status + ".\r\n";
                                        text_log.AppendText(message);
                                        text_log.ScrollToCaret();
                                        text_log.SelectionColor = prevColor;
                                    }));
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        void UpdateSRCache(string sid, string rep)
        {
            StringBuilder newFile = new StringBuilder();
            string temp = "";
            string filePath = Path.Combine(Application.StartupPath, "steamrep.cache");
            string[] file = File.ReadAllLines(filePath);
            foreach (string line in file)
            {
                if (line.Contains(sid))
                {
                    string add = "SteamID:" + sid + ";Rep:" + rep + ";Date:" + DateTime.Now + ".\r\n";
                    temp = line.Replace(line, add);
                    newFile.Append(temp);
                    continue;
                }
                newFile.Append(line + "\r\n");
            }
            File.WriteAllText(filePath, newFile.ToString());
        }

        private void text_log_DoubleClick(object sender, EventArgs e)
        {
            text_log.Focus();
        }

        private void text_log_Click(object sender, EventArgs e)
        {
            this.steam_name_Click(sender, e);
        }

        private void status_update_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    this.steam_name.Text = bot.SteamFriends.GetFriendPersonaName(sid);
                    this.steam_status.Text = bot.SteamFriends.GetFriendPersonaState(sid).ToString();
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
                    if (this.steam_status.Text != prevStatus)
                    {
                        UpdateChat("[" + DateTime.Now + "] " + steam_name.Text + " is now " + steam_status.Text + ".\r\n", false);
                        prevStatus = this.steam_status.Text;
                    }
                    if (this.steam_name.Text != prevName)
                    {
                        UpdateChat("[" + DateTime.Now + "] " + prevName + " has changed their name to " + steam_name.Text + ".\r\n", false);
                        prevName = this.steam_name.Text;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {

                }
            }
        }

        private void viewChatLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = bot.SteamFriends.GetFriendPersonaName(sid);
            string logDir = Path.Combine(Application.StartupPath, "logs");
            string file = Path.Combine(logDir, sid.ToString() + ".txt");
            if (!File.Exists(file))
            {
                ChatLog chatLog = new ChatLog(selected, sid.ToString());
                chatLog.Show();
                chatLog.Activate();
            }
            else
            {
                string[] log = File.ReadAllLines(file);
                StringBuilder sb = new StringBuilder();
                foreach (string line in log)
                {
                    sb.Append(line + Environment.NewLine);
                }
                ChatLog chatLog = new ChatLog(selected, sid.ToString(), sb.ToString());
                chatLog.Show();
                chatLog.Activate();
            }
        }
    }
}
