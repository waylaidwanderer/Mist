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
        public static ShowTrade ShowTrade;

        static List<ListOtherOfferings> list = new List<ListOtherOfferings>();

        public ListOtherOfferings(string itemName, ulong itemID)
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
            ListOtherOfferings item = new ListOtherOfferings(itemName, itemID);
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
