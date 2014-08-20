using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using SteamKit2;
using System.Windows.Forms;
using SteamTrade;
using System.Web;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using Awesomium.Core;

namespace MistClient
{
    public partial class ShowTrade_Web : MetroFramework.Forms.MetroForm
    {
        SteamBot.Bot bot;
        string trade_url = "";
        public bool focused = true;
        int baseWidth = 0;
        int baseZoom = 0;

        public ShowTrade_Web(SteamBot.Bot bot)
        {
            InitializeComponent();
            this.Icon = MistClient.Properties.Resources.Icon;
            this.bot = bot;
            trade_url = string.Format("http://steamcommunity.com/trade/{0}/", bot.SteamUser.SteamID.ConvertToUInt64());
            this.Text = "Trading with " + bot.SteamFriends.GetFriendPersonaName(bot.CurrentTrade.OtherSID);
            this.baseWidth = webControl1.Width;
            this.baseZoom = (int)Math.Round((double)baseWidth / 5.43);
        }

        private void ShowTrade_Web_Load(object sender, EventArgs e)
        {
            webControl1.DocumentReady += webControl1_DocumentReady;
            LoadTrade();
            new Thread(GetTradeState).Start();
        }

        void webControl1_DocumentReady(object sender, UrlEventArgs e)
        {
            webControl1.Zoom = baseZoom;            
            while (webControl1.ExecuteJavascriptWithResult("document.body.innerHTML").IsUndefined)
            {
                WebCore.Update();
            }
            webControl1.ExecuteJavascript("var footerSpacer = document.getElementById('footer_spacer'); footerSpacer.parentNode.removeChild(footerSpacer);");
            webControl1.ExecuteJavascript("var footer = document.getElementById('footer'); footerSpacer.parentNode.removeChild(footer);");
            var script = @" var scrollbarCSS = '::-webkit-scrollbar { width: 12px !important; } ::-webkit-scrollbar-track { background-color: #111111 !important;	} ::-webkit-scrollbar-thumb { background-color: #444444 !important; } ::-webkit-scrollbar-thumb:hover { background-color: #5e5e5e !important; }';              
                            var head = document.getElementsByTagName('head')[0];
                            var style = document.createElement('style');
                            style.type = 'text/css';
                            style.innerHTML = scrollbarCSS;
                            head.appendChild(style);
                            ";
            webControl1.ExecuteJavascript(script);
        }

        private void GetTradeState()
        {
            Dictionary<string, double> ItemPrices = new Dictionary<string, double>();
            while (bot.CurrentTrade != null)
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("=========MY ITEMS========");
                    foreach (var item in bot.CurrentTrade.MyOfferedItems)
                    {
                        bool exists = false;
                        var inventoryItem = bot.CurrentTrade.MyInventory.GetItem(item.AppId, item.ContextId, item.ItemId, item.IsCurrency);
                        if (!ItemPrices.ContainsKey(inventoryItem.Name))
                            ItemPrices.Add(inventoryItem.Name, Util.GetSteamMarketPrice(bot, inventoryItem));
                        foreach (var line in sb.ToString().Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(line) && line.StartsWith(inventoryItem.Name))
                            {
                                int num;
                                var amount = int.TryParse(line.Split(' ').Last().Replace("x", ""), out num);
                                if (num == 0) num = 1;
                                num++;
                                sb.Replace(line, string.Format("{0} (${1}) x{2}", inventoryItem.Name, ItemPrices[inventoryItem.Name], num));
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            sb.AppendLine(string.Format("{0} (${1})", inventoryItem.Name, ItemPrices[inventoryItem.Name]));
                        }
                    }
                    sb.AppendLine();
                    sb.AppendLine("=====OTHER USER'S ITEMS=====");
                    foreach (var item in bot.CurrentTrade.OtherOfferedItems)
                    {
                        bool exists = false;
                        var inventoryItem = bot.CurrentTrade.OtherInventory.GetItem(item.AppId, item.ContextId, item.ItemId, item.IsCurrency);
                        if (!ItemPrices.ContainsKey(inventoryItem.Name))
                            ItemPrices.Add(inventoryItem.Name, Util.GetSteamMarketPrice(bot, inventoryItem));
                        foreach (var line in sb.ToString().Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(line) && line.StartsWith(inventoryItem.Name))
                            {
                                int num;
                                var amount = int.TryParse(line.Split(' ').Last().Replace("x", ""), out num);
                                if (num == 0) num = 1;
                                num++;
                                sb.Replace(line, string.Format("{0} (${1}) x{2}", inventoryItem.Name, ItemPrices[inventoryItem.Name], num));
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            sb.AppendLine(string.Format("{0} (${1})", inventoryItem.Name, ItemPrices[inventoryItem.Name]));
                        }
                    }
                    bot.main.Invoke((Action)(() =>
                    {
                        metroLabel1.Text = sb.ToString();
                    }));
                }
                catch
                {

                }
                Thread.Sleep(800);
            }
        }

        private void LoadTrade()
        {
            string cookies = string.Format("steamLogin={0}; sessionid={1}", bot.token, bot.sessionId);
            webControl1.WebSession.SetCookie("http://steamcommunity.com".ToUri(), cookies, true, true);

            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Webpage failed to load! Aborting trade.", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bot.CloseTrade();
                }
                try
                {
                    webControl1.Source = trade_url.ToUri();
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void ShowTrade_Web_Resize(object sender, EventArgs e)
        {
            if (webControl1.WebSession == null || webControl1.ExecuteJavascriptWithResult("document.body.innerHTML").IsUndefined)
                return;

            var deltaWidthPercent = (int)Math.Round((double)(webControl1.Width - baseWidth) / baseWidth * 100);
            webControl1.Zoom = baseZoom + deltaWidthPercent;
        }

        private void ShowTrade_Web_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bot.CurrentTrade != null)
                new Thread(() => bot.CurrentTrade.CancelTrade()).Start();
        }
    }
}
