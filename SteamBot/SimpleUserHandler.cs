using SteamKit2;
using System.Collections.Generic;
using SteamTrade;
using System.Threading;
using System;
using MistClient;
using System.Windows.Forms;
using System.IO;
using ToastNotifications;

namespace SteamBot
{
    public class SimpleUserHandler : UserHandler
    {
        ShowTrade_Web ShowTradeWeb;

        public SimpleUserHandler(Bot bot, SteamID sid) : base(bot, sid) { }

        public override bool OnGroupAdd()
        {
            return false;
        }

        public override void OnLoginCompleted()
        {

        }

        public override void OnTradeSuccess()
        {
            
        }

        public override void SetChatStatus(string message)
        {
            if (Friends.chat_opened)
            {
                Bot.main.Invoke((Action)(() =>
                {
                    foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                    {
                        if (tab.Text == Bot.SteamFriends.GetFriendPersonaName(OtherSID))
                        {
                            foreach (var item in tab.Controls)
                            {
                                Friends.chat.chatTab = (ChatTab)item;
                            }
                            tab.Invoke((Action)(() =>
                            {
                                Friends.chat.chatTab.chat_status.Text = message;
                            }));
                            return;
                        }
                    }
                }));
            }
        }

        public override void SetStatus(EPersonaState state)
        {
            if (Friends.chat_opened)
            {
                Bot.main.Invoke((Action)(() =>
                {
                    foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                    {
                        if (tab.Text == Bot.SteamFriends.GetFriendPersonaName(OtherSID))
                        {
                            foreach (var item in tab.Controls)
                            {
                                Friends.chat.chatTab = (ChatTab)item;
                            }
                            tab.Invoke((Action)(() =>
                            {
                                Friends.chat.chatTab.steam_status.Text = state.ToString();
                            }));
                            return;
                        }
                    }
                    
                }));
            }
        }

        public override void SendTradeError(string message)
        {
            string selected = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
            ulong sid = OtherSID;
            Bot.main.Invoke((Action)(() =>
            {
                if (!Friends.chat_opened)
                {
                    Friends.chat = new Chat(Bot);
                    Friends.chat.AddChat(selected, sid);
                    Friends.chat.Show();
                    Friends.chat_opened = true;
                    Friends.chat.Flash();
                    DisplayChatNotify(message);
                }
                else
                {
                    bool found = false;
                    try
                    {
                        foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                        {
                            if (tab.Text == selected)
                            {
                                foreach (var item in tab.Controls)
                                {
                                    Friends.chat.chatTab = (ChatTab)item;
                                }
                                Friends.chat.ChatTabControl.SelectedTab = tab;
                                Friends.chat.Show();
                                Friends.chat.Flash();
                                found = true;
                                tab.Invoke((Action)(() =>
                                {
                                    DisplayChatNotify(message);
                                }));
                                break;
                            }
                        }
                        if (!found)
                        {
                            Friends.chat.AddChat(selected, sid);
                            Friends.chat.Show();
                            Friends.chat.Flash();
                            DisplayChatNotify(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }));
            
        }

        void DisplayChatNotify(string message)
        {
            Bot.main.Invoke((Action)(() =>
            {
                foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                {
                    if (tab.Text == Bot.SteamFriends.GetFriendPersonaName(OtherSID))
                    {
                        foreach (var item in tab.Controls)
                        {
                            Friends.chat.chatTab = (ChatTab)item;
                        }
                        tab.Invoke((Action)(() =>
                        {
                            if (message == "had asked to trade with you, but has cancelled their request.")
                            {                                
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n", true);
                            }
                            if (message == "Lost connection to Steam. Reconnecting as soon as possible...")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "has declined your trade request.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n", true);
                            if (message == "An error has occurred in sending the trade request.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "You are already in a trade so you cannot trade someone else.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "You cannot trade the other user because they are already in trade with someone else.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "did not respond to the trade request.")
                            {
                                if (Friends.chat.chatTab.otherSentTrade)
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " had asked to trade with you, but you did not respond in time." + "\r\n", true);
                                    Friends.chat.chatTab.otherSentTrade = false;
                                }
                                else
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n", true);
                                }
                            }
                            if (message == "It is too soon to send a new trade request. Try again later.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "You are trade-banned and cannot trade.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "You cannot trade with this person because they are trade-banned.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (message == "Trade failed to initialize because either you or the user are not logged in.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n", false);
                            if (Friends.chat_opened)
                                Friends.chat.chatTab.TradeButtonMode(1);
                        }));
                        return;
                    }
                }

            }));
            
        }

        public override bool OpenChat(SteamID steamId)
        {
            string selected = Bot.SteamFriends.GetFriendPersonaName(steamId);
            if (!Friends.chat_opened)
            {
                Friends.chat = new Chat(Bot);
                Friends.chat.AddChat(selected, steamId);
                Friends.chat.Show();
                Friends.chat.Flash();
                Friends.chat_opened = true;
                return true;
            }
            else
            {
                bool found = false;
                try
                {
                    foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                    {
                        if ((SteamID)tab.Tag == steamId)
                        {                            
                            Friends.chat.Show();
                            Friends.chat.Flash();
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        Friends.chat.AddChat(selected, steamId);
                        Friends.chat.Show();
                        Friends.chat.Flash();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return false;
            }
        }

        public override bool OnFriendAdd()
        {
            return false;
        }

        public override void OnFriendRemove() { }

        public override void OnMessage(string message, EChatEntryType type)
        {
            Bot.main.Invoke((Action)(() =>
            {
                var other = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                var opened = OpenChat(OtherSID);
                string date = "[" + DateTime.Now + "] ";
                string name = other + ": ";
                foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                {
                    if (tab.Text == other)
                    {
                        foreach (var item in tab.Controls)
                        {
                            Friends.chat.chatTab = (ChatTab)item;
                        }
                    }
                }
                int islink;
                islink = 0;
                if (message.Contains("http://") || (message.Contains("https://")) || (message.Contains("www.")) || (message.Contains("ftp.")))
                {
                    string[] stan = message.Split(' ');
                    foreach (string word in stan)
                    {
                        if (word.Contains("http://") || (word.Contains("https://")) || (word.Contains("www.")) || (word.Contains("ftp.")))
                        {
                            if (word.Contains("."))
                            {
                                islink = 1;
                            }
                        }
                    }
                }
                if (islink == 1)
                {
                    Friends.chat.chatTab.UpdateChat("[INFO] ", "WARNING: ", "Be cautious when clicking unknown links.");
                }
                Friends.chat.chatTab.UpdateChat(date, name, message);
                new Thread(() =>
                {
                    if (opened || !Chat.hasFocus)
                    {
                        int duration = 3;
                        FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                        FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                        string title = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                        Notification toastNotification = new Notification(title, Util.GetColorFromPersonaState(Bot, OtherSID), message, duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox, new Action(() =>
                        {
                            Friends.chat.BringToFront();
                        }));
                        Bot.main.Invoke((Action)(() =>
                        {
                            toastNotification.Show();
                        }));
                    }
                }).Start();
            }));
        }

        public override void SendTradeState(uint tradeID)
        {
            string name = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
            Bot.main.Invoke((Action)(() =>
            {
                if (!Friends.chat_opened)
                {
                    Friends.chat = new Chat(Bot);
                    Friends.chat.AddChat(name, OtherSID);
                    Friends.chat.Show();
                    Friends.chat_opened = true;
                    Friends.chat.Flash();
                    Friends.chat.chatTab.TradeButtonMode(3, tradeID);
                    Friends.chat.chatTab.otherSentTrade = true;
                }
                else
                {
                    bool found = false;
                    try
                    {
                        foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                        {
                            Console.WriteLine("Looking at " + tab.Text);
                            if (tab.Text == name)
                            {
                                foreach (var item in tab.Controls)
                                {
                                    Friends.chat.chatTab = (ChatTab)item;
                                }
                                Friends.chat.ChatTabControl.SelectedTab = tab;
                                Friends.chat.Show();
                                Friends.chat.Flash();
                                Friends.chat.chatTab.TradeButtonMode(3, tradeID);
                                Friends.chat.chatTab.otherSentTrade = true;
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Not found");
                            Friends.chat.AddChat(name, OtherSID);
                            Friends.chat.Show();
                            Friends.chat.Flash();
                            Friends.chat.chatTab.TradeButtonMode(3, tradeID);
                            Friends.chat.chatTab.otherSentTrade = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }));
        }

        public override bool OnTradeRequest()
        {
            return false;
        }

        public override void OnTradeError(string error)
        {
            string name = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
            Bot.main.Invoke((Action)(() =>
            {
                try
                {
                    base.OnTradeClose();
                }
                catch (Exception ex)
                {
                    Bot.Print(ex);
                }
                if (!Friends.chat_opened)
                {
                    Friends.chat = new Chat(Bot);
                    Friends.chat.AddChat(name, OtherSID);
                    Friends.chat.Show();
                    Friends.chat_opened = true;
                    Friends.chat.Flash();
                    Friends.chat.chatTab.TradeButtonMode(1);
                    if (error.Contains("cancelled"))
                    {
                        Friends.chat.Invoke((Action)(() =>
                        {
                            Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.\r\n", true);
                        }));
                    }
                    else
                    {
                        Friends.chat.Invoke((Action)(() =>
                        {
                            Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error + "\r\n", true);
                        }));
                    }
                }
                else
                {
                    bool found = false;
                    try
                    {
                        foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                        {
                            Console.WriteLine("Looking at " + tab.Text);
                            if (tab.Text == name)
                            {
                                foreach (var item in tab.Controls)
                                {
                                    Friends.chat.chatTab = (ChatTab)item;
                                }
                                Friends.chat.ChatTabControl.SelectedTab = tab;
                                Friends.chat.Show();
                                Friends.chat.Flash();
                                Friends.chat.chatTab.TradeButtonMode(1);
                                if (error.Contains("cancelled"))
                                {
                                    Friends.chat.Invoke((Action)(() =>
                                    {
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.\r\n", true);
                                    }));
                                }
                                else
                                {
                                    Friends.chat.Invoke((Action)(() =>
                                    {
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error, true);
                                    }));
                                }
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Not found");
                            Friends.chat.AddChat(name, OtherSID);
                            Friends.chat.Show();
                            Friends.chat.Flash();
                            Friends.chat.chatTab.TradeButtonMode(1);
                            if (error.Contains("cancelled"))
                            {
                                Friends.chat.Invoke((Action)(() =>
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.\r\n", true);
                                }));
                            }
                            else
                            {
                                Friends.chat.Invoke((Action)(() =>
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error + "\r\n", true);
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                
            }));
        }

        public override void OnTradeTimeout()
        {
            Bot.main.Invoke((Action)(() =>
            {
                try
                {
                    base.OnTradeClose();
                }
                catch (Exception ex)
                {
                    Bot.Print(ex);
                }
                foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                {
                    if (tab.Text == Bot.SteamFriends.GetFriendPersonaName(OtherSID))
                    {
                        foreach (var item in tab.Controls)
                        {
                            Friends.chat.chatTab = (ChatTab)item;
                        }
                    }
                }
                Friends.chat.Invoke((Action)(() =>
                {
                    if (Friends.chat_opened)
                    {
                        Friends.chat.chatTab.TradeButtonMode(1);
                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has closed.\r\n", false);
                    }
                }));
            }));
        }

        public override void OnTradeInit()
        {
            Bot.main.Invoke((Action)(() =>
            {
                ShowTradeWeb = new ShowTrade_Web(Bot);
                ShowTradeWeb.Show();
                ShowTradeWeb.Activate();
            }));
            ChatTab.AppendLog(OtherSID, "==========[TRADE STARTED]==========\r\n");            
            Bot.log.Success("Trade successfully initialized.");
            foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
            {
                if (tab.Text == Bot.SteamFriends.GetFriendPersonaName(OtherSID))
                {
                    foreach (var item in tab.Controls)
                    {
                        Friends.chat.chatTab = (ChatTab)item;
                    }
                }
            }
            Friends.chat.Invoke((Action)(() =>
            {
                Friends.chat.chatTab.UpdateButton("Currently in Trade");
                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " has accepted your trade request.\r\n", false);
            }));            
        }

        public override void OnTradeClose()
        {
            foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
            {
                if ((SteamID)tab.Tag == OtherSID)
                {
                    foreach (var item in tab.Controls)
                    {
                        Friends.chat.chatTab = (ChatTab)item;
                    }
                }
            }
            Bot.main.Invoke((Action)(() =>
            {
                if (Friends.chat_opened)
                {
                    Friends.chat.chatTab.TradeButtonMode(1);
                }
            }));
            base.OnTradeClose();
        }

        public override void OnTradeAddItem(GenericInventory.Inventory.Item inventoryItem)
        {
            Bot.main.Invoke((Action)(() =>
            {
                ChatTab.AppendLog(OtherSID, "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " added: " + inventoryItem.Name + "\r\n");
            }));     
        }

        public override void OnTradeRemoveItem(GenericInventory.Inventory.Item inventoryItem)
        {
            Bot.main.Invoke((Action)(() =>
            {
                ChatTab.AppendLog(OtherSID, "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " removed: " + inventoryItem.Name + "\r\n");
            }));           
        }

        public override void OnTradeMessage(string message)
        {
            Bot.main.Invoke((Action)(() =>
            {
                string send = Bot.SteamFriends.GetFriendPersonaName(OtherSID) + ": " + message + " [" + DateTime.Now.ToLongTimeString() + "]\r\n";
                ChatTab.AppendLog(OtherSID, "[Trade Chat] " + send);
                if (!ShowTradeWeb.Focused)
                {
                    int duration = 3;
                    FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                    FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                    string title = "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                    Notification toastNotification = new Notification(title, Util.GetColorFromPersonaState(Bot, OtherSID
                    ), message, duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox, new Action(() =>
                    {
                        ShowTradeWeb.BringToFront();
                    }));
                    toastNotification.Show();
                }
            }));
        }

        public override void OnTradeReady(bool ready)
        {
            Bot.main.Invoke((Action)(() =>
            {
                if (ready)
                {
                    ChatTab.AppendLog(OtherSID, "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " is ready. [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                }
                else
                {
                    ChatTab.AppendLog(OtherSID, "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " is not ready. [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                }
            }));
        }

        public override void OnTradeAccept()
        {
            Bot.otherAccepted = true;
            OnTradeClose();
        }
    }
}
