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

namespace MistClient
{
    public partial class ShowTrade_Web : Form
    {
        string trade_url = SteamTrade.Trade.baseTradeURL;

        public ShowTrade_Web(SteamID OtherSID)
        {
            InitializeComponent();
            
            extendedWebBrowser1.UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.47 Safari/536.11";
            extendedWebBrowser1.Navigate(trade_url, null, null, "Cookies: " + Trade.cookies.GetCookieHeader(new Uri(trade_url)) + Environment.NewLine);
        }
    }
}
