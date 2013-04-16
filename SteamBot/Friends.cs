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
using SteamKit2;
using System.Collections;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class Friends : MetroForm
    {
        public static bool chat_opened = false;
        public static Chat chat;
        public static bool keepLog = true;
        SteamBot.Bot bot;
        static int TimerInterval = 30000;
        System.Timers.Timer refreshTimer = new System.Timers.Timer(TimerInterval);
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public byte[] AvatarHash { get; set; } // checking if update is necessary
        public static string mist_ver = Application.ProductVersion.Remove(Application.ProductVersion.LastIndexOf('.'));
        int form_friendsHeight;
        int form_friendreqHeight;
        bool minimizeToTray = true;

        public static List<MetroFramework.Components.MetroStyleManager> globalThemeManager = new List<MetroFramework.Components.MetroStyleManager>();
        public static MetroFramework.Components.MetroStyleManager globalStyleManager = new MetroFramework.Components.MetroStyleManager();

        public Friends(SteamBot.Bot bot, string username)
        {
            InitializeComponent();
            Util.LoadTheme(metroStyleManager1);
            this.Text = "Friends - Mist v" + mist_ver;
            this.steam_name.Text = username;
            this.bot = bot;
            this.steam_name.ContextMenuStrip = menu_status;
            this.steam_status.ContextMenuStrip = menu_status;
            this.label1.ContextMenuStrip = menu_status;
            this.minimizeToTrayToolStripMenuItem.Checked = Properties.Settings.Default.MinimizeToTray;
            logConversationsToolStripMenuItem.Checked = Properties.Settings.Default.KeepLog;
            keepLog = logConversationsToolStripMenuItem.Checked;
            ListFriends.friends = this;
            form_friendsHeight = friends_list.Height;
            form_friendreqHeight = list_friendreq.Height;

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
            trayIcon.Visible = false;

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
            trayIcon.Visible = false;
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
            if (Application.OpenForms.Count < 1)
            {
                Friends friends = new Friends(bot, steam_name.Text);
                friends.Show();
                friends.Activate();
            }
            friends_list.RefreshObjects(ListFriends.Get());
            Console.WriteLine("Friends list refreshed.");
        }

        private void friends_list_ItemActivate(object sender, EventArgs e)
        {
            bot.main.Invoke((Action)(() =>
            {
                if (friends_list.SelectedItem != null)
                {
                    ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                    string selected = bot.SteamFriends.GetFriendPersonaName(sid);
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
                                break;
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
            friends_list.SetObjects(collection, true);
        }

        public void RefreshObjects(IList modelObjects)
        {
            friends_list.RefreshObjects(modelObjects);
        }

        public void RefreshObject(object modelObject)
        {
            friends_list.RefreshObject(modelObject);
        }

        private void Friends_FormClosed(object sender, FormClosedEventArgs e)
        {
            trayIcon.Icon = null;
            trayIcon.Visible = false;
            trayIcon.Dispose();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            if (globalStyleManager.Theme == MetroFramework.MetroThemeStyle.Dark)
                label1.ForeColor = Color.WhiteSmoke;
            else
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

        private void label_addfriend_Click(object sender, EventArgs e)
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
            ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
            ShowBackpack showBP = new ShowBackpack(bot, sid);
            showBP.Show();
            showBP.Activate();            
        }

        private void openChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.main.Invoke((Action)(() =>
            {
                if (friends_list.SelectedItem != null)
                {
                    ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                    string selected = bot.SteamFriends.GetFriendPersonaName(sid);
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
                if (friends_list.SelectedItem != null)
                {
                    ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                    string selected = bot.SteamFriends.GetFriendPersonaName(sid);
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
                                        chat.chatTab = (ChatTab)item;
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
            if (friends_list.SelectedItem != null)
            {
                ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                bot.SteamFriends.RemoveFriend(sid);
                bot.friends.Remove(sid);
                ListFriends.Remove(sid);
                friends_list.RemoveObject(friends_list.SelectedItem.RowObject);
                MessageBox.Show("You have removed " + bot.SteamFriends.GetFriendPersonaName(sid),
                        "Remove Friend",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void blockFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
            string selected = bot.SteamFriends.GetFriendPersonaName(sid);
            bot.SteamFriends.IgnoreFriend(sid);
            MessageBox.Show("You have blocked " + selected,
                    "Block Friend",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
        }

        

        private void steamRepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is a proxy for SteamRep's beta API. Not recommended for heavy/wide usage.
            try
            {
                if (friends_list.SelectedItem.Text != null)
                {
                    ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                    string url = "http://api.steamrep.org/profiles/" + sid;
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
            }
            catch
            {

            }
        }        

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Icon = null;
            trayIcon.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private void Friends_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (minimizeToTray)
            {
                e.Cancel = true;
                Visible = false;
                ShowInTaskbar = false;
                if (trayIcon != null)
                {
                    trayIcon.Visible = true;
                    trayIcon.ShowBalloonTip(5000, "Mist has been minimized to tray", "To restore Mist, double-click the tray icon.", ToolTipIcon.Info);
                }
            }
            else
            {
                OnExit(sender, e);
            }
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
            string url = "http://www.thectscommunity.com/dev/update_check.php";
            string response = Util.HTTPRequest(url);
            if (response != "")
            {
                string latestVer = Util.ParseBetween(response, "<version>", "</version>");                                
                if (latestVer != Friends.mist_ver)
                {
                    string changelog = Util.ParseBetween(response, "<changelog>", "</changelog>");
                    Updater updater = new Updater(latestVer, changelog, bot.log);
                    updater.ShowDialog();
                    updater.Activate();
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
            else
            {
                MessageBox.Show("Failed to connect to the update server! Please try again later.",
                        "Update Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }
        }

        public void GrowFriends()
        {
            bot.main.Invoke((Action)(() =>
            {
                if (!list_friendreq.Visible)
                {
                    friends_list.Height = friends_list.Height + list_friendreq.Height;
                    friends_list.Location = new Point(friends_list.Left, friends_list.Top - list_friendreq.Height);
                }
            }));
        }

        public void ShrinkFriends()
        {
            bot.main.Invoke((Action)(() =>
            {
                if (list_friendreq.Visible)
                {
                    friends_list.Height = friends_list.Height - list_friendreq.Height;
                    friends_list.Location = new Point(friends_list.Left, friends_list.Top + list_friendreq.Height);
                }
            }));
        }

        private void Friends_Load(object sender, EventArgs e)
        {
            friends_list.Height = friends_list.Height + list_friendreq.Height;
            friends_list.Location = new Point(friends_list.Left, friends_list.Top - list_friendreq.Height);
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

        public void NotifyFriendRequest()
        {
            bot.main.Invoke((Action)(() =>
            {
                if (!list_friendreq.Visible)
                {
                    list_friendreq.Visible = true;
                    ShrinkFriends();
                }
            }));
        }

        public void HideFriendRequests()
        {
            bot.main.Invoke((Action)(() =>
            {
                if (list_friendreq.Visible)
                {
                    list_friendreq.Visible = false;
                    friends_list.Height = friends_list.Height + list_friendreq.Height;
                    friends_list.Location = new Point(friends_list.Left, friends_list.Top - list_friendreq.Height);
                    list_friendreq.SetObjects(ListFriendRequests.Get());
                }
            }));
        }

        private void viewGameInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (friends_list.SelectedItem != null)
            {
                ulong SteamID = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                string game = bot.SteamFriends.GetFriendGamePlayedName(SteamID);
                if (game != null)
                {
                    Console.WriteLine(game);
                    
                }
            }
        }

        private void viewProfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (friends_list.SelectedItem != null)
            {
                string base_url = "http://steamcommunity.com/profiles/";
                ulong SteamID = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
                base_url += SteamID.ToString();
                System.Diagnostics.Process.Start("explorer.exe", base_url);
            }
        }

        private void acceptFriendRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_friendreq.SelectedItem != null)
            {
                ulong SteamID = Convert.ToUInt64(column_friendreq_sid.GetValue(list_friendreq.SelectedItem.RowObject));
                bot.SteamFriends.AddFriend(SteamID);
                friends_list.AddObject(list_friendreq.SelectedItem.RowObject);
                list_friendreq.SelectedItem.Remove();
                ListFriendRequests.Remove(SteamID);
            }
        }

        private void denyFriendRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_friendreq.SelectedItem != null)
            {
                ulong SteamID = Convert.ToUInt64(column_friendreq_sid.GetValue(list_friendreq.SelectedItem.RowObject));
                bot.SteamFriends.IgnoreFriend(SteamID);
                list_friendreq.SelectedItem.Remove();
                ListFriendRequests.Remove(SteamID);
            }
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_friendreq.SelectedItem != null)
            {
                ulong SteamID = Convert.ToUInt64(column_friendreq_sid.GetValue(list_friendreq.SelectedItem.RowObject));
                string base_url = "http://steamcommunity.com/profiles/";
                base_url += SteamID.ToString();
                System.Diagnostics.Process.Start("explorer.exe", base_url);
            }
        }

        private void showBackpackToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (list_friendreq.SelectedItem != null)
            {
                ulong sid = Convert.ToUInt64(column_friendreq_sid.GetValue(list_friendreq.SelectedItem.RowObject));
                ShowBackpack showBP = new ShowBackpack(bot, sid);
                showBP.Show();
                showBP.Activate();
            }
        }

        private void steamRepStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is a proxy for SteamRep's beta API. Not recommended for heavy/wide usage.
            try
            {
                if (list_friendreq.SelectedItem != null)
                {
                    ulong sid = Convert.ToUInt64(column_friendreq_sid.GetValue(list_friendreq.SelectedItem.RowObject));
                    string url = "http://api.steamrep.org/profiles/" + sid;
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
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list_friendreq.Visible = !list_friendreq.Visible;
            if (list_friendreq.Visible)
            {
                ShrinkFriends();
            }
            else
            {
                GrowFriends();
            }
            list_friendreq.SetObjects(ListFriendRequests.Get());
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list_friendreq.SetObjects(ListFriendRequests.Get());
        }

        private void Friends_ResizeEnd(object sender, EventArgs e)
        {
            form_friendsHeight = friends_list.Height;
            form_friendreqHeight = list_friendreq.Height;
        }

        private void minimizeToTrayOnCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minimizeToTray = minimizeToTrayToolStripMenuItem.Checked;
            Properties.Settings.Default.MinimizeToTray = minimizeToTray;
            Properties.Settings.Default.Save();
        }

        private void text_search_Enter(object sender, EventArgs e)
        {
            if (text_search.Text == "Search")
            {
                text_search.Clear();
                text_search.Font = new Font(text_search.Font, FontStyle.Regular);
                text_search.ForeColor = SystemColors.WindowText;
            }
        }

        private void text_search_Leave(object sender, EventArgs e)
        {
            if (text_search.Text == "")
            {
                text_search.Font = new Font(text_search.Font, FontStyle.Italic);
                text_search.ForeColor = SystemColors.ControlDark;
                text_search.Text = "Search";
                this.friends_list.SetObjects(ListFriends.Get());
            }
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            if (text_search.Text == "")
                this.friends_list.SetObjects(ListFriends.Get());
            else
                this.friends_list.SetObjects(ListFriends.Get(text_search.Text));
        }

        private void viewChatLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (friends_list.SelectedItem != null)
            {
                ulong sid = Convert.ToUInt64(column_sid.GetValue(friends_list.SelectedItem.RowObject));
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

        private void logConversationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keepLog = logConversationsToolStripMenuItem.Checked;
            Properties.Settings.Default.KeepLog = keepLog;
            Properties.Settings.Default.Save();
        }

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                text_search.Clear();
                this.friends_list.SetObjects(ListFriends.Get());
            }
        }

        private void friends_list_BeforeSearching_1(object sender, BeforeSearchingEventArgs e)
        {
            e.Canceled = true;
        }

        private void list_friendreq_BeforeSearching(object sender, BeforeSearchingEventArgs e)
        {
            e.Canceled = true;
        }

        private void exitMistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minimizeToTray = false;
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }

        private void Friends_Leave(object sender, EventArgs e)
        {
            text_search.Text = "";
            this.friends_list.SetObjects(ListFriends.Get());
            label1.Select();
        }

        private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minimizeToTray = minimizeToTrayToolStripMenuItem.Checked;
            Properties.Settings.Default.MinimizeToTray = minimizeToTray;
            Properties.Settings.Default.Save();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globalStyleManager.Theme = MetroFramework.MetroThemeStyle.Light;
            try
            {
                Console.WriteLine(globalStyleManager.Theme);
                Properties.Settings.Default.Theme = globalStyleManager.Theme.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            RefreshTheme();
            menu_status.Close();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globalStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            Properties.Settings.Default.Theme = globalStyleManager.Theme.ToString();
            Properties.Settings.Default.Save();
            RefreshTheme();
            menu_status.Close();
        }

        private void setColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StyleChooser styleChooser = new StyleChooser("Light");
            styleChooser.ShowDialog();
            styleChooser.Activate();
        }

        private void setColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StyleChooser styleChooser = new StyleChooser("Dark");
            styleChooser.ShowDialog();
            styleChooser.Activate();
        }

        public static void RefreshTheme()
        {
            foreach (var item in globalThemeManager)
            {
                item.Theme = globalStyleManager.Theme;
                item.Style = globalStyleManager.Style;
            }
        }
    }
}
