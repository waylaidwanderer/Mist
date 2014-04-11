using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using SteamBot;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class ShowTrade : MetroForm
    {
        Bot bot;
        ulong sid;
        string username;
        public static bool loading = false;
        public static int itemsAdded = 0;
        public static bool accepted = false;
        public bool focused = false;
        public double OtherTotalValue = 0;
        double YourTotalValue = 0;
        Thread acceptTrade;
        bool tradeCompleted;

        public ShowTrade(Bot bot, string name)
        {
            InitializeComponent();
            Util.LoadTheme(metroStyleManager1);
            this.Text = "Trading with " + name;
            this.bot = bot;
            this.sid = bot.CurrentTrade.OtherSID;
            this.username = name;
            this.label_yourvalue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_othervalue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            column_otherofferings.Text = name + "'s Offerings:";
            ListInventory.ShowTrade = this;
            Thread checkExpired = new Thread(() =>
            {
                while (true)
                {
                    if (bot.CurrentTrade == null)
                    {
                        bot.main.Invoke((Action)(this.Close));
                        bot.log.Warn("Trade expired.");
                        if (Friends.chat_opened)
                        {
                            bot.main.Invoke((Action)(() =>
                            {
                                foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                                {
                                    if (tab.Text == bot.SteamFriends.GetFriendPersonaName(sid))
                                    {
                                        tab.Invoke((Action)(() =>
                                        {
                                            foreach (var item in tab.Controls)
                                            {
                                                Friends.chat.chatTab = (ChatTab)item;
                                            }
                                            string result = "The trade session has closed.";
                                            bot.log.Warn(result);
                                            string date = "[" + DateTime.Now + "] ";
                                            Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + result + "\r\n", false);
                                            ChatTab.AppendLog(sid, "===========[TRADE ENDED]===========\r\n");
                                        }));
                                        break;
                                    }
                                }

                            }));
                        }
                        break;
                    }
                    Thread.Sleep(1000);
                }
            });
            checkExpired.Start();
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
            text_log.AppendText(text);
            text_log.ScrollToCaret();
            if (!focused)
            {
                try
                {
                    string soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                    string soundFile = Path.Combine(soundsFolder + "trade_message.wav");
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFile))
                    {
                        player.Play();
                    }
                }
                catch (Exception ex)
                {
                    bot.log.Error(ex.ToString());
                }
                FlashWindow.Flash(this);
            }
        }

        public void UpdateLabel(string text)
        {
            if (label_othervalue.InvokeRequired)
            {
                label_othervalue.Invoke((Action)(() => UpdateLabel(text)));
                return;
            }
            label_othervalue.Text = text;
        }

        private void label_cancel_MouseEnter(object sender, EventArgs e)
        {
            if (metroStyleManager1.Theme == MetroFramework.MetroThemeStyle.Dark)
            {
                label_cancel.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                label_cancel.ForeColor = SystemColors.ControlText;
            }
        }

        private void label_cancel_MouseLeave(object sender, EventArgs e)
        {
            label_cancel.ForeColor = SystemColors.ControlDarkDark;
        }

        private void label_cancel_Click(object sender, EventArgs e)
        {            
            try
            {
                bot.CurrentTrade.CancelTrade();
                ClearAll();
                Thread.Sleep(2000);
                this.Dispose();
            }
            catch (Exception ex)
            {
                bot.log.Error(ex.ToString());
                ClearAll();
                Thread.Sleep(2000);
                this.Dispose();
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
                    bot.CurrentTrade.SendMessage(text_input.Text);
                    text_log.AppendText(Bot.displayName + ": " + text_input.Text + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                    text_log.ScrollToCaret();
                    ChatTab.AppendLog(sid, "[Trade Chat] " + Bot.displayName + ": " + text_input.Text + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                    clear();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        public void ResetTradeStatus()
        {
            this.check_userready.Checked = false;
            this.check_otherready.Checked = false;
            this.button_accept.Enabled = false;
            button_accept.Enabled = false;
            button_accept.Highlight = false;
            button_accept.Text = "Accept Trade";
            accepted = false;
            if (acceptTrade != null)
            {
                if (acceptTrade.IsAlive)
                    acceptTrade.Abort();
            }
        }

        public void AppendText(string message)
        {
            text_log.AppendText(message + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
            text_log.ScrollToCaret();
        }

        public void AppendText(string message, string itemName)
        {
            Color prevColor = text_log.SelectionColor;
            text_log.AppendText(message);            
            if (itemName.Contains("Strange"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#CF6A32");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Vintage"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#476291");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Unusual"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#8650AC");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Geniune"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#4D7455");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Haunted"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#38F3AB");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Community") || itemName.Contains("Self-Made"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#70B04A");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else if (itemName.Contains("Valve"))
            {
                text_log.SelectionColor = ColorTranslator.FromHtml("#A50F79");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            else
            {
                //text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                text_log.SelectionColor = Color.DarkGoldenrod;
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
            text_log.ScrollToCaret();
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if (text_input.Text != "")
            {
                bot.CurrentTrade.SendMessage(text_input.Text);
                text_log.AppendText(Bot.displayName + ": " + text_input.Text + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                text_log.ScrollToCaret();
                ChatTab.AppendLog(sid, "[Trade Chat] " + Bot.displayName + ": " + text_input.Text + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                clear();
            }
        }

        void clear()
        {
            text_input.Select(0, 0);
            text_input.Clear();
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            accepted = true;
            button_accept.Enabled = false;
            button_accept.Highlight = false;
            button_accept.Text = "Waiting for other user...";
            Thread.Sleep(500);
            acceptTrade = new Thread(() =>
            {
                while (!bot.otherAccepted)
                {

                }
                bool success = false;
                for (int count = 0; count < 5; count++)
                {
                    try
                    {
                        success = tradeCompleted = bot.CurrentTrade.AcceptTrade();
                    }
                    catch
                    {

                    }
                    if (success)
                        break;
                    else
                        Thread.Sleep(250);
                }
                if (Friends.chat_opened)
                {
                    bot.main.Invoke((Action)(() =>
                    {
                        foreach (TabPage tab in Friends.chat.ChatTabControl.TabPages)
                        {
                            if (tab.Text == bot.SteamFriends.GetFriendPersonaName(sid))
                            {
                                tab.Invoke((Action)(() =>
                                {
                                    foreach (var item in tab.Controls)
                                    {
                                        Friends.chat.chatTab = (ChatTab)item;
                                    }
                                    if (success)
                                    {
                                        string result = String.Format("Trade completed successfully with {0}!", bot.SteamFriends.GetFriendPersonaName(sid));
                                        bot.log.Success(result);
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + result + "\r\n", false);
                                    }
                                    else
                                    {
                                        string result = "The trade may have failed.";
                                        bot.log.Warn(result);
                                        Friends.chat.chatTab.UpdateChat("[" + DateTime.Now + "] " + result + "\r\n", false);
                                    }
                                }));
                                break; ;
                            }
                        }

                    }));
                }
            });
            acceptTrade.Start();
        }

        private void list_inventory_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                ulong itemID = Convert.ToUInt64(column_id.GetValue(list_inventory.SelectedItem.RowObject));
                string itemValue = column_value.GetValue(list_inventory.SelectedItem.RowObject).ToString();
                // Check whether in ref, keys or buds first
                double value = 0;
                if (itemValue.Contains("ref"))
                {
                    string newValue = ReplaceLastOccurrence(itemValue, "ref", "");
                    value = Convert.ToDouble(newValue);
                }
                else if (itemValue.Contains("key"))
                {
                    string newValue = ReplaceLastOccurrence(itemValue, "keys", "");
                    Console.WriteLine("{0}, {1}", itemValue, newValue);
                    value = Convert.ToDouble(newValue);
                    value = value * BackpackTF.KeyPrice;
                }
                else if (itemValue.Contains("bud"))
                {
                    string newValue = ReplaceLastOccurrence(itemValue, "buds", "");
                    value = Convert.ToDouble(newValue);
                    value = value * BackpackTF.BudPrice;
                }
                YourTotalValue += value;
                if (YourTotalValue >= BackpackTF.BudPrice * 1.33)
                {
                    double formatPrice = YourTotalValue / BackpackTF.BudPrice;
                    label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " buds";
                }
                else if (YourTotalValue >= BackpackTF.KeyPrice)
                {
                    double formatPrice = YourTotalValue / BackpackTF.KeyPrice;
                    label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " keys";
                }
                else
                {
                    label_yourvalue.Text = "Total Value: " + YourTotalValue.ToString("0.00") + " ref";
                }
                if (itemID != 0)
                {
                    try
                    {
                        var itemName = list_inventory.SelectedItem.Text.Trim();
                        bool valid = false;
                        bot.GetInventory();
                        foreach (var item in bot.MyInventory.Items)
                        {
                            if (item.Id == itemID)
                                valid = true;
                        }
                        if (valid)
                        {
                            try
                            {
                                Color prevColor = text_log.SelectionColor;
                                bot.CurrentTrade.AddItem(itemID);
                                itemsAdded++;
                                if (itemsAdded > 0)
                                {
                                    check_userready.Enabled = true;                                    
                                }
                                text_log.AppendText("You added: ");
                                if (itemName.Contains("Strange"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#CF6A32");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Vintage"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#476291");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Unusual"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#8650AC");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Geniune"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#4D7455");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Haunted"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#38F3AB");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Community") || itemName.Contains("Self-Made"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#70B04A");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else if (itemName.Contains("Valve"))
                                {
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#A50F79");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                else
                                {
                                    //text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                                    text_log.SelectionColor = Color.DarkGoldenrod;
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                                text_log.ScrollToCaret();
                                ResetTradeStatus();
                                list_inventory.SelectedItem.Remove();
                                ListUserOfferings.Add(itemName, itemID, itemValue);
                                ListInventory.Remove(itemName, itemID);
                                list_userofferings.SetObjects(ListUserOfferings.Get());
                                //list_inventory.SetObjects(ListInventory.Get());
                            }
                            catch (SteamTrade.Exceptions.TradeException ex)
                            {
                                bot.main.Invoke((Action)(() =>
                                {
                                    bot.log.Error(ex.ToString());
                                    MessageBox.Show(ex + "\nYou can ignore this error. Just restart the trade. Sorry about that :(",
                                        "Trade Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                                }));
                            }
                        }
                        else
                        {
                            bot.log.Warn("Invalid item, skipping");
                        }
                    }
                    catch (Exception ex)
                    {
                        bot.log.Error(ex.ToString());
                        bot.main.Invoke((Action)(() =>
                        {
                            MessageBox.Show("\nSomething weird happened. Here's the error:\n" + ex,
                                        "Trade Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                bot.log.Error(ex.ToString());
            }
        }

        public static void ClearAll()
        {
            ListInventory.Clear();
            ListUserOfferings.Clear();
            ListOtherOfferings.Clear();
        }

        private void addAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Don't click Yes unless you haven't added any items to the trade yet. Are you sure you wish to continue?", "WARNING: Experimental Feature", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                bot.GetInventory();
                foreach (var item in bot.MyInventory.Items)
                {
                    if (!item.IsNotTradeable)
                    {
                        var currentItem = SteamTrade.Trade.CurrentSchema.GetItem(item.Defindex);
                        var itemName = GetItemName(currentItem, item);
                        string itemValue = Util.GetPrice(item.Defindex, currentItem.ItemQuality, item);
                        double value = 0;
                        if (itemValue.Contains("ref"))
                        {
                            string newValue = ReplaceLastOccurrence(itemValue, "ref", "");
                            value = Convert.ToDouble(newValue);
                        }
                        else if (itemValue.Contains("key"))
                        {
                            string newValue = ReplaceLastOccurrence(itemValue, "keys", "");
                            Console.WriteLine("{0}, {1}", itemValue, newValue);
                            value = Convert.ToDouble(newValue);
                            value = value * BackpackTF.KeyPrice;
                        }
                        else if (itemValue.Contains("bud"))
                        {
                            string newValue = ReplaceLastOccurrence(itemValue, "buds", "");
                            value = Convert.ToDouble(newValue);
                            value = value * BackpackTF.BudPrice;
                        }
                        YourTotalValue += value;
                        if (YourTotalValue >= BackpackTF.BudPrice * 1.33)
                        {
                            double formatPrice = YourTotalValue / BackpackTF.BudPrice;
                            label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " buds";
                        }
                        else if (YourTotalValue >= BackpackTF.KeyPrice)
                        {
                            double formatPrice = YourTotalValue / BackpackTF.KeyPrice;
                            label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " keys";
                        }
                        else
                        {
                            label_yourvalue.Text = "Total Value: " + YourTotalValue.ToString("0.00") + " ref";
                        }
                        bot.log.Info("Adding " + itemName + ", " + item.Id);
                        try
                        {
                            bot.CurrentTrade.AddItem(item.Id);
                            itemsAdded++;
                            if (itemsAdded > 0)
                            {
                                check_userready.Enabled = true;
                            }
                            Color prevColor = text_log.SelectionColor;
                            text_log.AppendText("You added: ");
                            if (itemName.Contains("Strange"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#CF6A32");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Vintage"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#476291");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Unusual"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#8650AC");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Geniune"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#4D7455");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Haunted"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#38F3AB");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Community") || itemName.Contains("Self-Made"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#70B04A");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Valve"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#A50F79");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else
                            {
                                //text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                                text_log.SelectionColor = Color.DarkGoldenrod;
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                            text_log.ScrollToCaret();
                            ResetTradeStatus();
                            ListUserOfferings.Add(itemName, item.Id, itemValue);
                            ListInventory.Remove(itemName, item.Id);
                            list_userofferings.SetObjects(ListUserOfferings.Get());
                            list_inventory.SetObjects(ListInventory.Get());
                        }
                        catch (Exception ex)
                        {
                            bot.log.Error(ex.ToString());
                        }
                    }
                }
                bot.log.Info("Done adding all items!");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        string GetItemName(SteamTrade.Schema.Item schemaItem, SteamTrade.Inventory.Item inventoryItem, bool id = false)
        {
            var currentItem = SteamTrade.Trade.CurrentSchema.GetItem(schemaItem.Defindex);
            string name = "";
            var type = Convert.ToInt32(inventoryItem.Quality.ToString());
            if (QualityToName(type) != "Unique")
                name += QualityToName(type) + " ";
            name += currentItem.ItemName;

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
                        var containedItem = SteamTrade.Trade.CurrentSchema.GetItem(inventoryItem.ContainedItem.Defindex);
                        name += " (Contains: " + containedItem.ItemName + ")";
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

        private void list_userofferings_ItemActivate(object sender, EventArgs e)
        {
            ulong itemID = Convert.ToUInt64(column_uo_id.GetValue(list_userofferings.SelectedItem.RowObject));
            string itemValue = column_uo_value.GetValue(list_userofferings.SelectedItem.RowObject).ToString();
            double value = 0;
            if (itemValue.Contains("ref"))
            {
                string newValue = ReplaceLastOccurrence(itemValue, "ref", "");
                value = Convert.ToDouble(newValue);
            }
            else if (itemValue.Contains("key"))
            {
                string newValue = ReplaceLastOccurrence(itemValue, "keys", "");
                value = Convert.ToDouble(newValue);
                value = value * BackpackTF.KeyPrice;
            }
            else if (itemValue.Contains("bud"))
            {
                string newValue = ReplaceLastOccurrence(itemValue, "buds", "");
                value = Convert.ToDouble(newValue);
                value = value * BackpackTF.BudPrice;
            }
            YourTotalValue -= value;
            Console.WriteLine(YourTotalValue);
            if (YourTotalValue >= BackpackTF.BudPrice * 1.33)
            {
                double formatPrice = YourTotalValue / BackpackTF.BudPrice;
                label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " buds";
            }
            else if (YourTotalValue >= BackpackTF.KeyPrice)
            {
                double formatPrice = YourTotalValue / BackpackTF.KeyPrice;
                label_yourvalue.Text = "Total Value: " + formatPrice.ToString("0.00") + " keys";
            }
            else
            {
                label_yourvalue.Text = "Total Value: " + YourTotalValue.ToString("0.00") + " ref";
            }
            if (itemID != 0)
            {
                try
                {
                    var itemName = list_userofferings.SelectedItem.Text.Trim();
                    bool valid = false;
                    string img = "";
                    bot.GetInventory();
                    foreach (var item in bot.MyInventory.Items)
                    {
                        if (item.Id == itemID)
                        {
                            valid = true;
                            img = SteamTrade.Trade.CurrentSchema.GetItem(item.Defindex).ImageURL;
                        }
                    }
                    if (valid)
                    {
                        try
                        {
                            bot.CurrentTrade.RemoveItem(itemID);
                            itemsAdded--;
                            if (itemsAdded < 1)
                            {
                                check_userready.Enabled = true;
                            }
                            Color prevColor = text_log.SelectionColor;
                            text_log.AppendText("You removed: ");
                            if (itemName.Contains("Strange"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#CF6A32");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Vintage"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#476291");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Unusual"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#8650AC");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Geniune"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#4D7455");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Haunted"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#38F3AB");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Community") || itemName.Contains("Self-Made"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#70B04A");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else if (itemName.Contains("Valve"))
                            {
                                text_log.SelectionColor = ColorTranslator.FromHtml("#A50F79");
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            else
                            {
                                //text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                                text_log.SelectionColor = Color.DarkGoldenrod;
                                text_log.AppendText(itemName);
                                text_log.SelectionColor = prevColor;
                            }
                            text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                            text_log.ScrollToCaret();
                            ResetTradeStatus();
                            list_userofferings.SelectedItem.Remove();
                            ListInventory.Add(itemName, itemID, img, itemValue);
                            ListUserOfferings.Remove(itemName, itemID);
                            //list_inventory.SetObjects(ListInventory.Get());
                            list_userofferings.SetObjects(ListUserOfferings.Get());
                        }
                        catch (SteamTrade.Exceptions.TradeException ex)
                        {
                            bot.log.Error(ex.ToString());
                        }
                    }
                    else
                    {
                        bot.log.Warn("Invalid item, skipping");
                    }
                }
                catch (Exception ex)
                {
                    bot.log.Error(ex.ToString());
                }
            }
        }

        private void check_userready_CheckedChanged(object sender, EventArgs e)
        {
            bool Checked = check_userready.Checked;
            if (Checked)
                AppendText("You are ready.");
            else
                AppendText("You are not ready.");
            bot.CurrentTrade.SetReady(Checked);
            if (Checked && check_otherready.Checked)
            {
                button_accept.Enabled = true;
                button_accept.Highlight = true;
            }
        }

        private void ShowTrade_Activated(object sender, EventArgs e)
        {
            focused = true;
        }

        private void ShowTrade_Deactivate(object sender, EventArgs e)
        {
            focused = false;
        }

        private void ShowTrade_Load(object sender, EventArgs e)
        {
            label_yourvalue.Visible = false;
            label_othervalue.Visible = false;
            focused = false;
            ToolTip priceTip = new ToolTip();
            priceTip.ToolTipIcon = ToolTipIcon.Info;
            priceTip.IsBalloon = true;
            priceTip.ShowAlways = true;
            priceTip.ToolTipTitle = "Item prices are from backpack.tf";
            string caution = "What the price checker doesn't do:\n-Factor in the cost of paint\n-Factor in the cost of strange parts\n-Calculate values of low craft numbers\nPrices are not guaranteed to be accurate.";
            priceTip.SetToolTip(label_yourvalue, caution);
            priceTip.SetToolTip(label_othervalue, caution);
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        private void disableGroupingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool checkedState = disableGroupingToolStripMenuItem.Checked;
            list_inventory.ShowGroups = !checkedState;
            list_userofferings.ShowGroups = !checkedState;
            list_otherofferings.ShowGroups = !checkedState;
        }

        private void disableItemGroupingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool checkedState = disableItemGroupingToolStripMenuItem.Checked;
            list_inventory.ShowGroups = !checkedState;
            list_userofferings.ShowGroups = !checkedState;
            list_otherofferings.ShowGroups = !checkedState;
        }

        private void viewSuggestedItemPricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool checkedState = viewSuggestedItemPricesToolStripMenuItem.Checked;
            label_yourvalue.Visible = checkedState;
            label_othervalue.Visible = checkedState;
            column_value.IsVisible = checkedState;
            column_uo_value.IsVisible = checkedState;
            column_oo_value.IsVisible = checkedState;
            list_inventory.RebuildColumns();
            list_userofferings.RebuildColumns();
            list_otherofferings.RebuildColumns();
            if (checkedState)
            {
                MessageBox.Show("These prices are pulled from backpack.tf. They are submitted suggestions, and should only be taken as a rough guide."
                                + "\nThe price checker DOES NOT take into account the value of paints or strange parts on items, and are especially not guaranteed to be accurate for Unusuals.",
                                "Item Price Checker",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void ShowTrade_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearAll();
            if (bot.CurrentTrade != null && !tradeCompleted)
            {
                bot.CurrentTrade.CancelTrade();
            }
        }

        private void text_search_Enter(object sender, EventArgs e)
        {
            text_search.Clear();
            text_search.ForeColor = SystemColors.WindowText;
            text_search.Font = new Font(text_search.Font, FontStyle.Regular);
        }

        private void text_search_Leave(object sender, EventArgs e)
        {
            if (text_search.Text == null)
            {
                this.list_inventory.SetObjects(ListInventory.Get());
                text_search.ForeColor = Color.Gray;
                text_search.Font = new Font(text_search.Font, FontStyle.Italic);
                text_search.Text = "Search for an item in your inventory...";
            }
            if (this.list_inventory.Columns == null)
                this.list_inventory.SetObjects(ListInventory.Get());
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            if (text_search.Text == "")
                this.list_inventory.SetObjects(ListInventory.Get());
            else
                this.list_inventory.SetObjects(ListInventory.Get(text_search.Text));
        }

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                text_search.Clear();
                this.list_inventory.SetObjects(ListInventory.Get());
            }
        }

        private void removeAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
