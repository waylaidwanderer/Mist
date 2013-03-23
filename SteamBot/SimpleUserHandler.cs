using SteamKit2;
using System.Collections.Generic;
using SteamTrade;
using System.Threading;
using System;
using MistClient;
using System.Windows.Forms;
using ToastNotifications;

namespace SteamBot
{
    public class SimpleUserHandler : UserHandler
    {
        ShowTrade ShowTrade;

        public SimpleUserHandler(Bot bot, SteamID sid) : base(bot, sid) { }

        public void SendMessage(string message)
        {
            if (message != "")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, EChatEntryType.ChatMsg, message);
            }
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
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n");
                            if (message == "Lost connection to Steam. Reconnecting as soon as possible...")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message);
                            if (message == "has declined your trade request.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n");
                            if (message == "An error has occurred in sending the trade request.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "You are already in a trade so you cannot trade someone else.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "You cannot trade the other user because they are already in trade with someone else.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "did not respond to the trade request.")
                            {
                                if (Friends.chat.chatTab.otherSentTrade)
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " had asked to trade with you, but you did not respond in time." + "\r\n");
                                    Friends.chat.chatTab.otherSentTrade = false;
                                }
                                else
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " " + message + "\r\n");
                                }
                            }
                            if (message == "It is too soon to send a new trade request. Try again later.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "You are trade-banned and cannot trade.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "You cannot trade with this person because they are trade-banned.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            if (message == "Trade failed to initialize because either you or the user are not logged in.")
                                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + message + "\r\n");
                            Friends.chat.chatTab.TradeButtonMode(1);
                        }));
                        return;
                    }
                }

            }));
            
        }

        public override void OpenChat(SteamID SID)
        {
            string selected = Bot.SteamFriends.GetFriendPersonaName(SID);
            ulong sid = SID;
            if (!Friends.chat_opened)
            {
                Friends.chat = new Chat(Bot);
                Friends.chat.AddChat(selected, sid);
                Friends.chat.Show();
                Friends.chat.Flash();
                Friends.chat_opened = true;
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
                            Friends.chat.Show();
                            Friends.chat.Flash();
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        Friends.chat.AddChat(selected, sid);
                        Friends.chat.Show();
                        Friends.chat.Flash();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public override bool OnFriendAdd()
        {
            return false;
        }

        public override void OnFriendRemove() { }

        public override void OnMessage(string message, EChatEntryType type)
        {
            if (Bot.main.InvokeRequired)
            {
                Bot.main.Invoke((Action)(() =>
                {
                    var other = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                    Console.WriteLine(OtherSID);
                    OpenChat(OtherSID);
                    string update = "[" + DateTime.Now + "] " + other + ": " + message + "\r\n";
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
                    Friends.chat.chatTab.UpdateChat(update);
                    if (!Chat.hasFocus)
                    {
                        int duration = 3;
                        FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                        FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                        string title = Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " says:";
                        Notification toastNotification = new Notification(title, message, duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox);
                        toastNotification.Show();
                    }
                }));
            }
            else
            {
                var other = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                OpenChat(OtherSID);
                string update = "[" + DateTime.Now + "] " + other + ": " + message + "\r\n";
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
                Friends.chat.chatTab.UpdateChat(update);
            }
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
                        Console.WriteLine("Trying");
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
                    Bot.main.Invoke((Action)(() =>
                    {
                        ShowTrade.Close();
                    }));
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
                            Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.\r\n");
                        }));
                    }
                    else
                    {
                        Friends.chat.Invoke((Action)(() =>
                        {
                            Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error + "\r\n");
                        }));
                    }
                }
                else
                {
                    bool found = false;
                    try
                    {
                        Console.WriteLine("Trying");
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
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.");
                                    }));
                                }
                                else
                                {
                                    Friends.chat.Invoke((Action)(() =>
                                    {
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error);
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
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has been cancelled.\r\n");
                                }));
                            }
                            else
                            {
                                Friends.chat.Invoke((Action)(() =>
                                {
                                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] Error: " + error + "\r\n");
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
                    ShowTrade.Close();
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
                    Friends.chat.chatTab.TradeButtonMode(1);
                    Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] The trade has expired.\r\n");
                }));
            }));
        }

        public override void OnTradeInit()
        {
            
            ShowTrade.itemsAdded = 0;
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
                Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " has accepted your trade request.\r\n");
            }));
            ShowTrade.loading = true;
            Bot.main.Invoke((Action)(() =>
            {
                ShowTrade.ClearAll();
            }));
            TradeCountInventory();
        }

        public override void OnTradeClose()
        {
            try
            {
                Friends.chat.Invoke((Action)(() =>
                {
                    ShowTrade.Close();
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Friends.chat.chatTab.TradeButtonMode(1);
            }));
            base.OnTradeClose();
        }

        public void TradeCountInventory()
        {
            Bot.main.Invoke((Action)(() =>
            {
                ShowTrade = new ShowTrade(Bot, Bot.SteamFriends.GetFriendPersonaName(OtherSID));
                ShowTrade.Show();
            }));
            // Let's count our inventory
            Thread loadInventory = new Thread(() =>
            {   
                Console.WriteLine("Trade window opened.");
                Console.WriteLine("Loading all inventory items.");
                Inventory.Item[] inventory = Trade.MyInventory.Items;
                foreach (Inventory.Item item in inventory)
                {
                    if (!item.IsNotTradeable)
                    {
                        var currentItem = Trade.CurrentSchema.GetItem(item.Defindex);
                        string name = "";
                        var type = Convert.ToInt32(item.Quality.ToString());
                        if (QualityToName(type) != "Unique")
                            name += QualityToName(type) + " ";                        
                        name += currentItem.ItemName;
                        if (QualityToName(type) == "Unusual")
                        {
                            try
                            {
                                for (int count = 0; count < item.Attributes.Length; count++)
                                {
                                    if (item.Attributes[count].Defindex == 134)
                                    {
                                        name += " (Effect: " + EffectToName(item.Attributes[count].FloatValue) + ")";
                                    }
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        if (currentItem.CraftMaterialType == "supply_crate")
                        {
                            for (int count = 0; count < item.Attributes.Length; count++)
                            {
                                name += " #" + (item.Attributes[count].FloatValue);
                                if (item.Attributes[count].Defindex == 186)
                                {
                                    name += " (Gifted)";
                                }
                            }
                        }
                        name += " (Level " + item.Level + ")";
                        try
                        {
                            int size = item.Attributes.Length;
                            for (int count = 0; count < size; count++)
                            {
                                if (item.Attributes[count].Defindex == 261)
                                {
                                    string paint = ShowBackpack.PaintToName(item.Attributes[count].FloatValue);
                                    name += " (Painted: " + paint + ")";
                                }
                                if (item.Attributes[count].Defindex == 186)
                                {
                                    name += " (Gifted)";
                                }
                            }
                        }
                        catch
                        {
                            // Item has no attributes... or something.
                        }
                        if (item.IsNotCraftable)
                            name += " (Uncraftable)";
                        if (currentItem.Name == "Wrapped Gift")
                        {
                            // Untested!
                            try
                            {                           
                                var containedItem = Trade.CurrentSchema.GetItem(item.ContainedItem.Defindex);
                                var containedName = GetItemName(containedItem, item.ContainedItem);
                                name += " (Contains: " + containedName + ")";
                            }
                            catch (Exception ex)
                            {
                                Bot.Print(ex);
                                // Guess this doesn't work :P.
                            }
                        }
                        ListInventory.Add(name, item.Id, currentItem.ImageURL);
                    }
                }
                try
                {
                    ShowTrade.loading = false;
                    Bot.main.Invoke((Action)(() => ShowTrade.list_inventory.SetObjects(ListInventory.Get())));
                }
                catch
                {

                }
            });
            loadInventory.Start();
        }

        string QualityToName(int quality)
        {
            if (quality == 1)
                return "Genuine";
            if (quality == 3)
                return "Vintage";
            if (quality == 5)
                return "Unusual";
            if (quality == 6)
                return "Unique";
            if (quality == 7)
                return "Community";
            if (quality == 9)
                return "Self-Made";
            if (quality == 11)
                return "Strange";
            if (quality == 13)
                return "Haunted";
            return "";
        }

        string EffectToName(float effect)
        {
            if (effect == 6)
                return "Green Confetti";
            if (effect == 7)
                return "Purple Confetti";
            if (effect == 8)
                return "Haunted Ghosts";
            if (effect == 9)
                return "Green Energy";
            if (effect == 10)
                return "Purple Energy";
            if (effect == 11)
                return "Circling TF Logo";
            if (effect == 12)
                return "Massed Flies";
            if (effect == 13)
                return "Burning Flames";
            if (effect == 14)
                return "Scorching Flames";
            if (effect == 15)
                return "Searing Plasma";
            if (effect == 16)
                return "Vivid Plasma";
            if (effect == 17)
                return "Sunbeams";
            if (effect == 18)
                return "Circling Peace Sign";
            if (effect == 19)
                return "Circling Heart";
            if (effect == 29)
                return "Stormy Storm";
            if (effect == 30)
                return "Blizzardy Storm";
            if (effect == 31)
                return "Nuts n' Bolts";
            if (effect == 32)
                return "Orbiting Planets";
            if (effect == 33)
                return "Orbiting Fire";
            if (effect == 34)
                return "Bubbling";
            if (effect == 35)
                return "Smoking";
            if (effect == 36)
                return "Steaming";
            if (effect == 37)
                return "Flaming Lantern";
            if (effect == 38)
                return "Cloudy Moon";
            if (effect == 39)
                return "Cauldron Bubbles";
            if (effect == 40)
                return "Eerie Orbiting Fire";
            if (effect == 43)
                return "Knifestorm";
            if (effect == 44)
                return "Misty Skull";
            if (effect == 45)
                return "Harvest Moon";
            if (effect == 46)
                return "It's a Secret to Everybody";
            if (effect == 47)
                return "Stormy 13th Hour";
            return "";
        }

        public override void OnTradeAddItem(Schema.Item schemaItem, Inventory.Item inventoryItem)
        {
            Bot.main.Invoke((Action)(() =>
            {
                string completeName = GetItemName(schemaItem, inventoryItem);
                ulong itemID = inventoryItem.Id;
                ListOtherOfferings.Add(completeName, itemID);
                ShowTrade.list_otherofferings.SetObjects(ListOtherOfferings.Get());
                ShowTrade.itemsAdded++;
                if (ShowTrade.itemsAdded > 0)
                {
                    ShowTrade.check_userready.Enabled = true;
                }
                ShowTrade.AppendText(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " added: " + GetItemName(schemaItem, inventoryItem, false));
                ShowTrade.ResetTradeStatus();
            }));
        }

        public override void OnTradeRemoveItem(Schema.Item schemaItem, Inventory.Item inventoryItem)
        {
            Bot.main.Invoke((Action)(() =>
            {
                string completeName = GetItemName(schemaItem, inventoryItem);
                ulong itemID = inventoryItem.Id;
                ListOtherOfferings.Remove(completeName, itemID);
                ShowTrade.list_otherofferings.SetObjects(ListOtherOfferings.Get());
                ShowTrade.itemsAdded--;
                if (ShowTrade.itemsAdded <= 0)
                {
                    ShowTrade.check_userready.Enabled = false;                    
                }
                ShowTrade.AppendText(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " removed: " + GetItemName(schemaItem, inventoryItem, false));
                ShowTrade.ResetTradeStatus();
            }));
        }

        string GetItemName(Schema.Item schemaItem, Inventory.Item inventoryItem, bool id = false)
        {
            var currentItem = Trade.CurrentSchema.GetItem(schemaItem.Defindex);
            string name = "";
            var type = Convert.ToInt32(inventoryItem.Quality.ToString());
            if (QualityToName(type) != "Unique")
                name += QualityToName(type) + " ";            
            name += currentItem.ItemName;
            if (QualityToName(type) == "Unusual")
            {
                try
                {
                    for (int count = 0; count < inventoryItem.Attributes.Length; count++)
                    {
                        if (inventoryItem.Attributes[count].Defindex == 134)
                        {
                            name += " (Effect: " + EffectToName(inventoryItem.Attributes[count].FloatValue) + ")";
                        }
                    }
                }
                catch (Exception)
                {
                    
                }
            }
            if (currentItem.CraftMaterialType == "supply_crate")
            {
                for (int count = 0; count < inventoryItem.Attributes.Length; count++)
                {
                    name += " #" + (inventoryItem.Attributes[count].FloatValue);
                }
            }
            name += " (Level " + inventoryItem.Level + ")";
            try
            {
                int size = inventoryItem.Attributes.Length;
                for (int count = 0; count < size; count++)
                {
                    if (inventoryItem.Attributes[count].Defindex == 261)
                    {
                        string paint = ShowBackpack.PaintToName(inventoryItem.Attributes[count].FloatValue);
                        name += " (Painted: " + paint + ")";
                    }
                    if (inventoryItem.Attributes[count].Defindex == 186)
                    {
                        name += " (Gifted)";
                    }
                }
            }
            catch
            {
                // Item has no attributes... or something.
            }
            if (inventoryItem.IsNotCraftable)
                name += " (Uncraftable)";
            if (currentItem.Name == "Wrapped Gift")
            {
                // Untested!
                try
                {
                    int size = inventoryItem.Attributes.Length;
                    for (int count = 0; count < size; count++)
                    {
                        var containedItem = Trade.CurrentSchema.GetItem(inventoryItem.ContainedItem.Defindex);
                        var containedName = GetItemName(containedItem, inventoryItem.ContainedItem);
                        name += " (Contains: " + containedName + ")";
                    }
                }
                catch
                {
                    // Item has no attributes... or something.
                }
            }
            if (id)
                name += " :" + inventoryItem.Id;
            return name;
        }

        public override void OnTradeMessage(string message)
        {
            Bot.main.Invoke((Action)(() =>
            {
                string send = Bot.SteamFriends.GetFriendPersonaName(OtherSID) + ": " + message + " [" + DateTime.Now.ToLongTimeString() + "]\r\n";
                ShowTrade.UpdateChat(send);
                if (!ShowTrade.focused)
                {
                    int duration = 3;
                    FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                    FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                    string title = "[Trade Chat] " + Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " says:";
                    Notification toastNotification = new Notification(title, message, duration, animationMethod, animationDirection, Friends.chat.chatTab.avatarBox);
                    toastNotification.Show();
                }
            }));
        }

        public override void OnTradeReady(bool ready)
        {
            Bot.main.Invoke((Action)(() =>
            {
                ShowTrade.check_otherready.Checked = ready;
                if (ready)
                    ShowTrade.AppendText(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " is ready.");
                else
                {
                    ShowTrade.AppendText(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " is not ready. [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                    ShowTrade.ResetTradeStatus();
                }
                if (ready && ShowTrade.check_userready.Checked)
                    ShowTrade.button_accept.Enabled = true;
                else
                    ShowTrade.button_accept.Enabled = false;
            }));
        }

        public override void OnTradeAccept()
        {
            Bot.otherAccepted = true;
            while (!ShowTrade.accepted)
            {
                // wait
            }
            OnTradeClose();
        }

    }

}