using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using SteamKit2;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SteamTrade;
using System.Web;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using Awesomium.Core;
using Awesomium.Windows.Forms;

namespace MistClient
{
    public partial class ShowTrade_Web : Form
    {
        string trade_url = SteamTrade.Trade.baseTradeURL;

        public ShowTrade_Web()
        {
            InitializeComponent();
        }

        private void ShowTrade_Web_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

        private void WriteNewDocument()
        {
            var data = new NameValueCollection();
            data.Add("sessionid", Trade.sessionIdEsc);
            data.Add("logpos", "" + Trade.LogPos);
            data.Add("version", "" + Trade.Version);
            InternetSetCookie(trade_url, null, Trade.cookies.GetCookieHeader(new Uri(trade_url)) + "; expires = Sun, 01-Jan-2014 00:00:00 GMT");
            webBrowser1.Navigate(trade_url, true);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteNewDocument();
        }
    }
}
