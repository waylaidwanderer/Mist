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
            var url = "http://backpack.tf/api/IGetPrices/v2/?format=json&currency=metal";

            string cachefile = "tf_pricelist.cache";
            string result = "";

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
            TimeSpan difference = DateTime.Now - System.IO.File.GetCreationTime(cachefile);

            if (!System.IO.File.Exists(cachefile) || ((difference.TotalMinutes > 5) && response != null))
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

            BackpackTF schemaResult = JsonConvert.DeserializeObject<BackpackTF>(result);
            UpdateBasePrices(schemaResult);
            return schemaResult ?? null;
        }

        static void UpdateBasePrices(BackpackTF schemaResult)
        {
            KeyPrice = schemaResult.Response.Prices[5021][6][0].Value;
            BillPrice = schemaResult.Response.Prices[126][6][0].Value;
            BudPrice = schemaResult.Response.Prices[143][6][0].Value;
        }

        [JsonProperty("response")]
        public BackpackTFResponse Response { get; set; }

        public class BackpackTFResponse
        {
            [JsonProperty("success")]
            public int Success { get; set; }

            [JsonProperty("current_time")]
            public long CurrentTime { get; set; }

            [JsonProperty("refined_usd_value")]
            public double RefinedValue { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            // Even though the API gives us string keys, we're going to cast
            // them to integers so accessing them is easier.  Also, we use
            // a dictionary instead of an array because of the apparent
            // non-continuity of the numbers.
            [JsonProperty("prices")]
            public Dictionary<int,
                Dictionary<int,
                    Dictionary<int, BackpackTFItem>>> Prices { get; set; }

        }

        public class BackpackTFItem
        {
            // This always seems to be there.
            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("last_change")]
            public double LastChange { get; set; }

            [JsonProperty("last_update")]
            public long LastUpdate { get; set; }
        }
    }
}
