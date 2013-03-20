using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MistClient
{
    public partial class Friends : Form
    {
        public static bool chat_opened = false;
        public static Chat chat;
        SteamBot.Bot bot;
        static int TimerInterval = 30000;
        System.Timers.Timer refreshTimer = new System.Timers.Timer(TimerInterval);
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public byte[] AvatarHash { get; set; } // checking if update is necessary
        string mist_ver = "1.0.0";

        public Friends(SteamBot.Bot bot, string username)
        {
            InitializeComponent();
            this.Text = "Friends - Mist v" + mist_ver;
            this.steam_name.Text = username;
            this.bot = bot;
            ListFriends.friends = this;
            this.steam_name.ContextMenuStrip = menu_status;
            this.steam_status.ContextMenuStrip = menu_status;
            this.label1.ContextMenuStrip = menu_status;
            refreshTimer.Interval = TimerInterval;
            refreshTimer.Elapsed += (sender, e) => OnTimerElapsed(sender, e);
            refreshTimer.Start();            

            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Show", OnTrayIconDoubleClick);
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Mist";
            Bitmap bmp = Properties.Resources.mist_icon;
            trayIcon.Icon = Icon.FromHandle(bmp.GetHicon());

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            trayIcon.DoubleClick += new System.EventHandler(this.OnTrayIconDoubleClick);
        }

        public bool IsInGame()
        {
            try
            {
                string gameName = bot.SteamFriends.GetFriendGamePlayedName(bot.SteamUser.SteamID);
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
                return bot.SteamFriends.GetPersonaState() != SteamKit2.EPersonaState.Offline;
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

        void OnTrayIconDoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true;
            this.Show();
            this.Activate();
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            byte[] avatarHash = bot.SteamFriends.GetFriendAvatar(bot.SteamUser.SteamID);
            bool validHash = avatarHash != null && !IsZeros(avatarHash);

            if ((AvatarHash == null && !validHash && avatarBox.Image != null) || (AvatarHash != null && AvatarHash.SequenceEqual(avatarHash)))
            {
                // avatar is already up to date, no operations necessary
            }
            else if (validHash)
            {
                AvatarHash = avatarHash;
                CDNCache.DownloadAvatar(bot.SteamUser.SteamID, avatarHash, AvatarDownloaded);
            }
            else
            {
                AvatarHash = null;
                avatarBox.Image = ComposeAvatar(null);
            }
            bot.LoadFriends();
            friends_list.SetObjects(ListFriends.Get());
            Console.WriteLine("Friends list refreshed.");
        }

        private void friends_list_ItemActivate(object sender, EventArgs e)
        {
            bot.main.Invoke((Action)(() =>
            {
                string selected = "";
                try
                {
                    selected = friends_list.SelectedItem.Text;
                }
                catch
                {
                    selected = null;
                }
                if (selected != null)
                {
                    ulong sid = ListFriends.GetSID(selected);
                    if (!chat_opened)
                    {
                        chat = new Chat(bot);
                        chat.AddChat(selected, sid);
                        chat.Show();
                        chat.Activate();
                        chat_opened = true;
                    }
                    else
                    {
                        bool found = false;
                        foreach (TabPage tab in chat.ChatTabControl.TabPages)
                        {
                            if (tab.Text == selected)
                            {
                                chat.ChatTabControl.SelectedTab = tab;
                                chat.Activate();
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            chat.AddChat(selected, sid);
                            chat.Activate();
                        }
                    }
                }
            }));
        }

        public void SetObject(System.Collections.IEnumerable collection)
        {
            friends_list.SetObjects(collection);
        }

        private void Friends_FormClosed(object sender, FormClosedEventArgs e)
        {
            trayIcon.Icon = null;
            trayIcon.Visible = false;
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ControlText;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ControlDarkDark;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            menu_status.Show(label1, Cursor.HotSpot.X + 4, Cursor.HotSpot.Y + 4);
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.Online);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void awayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.Away);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void busyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.Busy);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void lookingToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.LookingToPlay);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void lookingToTradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.LookingToTrade);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void snoozeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.Snooze);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.SteamFriends.SetPersonaState(SteamKit2.EPersonaState.Offline);
            this.steam_status.Text = bot.SteamFriends.GetPersonaState().ToString();
        }

        private void changeProfileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileName changeProfile = new ProfileName(bot);
            changeProfile.ShowDialog();
        }

        private void label_addfriend_MouseHover(object sender, EventArgs e)
        {
            label_addfriend.ForeColor = SystemColors.ControlText;
            label_addfriend2.ForeColor = SystemColors.ControlText;
        }

        private void label_addfriend_MouseLeave(object sender, EventArgs e)
        {
            label_addfriend.ForeColor = SystemColors.ControlDarkDark;
            label_addfriend2.ForeColor = SystemColors.ControlDarkDark;
        }

        private void label_addfriend2_MouseHover(object sender, EventArgs e)
        {
            label_addfriend.ForeColor = SystemColors.ControlText;
            label_addfriend2.ForeColor = SystemColors.ControlText;
        }

        private void label_addfriend2_MouseLeave(object sender, EventArgs e)
        {
            label_addfriend.ForeColor = SystemColors.ControlDarkDark;
            label_addfriend2.ForeColor = SystemColors.ControlDarkDark;
        }

        private void label_addfriend_Click(object sender, EventArgs e)
        {
            AddFriend addFriend = new AddFriend(bot);
            addFriend.ShowDialog();
        }

        private void label_addfriend2_Click(object sender, EventArgs e)
        {
            AddFriend addFriend = new AddFriend(bot);
            addFriend.ShowDialog();
        }

        private void friends_list_BeforeSearching(object sender, BeforeSearchingEventArgs e)
        {
            e.Canceled = true;
        }

        private void showBackpackToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Your backpack
            ulong sid = bot.SteamUser.SteamID;
            ShowBackpack showBP = new ShowBackpack(bot, sid);
            showBP.Show();
            showBP.Activate();
        }

        private void showBackpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Friend's backpack
            string selected = "";
            try
            {
                selected = friends_list.SelectedItem.Text;
            }
            catch
            {
                selected = null;
            }
            if (selected != null)
            {
                ulong sid = ListFriends.GetSID(selected);
                ShowBackpack showBP = new ShowBackpack(bot, sid);
                showBP.Show();
                showBP.Activate();
            }
        }

        private void openChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.main.Invoke((Action)(() =>
            {
                string selected = "";
                try
                {
                    selected = friends_list.SelectedItem.Text;
                }
                catch
                {
                    selected = null;
                }
                if (selected != null)
                {
                    ulong sid = ListFriends.GetSID(selected);
                    if (!chat_opened)
                    {
                        chat = new Chat(bot);
                        chat.AddChat(selected, sid);
                        chat.Show();
                        chat.Focus();
                        chat_opened = true;
                    }
                    else
                    {
                        bool found = false;
                        foreach (TabPage tab in chat.ChatTabControl.TabPages)
                        {
                            if (tab.Text == selected)
                            {
                                chat.ChatTabControl.SelectedTab = tab;
                                chat.Focus();
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            chat.AddChat(selected, sid);
                            chat.Focus();
                        }
                    }
                }
            }));
        }

        private void inviteToTradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.main.Invoke((Action)(() =>
            {
                string selected = "";
                try
                {
                    selected = friends_list.SelectedItem.Text;
                }
                catch
                {
                    selected = null;
                }
                if (selected != null)
                {
                    ulong sid = ListFriends.GetSID(selected);
                    if (!chat_opened)
                    {
                        chat = new Chat(bot);
                        chat.AddChat(selected, sid);
                        chat.Show();
                        chat.Focus();
                        chat_opened = true;
                        chat.chatTab.tradeClicked();
                    }
                    else
                    {
                        bool found = false;
                        foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                        {
                            if (tab.Text == selected)
                            {
                                found = true;
                                tab.Invoke((Action)(() =>
                                {
                                    foreach (var item in tab.Controls)
                                    {
                                        chat.chatTab = (ChatTab) item;
                                        chat.chatTab.tradeClicked();
                                    }
                                }));
                                return;
                            }
                        }
                        if (!found)
                        {
                            chat.AddChat(selected, sid);
                            chat.Focus();
                            chat.chatTab.tradeClicked();
                        }
                    }
                }
            }));
        }

        private void removeFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = "";
            try
            {
                selected = friends_list.SelectedItem.Text;
            }
            catch
            {
                selected = null;
            }
            if (selected != null)
            {
                ulong sid = ListFriends.GetSID(selected);
                bot.SteamFriends.RemoveFriend(sid);
                ListFriends.Remove(sid);
                friends_list.RemoveObject(friends_list.SelectedItem);
                MessageBox.Show("You have removed " + selected,
                        "Remove Friend",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void blockFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = "";
            try
            {
                selected = friends_list.SelectedItem.Text;
            }
            catch
            {
                selected = null;
            }
            if (selected != null)
            {
                ulong sid = ListFriends.GetSID(selected);
                bot.SteamFriends.IgnoreFriend(sid);
                MessageBox.Show("You have blocked " + selected,
                        "Block Friend",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
        }

        public static string HTTPRequest(string url)
        {
            var result = "";
            try
            {
                using (var webClient = new WebClient())
                {
                    using (var stream = webClient.OpenRead(url))
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var wtf = ex.Message;
            }

            return result;
        }

        public static string ParseBetween(string Subject, string Start, string End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        private void steamRepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (friends_list.SelectedItem.Text != null)
                {
                    string selected = friends_list.SelectedItem.Text;
                    ulong sid = ListFriends.GetSID(selected);
                    string url = "http://steamrep.com/api/beta/reputation/" + sid;
                    string response = HTTPRequest(url);
                    if (response != "")
                    {
                        string status = ParseBetween(response, "<reputation>", "</reputation>");
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
            }
            catch
            {

            }
        }        

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void Friends_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            trayIcon.ShowBalloonTip(5000, "Mist has been minimized to tray", "To restore Mist, double-click the tray icon.", ToolTipIcon.Info);
            e.Cancel = true;
        }

        private void aboutMistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mist is written by waylaidwanderer (http://steamcommunity.com/id/waylaidwanderer)."
                + "\nA large part of it was built using the underlying functions of SteamBot (https://github.com/Jessecar96/SteamBot/), and I thank all of SteamBot's contributors"
                + " for making Mist possible.",
                        "About",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "http://www.thectscommunity.com/dev/mist_ver.php";
            string response = HTTPRequest(url);
            if (response != "")
            {
                if (response != mist_ver)
                {
                    string message = "There is a new version of Mist available. Would you like to be taken to http://steamcommunity.com/groups/MistClient/discussions/0/810919057023360607/ for the latest release? Click No to simply close this message box.";
                    DialogResult choice = MessageBox.Show(new Form() { TopMost = true }, message,
                                    "Outdated",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);
                    switch (choice)
                    {
                        case DialogResult.Yes:
                            System.Diagnostics.Process.Start("http://steamcommunity.com/groups/MistClient/discussions/0/810919057023360607/");
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Congratulations, Mist is up-to-date! Thank you for using Mist :)",
                        "Latest Version",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void Friends_Load(object sender, EventArgs e)
        {
            byte[] avatarHash = bot.SteamFriends.GetFriendAvatar(bot.SteamUser.SteamID);
            bool validHash = avatarHash != null && !IsZeros(avatarHash);

            if ((AvatarHash == null && !validHash && avatarBox.Image != null) || (AvatarHash != null && AvatarHash.SequenceEqual(avatarHash)))
            {
                // avatar is already up to date, no operations necessary
            }
            else if (validHash)
            {
                AvatarHash = avatarHash;
                CDNCache.DownloadAvatar(bot.SteamUser.SteamID, avatarHash, AvatarDownloaded);
            }
            else
            {
                AvatarHash = null;
                avatarBox.Image = ComposeAvatar(null);
            }
        }
    }
}
