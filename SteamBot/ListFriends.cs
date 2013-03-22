using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListFriends
    {
        string name;
        ulong sid;
        string status;
        public static Friends friends;

        static List<ListFriends> list = new List<ListFriends>();

        public ListFriends(string name, ulong sid, string status = "Offline")
        {
            this.name = name;
            this.sid = sid;
            this.status = status;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ulong SID
        {
            get { return sid; }
            set { sid = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public static void Add(string name, ulong sid, string status = "Offline")
        {
            ListFriends item = new ListFriends(name, sid, status);
            list.Add(item);
        }

        public static void Remove(ulong sid)
        {
            ListFriends item = list.Find(x => x.sid == sid);
            list.Remove(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        public static void UpdateStatus(ulong sid, string status)
        {
            ListFriends item = null;
            try
            {
                item = list.Find(x => x.SID == sid);
                item.Status = status;
                friends.SetObject(list);
            }
            catch
            {
                // Friends form hasn't been initialized yet, so let's not worry about it
            }
        }

        public static ulong GetSID (string name)
        {
            ListFriends item = null;
            try
            {
                item = list.Find(x => x.name == name);
                return item.sid;
            }
            catch
            {
                
            }
            return 0;
        }

        static internal List<ListFriends> Get(string name)
        {
            name = name.ToLower();
            List<ListFriends> returnList = new List<ListFriends>();
            foreach (ListFriends item in list)
            {
                if (item.name.ToLower().Contains(name))
                    returnList.Add(item);
            }
            return returnList;
        }

        static internal List<ListFriends> Get()
        {
            List<ListFriends> returnList = new List<ListFriends>();
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "Online")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "LookingToTrade")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "LookingToPlay")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "Busy")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "Away")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "Snooze")
                    returnList.Add(item);
            }
            foreach (ListFriends item in list)
            {
                if (item.status.ToString() == "Offline")
                    returnList.Add(item);
            }
            return returnList;
        }
    }
}
