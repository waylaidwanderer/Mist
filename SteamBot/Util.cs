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
        public static void LoadTheme(MetroFramework.Components.MetroStyleManager MetroStyleManager)
        {
            Friends.globalThemeManager.Add(MetroStyleManager);
            MetroStyleManager.Theme = Friends.globalStyleManager.Theme;
            MetroStyleManager.Style = Friends.globalStyleManager.Style;
        }

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

        public static string GetPrice(int defindex, int quality, SteamTrade.Inventory.Item inventoryItem, bool gifted = false, int attribute = 0)
        {
            try
            {
                var item = SteamTrade.Trade.CurrentSchema.GetItem(defindex);
                string craftable = inventoryItem.IsNotCraftable ? "Non-Craftable" : "Craftable";
                double value = BackpackTF.CurrentSchema.Response.Items[item.ItemName].Prices[quality.ToString()]["Tradable"][craftable]["0"].Value;
                double keyValue = BackpackTF.KeyPrice;
                double billsValue = BackpackTF.BillPrice * keyValue;
                double budValue = BackpackTF.BudPrice * keyValue;

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
                else if (value >= keyValue && !item.ItemName.Contains("Crate Key"))
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

        public static string GetItemName(Schema.Item schemaItem, Inventory.Item inventoryItem, bool id = false)
        {
            var currentItem = Trade.CurrentSchema.GetItem(schemaItem.Defindex);
            string name = "";
            var quality = Convert.ToInt32(inventoryItem.Quality);
            if (quality != 6)
                name += QualityToName(quality) + " ";
            name += currentItem.ItemName;
            if (quality == 5)
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
                catch
                {

                }
            }
            if (currentItem.CraftMaterialType == "supply_crate")
            {
                for (int count = 0; count < inventoryItem.Attributes.Length; count++)
                {
                    if (inventoryItem.Attributes[count].Defindex == 187)
                    {
                        name += " #" + (inventoryItem.Attributes[count].FloatValue);
                    }
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
                        string paint = PaintToName(inventoryItem.Attributes[count].FloatValue);
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

        public static bool IsItemGifted(Inventory.Item inventoryItem)
        {
            try
            {
                for (int count = 0; count < inventoryItem.Attributes.Length; count++)
                {
                    if (inventoryItem.Attributes[count].Defindex == 186)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Item has no attributes... or something.
            }
            return false;
        }

        public static string QualityToName(int quality)
        {
            foreach (var key in Trade.CurrentSchema.Qualities)
            {
                if (key.Value == quality)
                {
                    foreach (var key2 in Trade.CurrentSchema.QualityNames)
                    {
                        if (key2.Key == key.Key)
                        {
                            return key2.Value;
                        }
                    }
                }
            }
            return "";
        }

        static string EffectToName(float defindex)
        {
            foreach (var effect in Trade.CurrentSchema.AttachedParticles)
            {
                if (effect.Defindex == defindex)
                    return effect.Name;
            }
            return "";
        }

        static string PaintToName(float color)
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
    }
}
