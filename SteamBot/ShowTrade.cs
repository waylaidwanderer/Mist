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

namespace MistClient
{
    public partial class ShowTrade : Form
    {
        Bot bot;
        ulong sid;
        string username;
        public static bool loading = false;
        public static int itemsAdded = 0;
        public static bool accepted = false;
        public bool focused = false;

        public ShowTrade(Bot bot, string name)
        {
            InitializeComponent();
            this.Text = "Trading with " + name;
            this.bot = bot;
            this.sid = bot.CurrentTrade.OtherSID;
            this.username = name;
            column_otherofferings.Text = name + "'s Offerings:";
            ListInventory.ShowTrade = this;
            Thread checkExpired = new Thread(() =>
            {
                while (true)
                {
                    if (bot.CurrentTrade == null)
                    {
                        bot.main.Invoke((Action)(this.Dispose));
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
                                        break; ;
                                    }
                                }

                            }));
                        }
                        break;
                    }
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

        private void label_cancel_MouseEnter(object sender, EventArgs e)
        {
            label_cancel.ForeColor = SystemColors.ControlText;
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
            button_accept.Text = "Accept Trade";
            accepted = false;
        }

        public void AppendText(string message)
        {
            text_log.AppendText(message + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
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
                text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                text_log.AppendText(itemName);
                text_log.SelectionColor = prevColor;
            }
            text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if (text_input.Text != "")
            {
                bot.CurrentTrade.SendMessage(text_input.Text);
                text_log.AppendText(Bot.displayName + ": " + text_input.Text + " [" + DateTime.Now.ToLongTimeString() + "]\r\n");
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
            button_accept.Text = "Waiting for other user...";
            Thread.Sleep(500);
            while (!bot.otherAccepted)
            {

            }
            bool success = bot.CurrentTrade.AcceptTrade();             
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
                                    string result = String.Format("Trade completed with {0}.", bot.SteamFriends.GetFriendPersonaName(sid));
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
        }

        private void ShowTrade_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bot.CurrentTrade != null)
            {
                ClearAll();
                bot.CurrentTrade.CancelTrade();
            }
        }

        private void list_inventory_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                ulong itemID = Convert.ToUInt64(column_id.GetValue(list_inventory.SelectedItem.RowObject));
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
                                    text_log.SelectionColor = ColorTranslator.FromHtml("#FFD700");
                                    text_log.AppendText(itemName);
                                    text_log.SelectionColor = prevColor;
                                }
                                text_log.AppendText(" [" + DateTime.Now.ToLongTimeString() + "]\r\n");
                                ResetTradeStatus();
                                list_inventory.SelectedItem.Remove();
                                ListUserOfferings.Add(itemName, itemID);
                                ListInventory.Remove(itemName, itemID);
                                list_userofferings.SetObjects(ListUserOfferings.Get());
                                list_inventory.SetObjects(ListInventory.Get());
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
                        var name = GetItemName(currentItem, item);
                        bot.log.Info("Adding " + name + ", " + item.Id);
                        try
                        {
                            bot.CurrentTrade.AddItem(item.Id);
                            itemsAdded++;
                            if (itemsAdded > 0)
                            {
                                check_userready.Enabled = true;
                            }
                            AppendText("You added: " + name);
                            ResetTradeStatus();
                            ListUserOfferings.Add(name, item.Id);
                            ListInventory.Remove(name, item.Id);
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
            ulong itemID = Convert.ToUInt64(column_id.GetValue(list_userofferings.SelectedItem.RowObject));
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
                            AppendText("You removed: " + itemName);
                            ResetTradeStatus();
                            list_userofferings.SelectedItem.Remove();
                            ListInventory.Add(itemName, itemID, img);
                            ListUserOfferings.Remove(itemName, itemID);
                            list_inventory.SetObjects(ListInventory.Get());
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
                button_accept.Enabled = true;
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
            focused = false;
        }

        
    }
}
