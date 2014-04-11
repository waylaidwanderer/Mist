using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SteamTrade;

namespace MistClient
{
    class BackpackTF
    {
        public static BackpackTF CurrentSchema;
        public static double KeyPrice;
        public static double BillPrice;
        public static double BudPrice;

        public static BackpackTF FetchSchema()
        {
            var apiKey = Properties.Settings.Default.backpackTfApiKey;
            var url = "http://backpack.tf/api/IGetPrices/v4/?key=" + apiKey + "&compress=1&raw=1";

            string cachefile = "tf_pricelist.cache";
            string result = "";

            TimeSpan difference = DateTime.Now - System.IO.File.GetCreationTime(cachefile);

            if (System.IO.File.Exists(cachefile) && difference.TotalMinutes < 10)
            {
                TextReader reader = new StreamReader(cachefile);
                result = reader.ReadToEnd();
                reader.Close();
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch
                {
                    response = null;
                }

                DateTime SchemaLastRequested = response.LastModified;


                if (response != null)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                        //Close and clean up the StreamReader
                        sr.Close();
                    }
                    File.WriteAllText(cachefile, result);
                    System.IO.File.SetCreationTime(cachefile, SchemaLastRequested);
                }
                else
                {
                    TextReader reader = new StreamReader(cachefile);
                    result = reader.ReadToEnd();
                    reader.Close();
                }
                response.Close();
            }

            BackpackTF schemaResult = JsonConvert.DeserializeObject<BackpackTF>(result);
            UpdateBasePrices(schemaResult);
            return schemaResult ?? null;
        }

        static void UpdateBasePrices(BackpackTF schemaResult)
        {
            KeyPrice = schemaResult.Response.Items["Mann Co. Supply Crate Key"].Prices["6"]["Tradable"]["Craftable"]["0"].Value;
            BillPrice = schemaResult.Response.Items["Bill's Hat"].Prices["6"]["Tradable"]["Craftable"]["0"].Value;
            BudPrice = schemaResult.Response.Items["Earbuds"].Prices["6"]["Tradable"]["Craftable"]["0"].Value;
        }

        [JsonProperty("response")]
        public BackpackTFResponse Response { get; set; }

        public class BackpackTFResponse
        {
            [JsonProperty("success")]
            public int Success { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("current_time")]
            public long CurrentTime { get; set; }

            [JsonProperty("raw_usd_value")]
            public double RawUsdValue { get; set; }

            [JsonProperty("usd_currency")]
            public string UsdCurrencyName { get; set; }

            [JsonProperty("usd_currency_index")]
            public string UsdCurrencyDefindex { get; set; }

            [JsonProperty("items")]
            public Dictionary<string, BackpackTFItem> Items { get; set; }
        }

        public class BackpackTFItem
        {
            // This always seems to be there.
            [JsonProperty("defindex")]
            public dynamic Defindex { get; set; }

            [JsonProperty("prices")]
            public Dictionary<string,
                Dictionary<string,
                    Dictionary<string, 
                        Dictionary<string, BackpackTFItemPrices>>>> Prices { get; set; }
        }

        public class BackpackTFItemPrices
        {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("value_high")]
            public double ValueHigh { get; set; }

            [JsonProperty("value_raw")]
            public double ValueRaw { get; set; }

            [JsonProperty("last_update")]
            public int LastUpdate { get; set; }

            [JsonProperty("difference")]
            public double Difference { get; set; }
        }
    }
}
