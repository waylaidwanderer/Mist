using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace MistClient
{
    class Util
    {
        public static string HTTPRequest(string url)
        {
            var result = "";
            try
            {
                using (var webClient = new WebClient())
                {
                    using (var stream = webClient.OpenRead(url))
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var wtf = ex.Message;
            }

            return result;
        }

        public static HttpWebResponse Fetch(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            HttpWebResponse response;
            for (int count = 0; count < 10; count++)
            {
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                    return response;
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("retry");
                }
            }
            return null;
        }

        public static string ParseBetween(string Subject, string Start, string End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public static string GetPrice(int defindex, int quality, SteamTrade.Inventory.Item inventoryItem, bool gifted = false, int attribute = 0)
        {
            BackpackTF.CurrentSchema = BackpackTF.FetchSchema();
            try
            {
                double value = BackpackTF.CurrentSchema.Response.Prices[defindex][quality][attribute].Value;
                double keyValue = BackpackTF.CurrentSchema.Response.Prices[5021][6][0].Value;
                double billsValue = BackpackTF.CurrentSchema.Response.Prices[126][6][0].Value;
                double budValue = BackpackTF.CurrentSchema.Response.Prices[143][6][0].Value;

                var item = SteamTrade.Trade.CurrentSchema.GetItem(defindex);
                string result = "";

                if (inventoryItem.IsNotCraftable)
                {
                    value = value / 2.0;
                }
                if (inventoryItem.IsNotTradeable)
                {
                    value = value / 2.0;
                }
                if (gifted)
                {
                    value = value * 0.75;
                }
                if (quality == 3)
                {
                    if (item.CraftMaterialType == "weapon")
                    {
                        int level = inventoryItem.Level;
                        switch (level)
                        {
                            case 0:
                                value = billsValue + 5.11;
                                break;
                            case 1:
                                value = billsValue;
                                break;
                            case 42:
                                value = value * 10.0;
                                break;
                            case 69:
                                value = billsValue;
                                break;
                            case 99:
                                value = billsValue;
                                break;
                            case 100:
                                value = billsValue;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (item.CraftMaterialType == "hat")
                    {
                        int level = inventoryItem.Level;
                        switch (level)
                        {
                            case 0:
                                value = value * 10.0;
                                break;
                            case 1:
                                value = value * 5.0;
                                break;
                            case 42:
                                value = value * 3.0;
                                break;
                            case 69:
                                value = value * 4.0;
                                break;
                            case 99:
                                value = value * 4.0;
                                break;
                            case 100:
                                value = value * 6.0;
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (value >= budValue * 1.33)
                {
                    value = value / budValue;
                    result = value.ToString("0.00") + " buds";
                }
                else if (value > keyValue)
                {
                    value = value / keyValue;
                    result = value.ToString("0.00") + " keys";
                }
                else
                {
                    result = value.ToString("0.00") + " ref";
                }

                return result;
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
