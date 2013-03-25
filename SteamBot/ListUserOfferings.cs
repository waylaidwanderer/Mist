using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListUserOfferings
    {
        string itemName;
        ulong itemID;
        string price;

        static List<ListUserOfferings> list = new List<ListUserOfferings>();

        public ListUserOfferings(string itemName, ulong itemID, string price)
        {
            this.itemName = itemName;
            this.itemID = itemID;
            this.price = price;
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public ulong ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public string ItemPrice
        {
            get { return price; }
            set { }
        }

        public static void Add(string itemName, ulong itemID, string price = null)
        {
            ListUserOfferings item = new ListUserOfferings(itemName, itemID, price);
            list.Add(item);
        }

        public static void Remove(string itemName, ulong itemID)
        {
            ListUserOfferings item = list.Find(x => x.itemName == itemName && x.itemID == itemID);
            list.Remove(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        static internal List<ListUserOfferings> Get()
        {
            return list;
        }
    }
}
