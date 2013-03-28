using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListInventory
    {
        string itemName;
        string imageURL;
        ulong itemID;
        string price;
        public static ShowTrade ShowTrade;

        static List<ListInventory> list = new List<ListInventory>();

        public ListInventory(string itemName, ulong itemID, string imageURL, string price)
        {
            this.itemName = itemName;
            this.itemID = itemID;
            this.imageURL = imageURL;
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

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        public string ItemPrice
        {
            get { return price; }
            set { }
        }

        public static void Add(string itemName, ulong itemID, string imageURL, string price = null)
        {
            ListInventory item = new ListInventory(itemName, itemID, imageURL, price);
            list.Add(item);
        }

        public static void Remove(string itemName, ulong itemID)
        {
            ListInventory item = list.Find(x => x.itemName == itemName && x.itemID == itemID);
            list.Remove(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        static internal List<ListInventory> Get()
        {
            return list;
        }

        static internal List<ListInventory> Get(string name)
        {
            name = name.ToLower();
            List<ListInventory> returnList = new List<ListInventory>();
            foreach (ListInventory item in list)
            {
                if (item.itemName.ToLower().Contains(name))
                    returnList.Add(item);
            }
            return returnList;
        }
    }
}
