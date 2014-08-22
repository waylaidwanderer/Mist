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
using MetroFramework.Forms;
using SteamBot;

namespace MistClient
{
    public partial class ShowBackpack : MetroForm
    {
        Bot bot;
        SteamID SID;

        public ShowBackpack(Bot bot, SteamID SID)
        {
            InitializeComponent();
            this.bot = bot;
            this.SID = SID;
            this.Text = bot.SteamFriends.GetFriendPersonaName(SID) + "'s Backpack";
            Util.LoadTheme(this, this.Controls);
            this.Width = 1012;            
        }

        private void ShowBackpack_Load(object sender, EventArgs e)
        {
            string cookies = string.Format("steamLogin={0}; sessionid={1}", bot.token, bot.sessionId);
            webControl1.WebSession.SetCookie("http://steamcommunity.com".ToUri(), cookies, true, true);
            webControl1.DocumentReady += webControl1_DocumentReady;
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Webpage failed to load!", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    var url = "http://steamcommunity.com/profiles/" + SID.ConvertToUInt64() + "/inventory/";
                    webControl1.Source = url.ToUri();   
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                }
            }
        }

        void webControl1_DocumentReady(object sender, UrlEventArgs e)
        {
            webControl1.ResetZoom();
            while (webControl1.ExecuteJavascriptWithResult("document.body.innerHTML").IsUndefined)
            {                
                WebCore.Update();
            }            
            var script = @"var $j = jQuery.noConflict();
                            $j(function() {
                                $j('#global_header').hide();
                                $j('.profile_small_header_bg').hide();
                                $j('.inventory_links').hide();
                                $j('#footer_spacer').hide();
                                $j('#footer').hide();
                            });";
            webControl1.ExecuteJavascript(script);
            script = @" var scrollbarCSS = '::-webkit-scrollbar { width: 14px !important; height: 14px !important; } ::-webkit-scrollbar-track { background-color: #111111 !important;	} ::-webkit-scrollbar-thumb { background-color: #444444 !important; } ::-webkit-scrollbar-thumb:hover { background-color: #5e5e5e !important; } ::-webkit-scrollbar-corner { background-color: #111111 !important; }';              
                            var head = document.getElementsByTagName('head')[0];
                            var style = document.createElement('style');
                            style.type = 'text/css';
                            style.innerHTML = scrollbarCSS;
                            head.appendChild(style);
                            ";
            webControl1.ExecuteJavascript(script);
        }

        private void ShowBackpack_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
