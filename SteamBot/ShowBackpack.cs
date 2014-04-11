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
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class ShowBackpack : MetroForm
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
            Util.LoadTheme(metroStyleManager1);
        }

        void LoadBP()
        {            
            ListBackpack.Clear();
            bot.GetOtherInventory(SID);
            Inventory.Item[] inventory = bot.OtherInventory(SID).Items;
            if (inventory == null)
            {
                bot.main.Invoke((Action)(() =>
                {
                    list_inventory.EmptyListMsg = "Could not retrieve backpack contents. Backpack is likely private.";
                    BrightIdeasSoftware.TextOverlay textOverlay = this.list_inventory.EmptyListMsgOverlay as BrightIdeasSoftware.TextOverlay;
                }));
                return;
            }
            bot.main.Invoke((Action)(() =>
            {
                list_inventory.View = View.Tile;
                list_inventory.TileSize = new Size(250, 64);
                ListView_SetSpacing(list_inventory, 70, 10);
            }));
            foreach (Inventory.Item item in inventory)
            {                
                bool isGift = false;
                bool isUnusual = false;
                var currentItem = Trade.CurrentSchema.GetItem(item.Defindex);
                try
                {
                    for (int count = 0; count < item.Attributes.Length; count++)
                    {
                        if (item.Attributes[count].Defindex == 229)
                        {
                            Console.WriteLine("Item: " + currentItem.ItemName);
                            Console.WriteLine(item.Attributes[count].FloatValue);
                            Console.WriteLine(item.Attributes[count].Value);
                        }
                    }
                }
                catch
                {

                }
                string name = "";
                string price = null;
                var type = Convert.ToInt32(item.Quality.ToString());
                if (QualityToName(type) != "Unique")
                    name += QualityToName(type) + " ";
                name += currentItem.ItemName;
                if (QualityToName(type) == "Unusual")
                {
                    isUnusual = true;
                    try
                    {
                        for (int count = 0; count < item.Attributes.Length; count++)
                        {
                            if (item.Attributes[count].Defindex == 134)
                            {
                                name += " (Effect: " + EffectToName(item.Attributes[count].FloatValue) + ")";
                                price = Util.GetPrice(item.Defindex, type, item, false, (int)item.Attributes[count].FloatValue);
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
                        if (item.Attributes[count].Defindex == 187)
                        {
                            name += " #" + (item.Attributes[count].FloatValue);
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
                            string paint = PaintToName(item.Attributes[count].FloatValue);
                            name += " (Painted: " + paint + ")";
                        }
                        if (item.Attributes[count].Defindex == 186)
                        {
                            isGift = true;
                            name += " (Gifted)";
                        }
                    }
                }
                catch
                {
                    // Item has no attributes... or something.
                }
                if (currentItem.Name == "Wrapped Gift")
                {
                    isGift = true;
                    // Untested!
                    try
                    {
                        var containedItem = Trade.CurrentSchema.GetItem(item.ContainedItem.Defindex);
                        var containedName = GetItemName(containedItem, item.ContainedItem);
                        price = Util.GetPrice(item.ContainedItem.Defindex, Convert.ToInt32(item.ContainedItem.Quality.ToString()), item, true);
                        name += " (Contains: " + containedName + ")";
                    }
                    catch (Exception ex)
                    {
                        Bot.Print(ex);
                        // Guess this doesn't work :P.
                    }
                }
                if (item.IsNotCraftable)
                    name += " (Uncraftable)";
                if (item.IsNotTradeable)
                    name += " (Untradeable)";
                if (!isGift && !isUnusual)
                {
                    price = Util.GetPrice(currentItem.Defindex, type, item);
                    ListBackpack.Add(name, item.Defindex, currentItem.ImageURL, price);
                }
                else
                {
                    ListBackpack.Add(name, item.Defindex, currentItem.ImageURL, price);
                }
                list_inventory.SetObjects(ListBackpack.Get());
            }            
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

        public static string EffectToName(float effect)
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

        public static string PaintToName(float color)
        {
            if (color == 3100495)
                return "A Color Similar to Slate";
            if (color == 7511618)
                return "Indubitably Green";
            if (color == 8208497)
                return "A Deep Commitment to Purple";
            if (color == 13595446)
                return "Mann Co. Orange";
            if (color == 1315860)
                return "A Distinctive Lack of Hue";
            if (color == 10843461)
                return "Muskelmannbraun";
            if (color == 12377523)
                return "A Mann's Mint";
            if (color == 5322826)
                return "Noble Hatter's Violet";
            if (color == 2960676)
                return "After Eight";
            if (color == 12955537)
                return "Peculiarly Drab Tincture";
            if (color == 8289918)
                return "Aged Moustache Grey";
            if (color == 16738740)
                return "Pink as Hell";
            if (color == 15132390)
                return "An Extraordinary Abundance of Tinge";
            if (color == 6901050)
                return "Radigan Conagher Brown";
            if (color == 15185211)
                return "Australium Gold";
            if (color == 3329330)
                return "The Bitter Taste of Defeat and Lime";
            if (color == 14204632)
                return "Color No. 216-190-216";
            if (color == 15787660)
                return "The Color of a Gentlemann's Business Pants";
            if (color == 15308410)
                return "Dark Salmon Injustice";
            if (color == 8154199)
                return "Ye Olde Rustic Colour";
            if (color == 8421376)
                return "Drably Olive";
            if (color == 4345659)
                return "Zepheniah's Greed";
            if (color == 6637376 || color == 2636109)
                return "An Air of Debonair";
            if (color == 12073019 || color == 5801378)
                return "Team Spirit";
            if (color == 3874595 || color == 1581885)
                return "Balaclavas Are Forever";
            if (color == 8400928 || color == 2452877)
                return "The Value of Teamwork";
            if (color == 12807213 || color == 12091445)
                return "Cream Spirit";
            if (color == 11049612 || color == 8626083)
                return "Waterlogged Lab Coat";
            if (color == 4732984 || color == 3686984)
                return "Operator's Overalls";
            return "Unknown";
        }

        private void ShowBackpack_Load(object sender, EventArgs e)
        {
            ToolTip priceTip = new ToolTip();
            priceTip.ToolTipIcon = ToolTipIcon.Info;
            priceTip.IsBalloon = true;
            priceTip.ShowAlways = true;
            priceTip.ToolTipTitle = "Item prices are from backpack.tf";
            priceTip.SetToolTip(checkBox1, "What the price checker doesn't do:\n-Factor in the cost of paint\n-Factor in the cost of strange parts\n-Calculate values of low craft numbers\nPrices are not guaranteed to be accurate.");
            this.Invoke((Action)(() =>
            {
                loadBP = new Thread(LoadBP);
                loadBP.Start();
            }));
            this.Invoke((Action)(() =>
            {
                this.column_inventory.ImageGetter = delegate(object row)
                {
                    string key = column_defindex.GetValue(row).ToString();
                    if (!this.list_inventory.LargeImageList.Images.ContainsKey(key))
                    {
                        string url = column_url.GetValue(row).ToString();
                        if (url != "")
                        {
                            Image largeImage = getImageFromURL(url, Convert.ToInt32(key));
                            this.list_inventory.SmallImageList.Images.Add(key, largeImage);
                            this.list_inventory.LargeImageList.Images.Add(key, largeImage);
                        }
                    }                    
                    return key;
                };
            }));
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        public void ListView_SetSpacing(ListView listview, short cx, short cy)
        {
            const int LVM_FIRST = 0x1000;
            const int LVM_SETICONSPACING = LVM_FIRST + 53;
            // http://msdn.microsoft.com/en-us/library/bb761176(VS.85).aspx
            // minimum spacing = 4
            SendMessage(listview.Handle, LVM_SETICONSPACING,
            IntPtr.Zero, (IntPtr)MakeLong(cx, cy));
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

        public Bitmap getImageFromURL(string url, int defindex)
        {
            //string name = defindex + ".png";
            //string localPath = Path.Combine(Application.StartupPath, "cache");
            //string localFile = Path.Combine(localPath, name);
            //WebClient client = new WebClient();
            //client.DownloadFileAsync(new Uri(url), localFile);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
            myResponse.Close();
            return bmp;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                list_inventory.View = View.Details;
                column_value.IsVisible = true;
                list_inventory.RebuildColumns();
            }
            else
            {
                list_inventory.View = View.Tile;
                column_value.IsVisible = false;
                list_inventory.RebuildColumns();
            }
        }
    }
}
