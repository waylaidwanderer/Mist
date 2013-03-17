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
        public static ShowTrade ShowTrade;

        static List<ListUserOfferings> list = new List<ListUserOfferings>();

        public ListUserOfferings(string itemName, ulong itemID)
        {
            this.itemName = itemName;
            this.itemID = itemID;
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

        public static void Add(string itemName, ulong itemID)
        {
            ListUserOfferings item = new ListUserOfferings(itemName, itemID);
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
