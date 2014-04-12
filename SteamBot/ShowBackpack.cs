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
            var count = 1;
            foreach (Inventory.Item item in inventory)
            {
                var currentItem = Trade.CurrentSchema.GetItem(item.Defindex);
                var name = Util.GetItemName(currentItem, item);
                var price = "";
                var isGift = Util.IsItemGifted(item);

                if (currentItem.Name == "Wrapped Gift")
                {
                    price = Util.GetPrice(item.ContainedItem.Defindex, Convert.ToInt32(item.ContainedItem.Quality.ToString()), item, isGift);
                }
                else
                {
                    price = Util.GetPrice(currentItem.Defindex, Convert.ToInt32(item.Quality.ToString()), item, isGift);
                }
                ListBackpack.Add(name, item.Defindex, currentItem.ImageURL, price);
                bot.main.Invoke((Action)(() =>
                {
                    double percent = Math.Round(count / (double) inventory.Length * 100);
                    list_inventory.EmptyListMsg = "Loading backpack: " + percent + "%";
                    BrightIdeasSoftware.TextOverlay textOverlay = this.list_inventory.EmptyListMsgOverlay as BrightIdeasSoftware.TextOverlay;
                }));
                count++;
            }
            list_inventory.SetObjects(ListBackpack.Get());
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
