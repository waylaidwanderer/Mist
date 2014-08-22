using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using MetroFramework.Forms;
using SteamTrade;

namespace MistClient
{
    class Util
    {
        public static string UpdateCheckUrl = "http://jzhang.net/mist/update.php";

        public static void LoadTheme(MetroFramework.Forms.MetroForm Form, System.Windows.Forms.Control.ControlCollection Controls, MetroFramework.Controls.MetroUserControl MetroUserControl = null)
        {
            foreach (var form in System.Windows.Forms.Application.OpenForms)
            {
                if (form is MetroForm && !form.Equals(Form))
                {
                    try
                    {
                        var metroForm = (MetroForm)form;
                        metroForm.Theme = metroForm.StyleManager.Theme = Friends.GlobalStyleManager.Theme;
                        metroForm.Style = metroForm.StyleManager.Style = Friends.GlobalStyleManager.Style;
                        metroForm.Refresh();
                    }
                    catch
                    {

                    }                    
                }          
            }
            if (Controls != null)
            {
                foreach (System.Windows.Forms.Control control in Controls)
                {
                    try
                    {
                        if (control.GetType().GetProperties().Any(x => x.Name == "StyleManager") || (control.GetType().GetProperties().Any(x => x.Name == "Theme") && control.GetType().GetProperties().Any(x => x.Name == "Style")))
                        {
                            control.GetType().GetProperty("Theme").SetValue(control, Friends.GlobalStyleManager.Theme, null);
                            control.GetType().GetProperty("Style").SetValue(control, Friends.GlobalStyleManager.Style, null);
                            var styleManager = control.GetType().GetProperty("StyleManager").GetValue(control, null) as MetroFramework.Components.MetroStyleManager;
                            styleManager = Friends.GlobalStyleManager;
                            styleManager.Style = Friends.GlobalStyleManager.Style;
                            styleManager.Theme = Friends.GlobalStyleManager.Theme;
                            control.Refresh();
                        }
                        LoadTheme(null, control.Controls);
                    }
                    catch
                    {

                    }                    
                }
            }
            if (Form != null)
            {
                Form.Theme = Form.StyleManager.Theme = Friends.GlobalStyleManager.Theme;
                Form.Style = Form.StyleManager.Style = Friends.GlobalStyleManager.Style;
                Form.Refresh();
            }            
            if (MetroUserControl != null)
            {
                MetroUserControl.Theme = MetroUserControl.StyleManager.Theme = Friends.GlobalStyleManager.Theme;
                MetroUserControl.Style = MetroUserControl.StyleManager.Style = Friends.GlobalStyleManager.Style;
                MetroUserControl.Refresh();
            }
        }

        public static string ParseBetween(string Subject, string Start, string End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public static string[] GetStringInBetween(string strBegin,
                                                  string strEnd, string strSource,
                                                  bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired
                if (includeBegin)
                    iIndexOfBegin -= strBegin.Length;
                strSource = strSource.Substring(iIndexOfBegin
                    + strBegin.Length);
                int iEnd = strSource.IndexOf(strEnd);
                if (iEnd != -1)
                {
                    // include the End string if desired
                    if (includeEnd)
                        iEnd += strEnd.Length;
                    result[0] = strSource.Substring(0, iEnd);
                    // advance beyond this segment
                    if (iEnd + strEnd.Length < strSource.Length)
                        result[1] = strSource.Substring(iEnd
                            + strEnd.Length);
                }
            }
            else
                // stay where we are
                result[1] = strSource;
            return result;
        }

        public static double GetSteamMarketPrice(SteamBot.Bot bot, GenericInventory.Inventory.Item item, bool withFee = true)
        {
            var url = string.Format("http://steamcommunity.com/market/listings/{0}/{1}/render?currency=1", item.AppId, item.MarketHashName);
            var response = SteamWeb.Fetch(url, "GET", null, bot.botCookies, false);
            try
            {
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                List<double> prices = new List<double>();
                int count = 0;
                var listings = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Convert.ToString(json.listinginfo));
                foreach (var listingItem in listings.Values)
                {
                    if (listingItem.price == 0) continue;
                    if (count == 3) break;
                    var basePrice = (double)listingItem.converted_price;
                    var fee = withFee ? (double)listingItem.converted_fee : 0;
                    var price = (double)((basePrice + fee) / 100);
                    prices.Add(price);
                    count++;
                }
                var totalPrices = 0.0;
                foreach (var price in prices)
                {
                    totalPrices += price;
                }
                var averagePrice = prices.Count > 0 ? Math.Round(totalPrices / prices.Count, 2) : 0;
                return averagePrice;
            }
            catch
            {
                return 0;
            }            
        }

        public static string GetSteamIDInfo(SteamBot.Bot bot, SteamKit2.SteamID steamId)
        {
            string output = "";
            output += "| steamname: " + bot.SteamFriends.GetFriendPersonaName(steamId);
            output += "\r\n| steamID32: " + steamId.ToString();
            output += "\r\n| steamID64: http://steamcommunity.com/profiles/" + steamId.ConvertToUInt64();
            output += "\r\n|  steamrep: http://steamrep.com/profiles/" + steamId.ConvertToUInt64();
            return output;
        }

        public static string GetSteamRepStatus(ulong sid, bool useCache = false)
        {
            if (useCache)
            {
                var file = Path.Combine(System.Windows.Forms.Application.StartupPath, "steamrep.cache");
                var cache = ReadAllLines(file);
                foreach (var line in cache)
                {
                    if (line.Contains(sid.ToString()))
                    {
                        var lastChecked = Util.ParseBetween(line, "Date:", ".");
                        var dateLast = Convert.ToDateTime(lastChecked);
                        var dateNow = DateTime.Now;
                        var difference = dateNow - dateLast;
                        if (difference.TotalDays >= 1) break;
                        return Util.ParseBetween(line, "Rep:", ";");
                    }
                }
                return GetSteamRepStatus(sid);
            }
            else
            {
                try
                {
                    // This is a proxy for SteamRep's beta API. Not recommended for heavy/wide usage.
                    string url = "http://scam.io/profiles/" + sid;
                    string response = SteamTrade.SteamWeb.Fetch(url);
                    if (response != "")
                    {
                        var status = Util.ParseBetween(response, "<reputation>", "</reputation>");
                        WriteToSteamRepCache(sid.ToString(), status);
                        return status;
                    }
                }
                catch { }
            }     
            return "";
        }

        private static Object locker = new Object();

        public static void WriteToSteamRepCache(string steamId, string rep)
        {
            lock (locker)
            {
                if (rep == "") rep = "None";
                var newFile = new StringBuilder();
                var filePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "steamrep.cache");
                var file = ReadAllLines(filePath);
                var add = "SteamID:" + steamId + ";Rep:" + rep + ";Date:" + DateTime.Now + ".\r\n";
                var found = false;
                foreach (var line in file)
                {
                    if (line.Contains(steamId))
                    {
                        found = true;
                        newFile.Append(line.Replace(line, add));
                        continue;
                    }
                    newFile.Append(line + "\r\n");
                }
                if (!found)
                    newFile.Append(add + "\r\n");
                File.WriteAllText(filePath, newFile.ToString());
            }            
        }

        public static System.Drawing.Bitmap GetAvatar(string path)
        {
            try
            {
                if (path == null)
                    return MistClient.Properties.Resources.IconUnknown;
                return (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(path);
            }
            catch
            {
                return MistClient.Properties.Resources.IconUnknown;
            }
        }

        public static void ShowSteamProfile(SteamBot.Bot bot, ulong steamId)
        {
            var form = new MetroForm();
            form.Text = "Steam Community";
            form.Width = 800;
            form.Height = 600;
            form.Style = Friends.GlobalStyleManager.Style;
            form.Theme = Friends.GlobalStyleManager.Theme;
            form.Icon = MistClient.Properties.Resources.Icon;
            form.ShadowType = MetroFormShadowType.DropShadow;
            var webControl = new Awesomium.Windows.Forms.WebControl();
            webControl.Dock = System.Windows.Forms.DockStyle.Fill;
            string cookies = string.Format("steamLogin={0}; sessionid={1}", bot.token, bot.sessionId);
            webControl.WebSession = Awesomium.Core.WebCore.CreateWebSession(new Awesomium.Core.WebPreferences());
            webControl.WebSession.SetCookie(new Uri("http://steamcommunity.com"), cookies, true, true);
            webControl.Source = new Uri((string.Format("http://steamcommunity.com/profiles/{0}/", steamId)));
            webControl.DocumentReady += webControl_DocumentReady;
            webControl.TitleChanged += (s, e) => webControl_TitleChanged(s, e, form);
            form.Controls.Add(webControl);
            form.Show();
        }

        static void webControl_DocumentReady(object sender, Awesomium.Core.UrlEventArgs e)
        {
            var webControl = (Awesomium.Windows.Forms.WebControl)sender;
            while (webControl.ExecuteJavascriptWithResult("document.body.innerHTML").IsUndefined)
            {
                Awesomium.Core.WebCore.Update();
            }
            var script = @" var scrollbarCSS = '::-webkit-scrollbar { width: 14px !important; height: 14px !important; } ::-webkit-scrollbar-track { background-color: #111111 !important;	} ::-webkit-scrollbar-thumb { background-color: #444444 !important; } ::-webkit-scrollbar-thumb:hover { background-color: #5e5e5e !important; } ::-webkit-scrollbar-corner { background-color: #111111 !important; }';              
                            var head = document.getElementsByTagName('head')[0];
                            var style = document.createElement('style');
                            style.type = 'text/css';
                            style.innerHTML = scrollbarCSS;                            
                            head.appendChild(style);
                            ";
            webControl.ExecuteJavascript(script);
        }

        private static void webControl_TitleChanged(object sender, Awesomium.Core.TitleChangedEventArgs e, MetroForm parentForm)
        {
            parentForm.Text = e.Title;
            parentForm.Refresh();
            ((Awesomium.Windows.Forms.WebControl)sender).TitleChanged -= (s, ev) => webControl_TitleChanged(s, ev, parentForm);
        }

        public static string[] ReadAllLines(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

            System.IO.StreamReader sr = new System.IO.StreamReader(fs);

            List<String> lst = new List<string>();

            while (!sr.EndOfStream)
                lst.Add(sr.ReadLine());

            return lst.ToArray();
        }

        public static System.Drawing.Color GetColorFromPersonaState(SteamBot.Bot bot, SteamKit2.SteamID steamId)
        {
            var state = bot.SteamFriends.GetFriendPersonaState(steamId);
            if (state != SteamKit2.EPersonaState.Offline)
            {
                var isPlayingGame = !string.IsNullOrEmpty(bot.SteamFriends.GetFriendGamePlayedName(steamId));
                if (isPlayingGame)
                    return (System.Drawing.Color)System.Drawing.ColorTranslator.FromHtml("#81b900");
                else
                    return (System.Drawing.Color)System.Drawing.ColorTranslator.FromHtml("#5db2ff");
            }
            return System.Drawing.ColorTranslator.FromHtml("#8a8a8a");
        }

        public static void SendUsageStats(ulong steamId)
        {
            string systemName = string.Empty;
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (System.Management.ManagementObject os in searcher.Get())
            {
                systemName = os["Caption"].ToString();
                break;
            }
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("steamId", steamId.ToString());
            data.Add("os", systemName);
            SteamWeb.Fetch("http://jzhang.net/mist/stats.php", "POST", data);
        }

        public static void StyleWebcontrolScrollbars(ref Awesomium.Windows.Forms.WebControl webcontrol)
        {
            var script = @" var css = ""::-webkit-scrollbar { width: 12px; } ::-webkit-scrollbar-track { background-color: #111111;	} ::-webkit-scrollbar-thumb { background-color: #444444; } ::-webkit-scrollbar-thumb:hover { background-color: #5e5e5e;	}"";
                            var style = document.createElement('style');
                            if (style.styleSheet)
                            {
                                style.styleSheet.cssText = css;
                            }
                            else 
                            {
                                style.appendChild(document.createTextNode(css));
                            }
                            document.getElementsByTagName('head')[0].appendChild(style);
                            ";
            webcontrol.ExecuteJavascript(script);
        }
    }
}
