using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListBackpack
    {
        string itemName;
        static List<ListBackpack> list = new List<ListBackpack>();

        public ListBackpack(string itemName)
        {
            this.itemName = itemName;
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public static void Add(string itemName)
        {
            ListBackpack item = new ListBackpack(itemName);
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
