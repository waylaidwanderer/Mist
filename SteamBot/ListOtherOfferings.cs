using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListOtherOfferings
    {
        string itemName;
        ulong itemID;
        string price;

        static List<ListOtherOfferings> list = new List<ListOtherOfferings>();

        public ListOtherOfferings(string itemName, ulong itemID, string price)
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

        public static void Add(string itemName, ulong itemID, string price)
        {
            ListOtherOfferings item = new ListOtherOfferings(itemName, itemID, price);
            list.Add(item);
        }

        public static void Remove(string itemName, ulong itemID)
        {
            ListOtherOfferings item = list.Find(x => x.itemName == itemName && x.itemID == itemID);
            list.Remove(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        static internal List<ListOtherOfferings> Get()
        {
            return list;
        }
    }
}
