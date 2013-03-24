using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListBackpack
    {
        string itemName;
        int defindex;
        string url;
        string price;
        static List<ListBackpack> list = new List<ListBackpack>();

        public ListBackpack(string itemName, int defindex, string url, string price)
        {
            this.itemName = itemName;
            this.defindex = defindex;
            this.url = url;
            this.price = price;
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string ItemURL
        {
            get { return url; }
            set { url = value; }
        }

        public int DefIndex
        {
            get { return defindex; }
            set { }
        }

        public string ItemPrice
        {
            get { return price; }
            set { }
        }

        public static void Add(string itemName, int defindex, string url, string price = null)
        {
            ListBackpack item = new ListBackpack(itemName, defindex, url, price);
            list.Add(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        static internal List<ListBackpack> Get()
        {
            return list;
        }
    }
}
