using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SteamBot;
using SteamTrade;
using SteamKit2;
using System.Threading;

namespace MistClient
{
    public partial class ShowBackpack : Form
    {
        Bot bot;
        SteamID SID;
        Thread loadBP;

        public ShowBackpack(Bot bot, SteamID SID)
        {
            InitializeComponent();
            this.bot = bot;
            this.SID = SID;
            this.Text = bot.SteamFriends.GetFriendPersonaName(SID) + "'s Backpack";
        }

        void LoadBP()
        {
            ListBackpack.Clear();
            bot.GetOtherInventory(SID);
            Inventory.Item[] inventory = bot.OtherInventory.Items;
            if (inventory == null)
            {
                bot.main.Invoke((Action)(() =>
                {
                    list_inventory.ShowGroups = false;
                }));
                ListBackpack.Add("Could not retrieve backpack contents. Backpack is likely private.");
                list_inventory.SetObjects(ListBackpack.Get());
                return;
            }
            foreach (Inventory.Item item in inventory)
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
                    catch
                    {

                    }
                }                                    
                if (currentItem.CraftMaterialType == "supply_crate")
                {
                    for (int count = 0; count < item.Attributes.Length; count++)
                    {
                        name += " #" + (item.Attributes[count].FloatValue);
                    }
                }
                name += " (Level " + item.Level + ")";
                if (item.IsNotTradeable)
                    name += " (Untradeable)";
                try
                {
                    int size = item.Attributes.Length;
                    for (int count = 0; count < size; count++)
                    {
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
                        name += " (Contains: " + containedItem.ItemName + ")";
                    }
                    catch (Exception ex)
                    {
                        Bot.Print(ex);
                        // Guess this doesn't work :P.
                    }
                }
                ListBackpack.Add(name);
                list_inventory.SetObjects(ListBackpack.Get());
            } 
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

        private void ShowBackpack_Load(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                loadBP = new Thread(LoadBP);
                loadBP.Start();
            }));
        }

        private void ShowBackpack_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                loadBP.Abort();
            }
            catch (Exception ex)
            {
                Bot.Print(ex);
            }
        }
    }
}
