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
        string trade_url = ""; //SteamTrade.Trade.baseTradeURL;

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
            string cookies = ""; // Trade.cookies.GetCookieHeader(new Uri(trade_url));
            cookies += "; steamMachineAuth76561198065838051=" + SteamBot.Bot.MachineAuthData
                + "; strInventoryLastContext=440_2; "
            + "bCompletedTradeTutorial=true; strTradeLastInventoryContext=440_2";
            Console.WriteLine(cookies);
            InternetSetCookie(trade_url, null, cookies);
            webBrowser1.Navigate(trade_url);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteNewDocument();
        }
    }
}
