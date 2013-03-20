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
        static List<ListBackpack> list = new List<ListBackpack>();

        public ListBackpack(string itemName, int defindex, string url)
        {
            this.itemName = itemName;
            this.defindex = defindex;
            this.url = url;
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

        public static void Add(string itemName, int defindex, string url)
        {
            ListBackpack item = new ListBackpack(itemName, defindex, url);
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
