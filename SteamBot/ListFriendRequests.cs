using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MistClient
{
    class ListFriendRequests
    {
        string name;
        ulong sid;
        string status;
        public static Friends friends;

        static List<ListFriendRequests> list = new List<ListFriendRequests>();

        public ListFriendRequests(string name, ulong sid, string status = "Offline")
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

        public ulong SteamID
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
            ListFriendRequests item = new ListFriendRequests(name, sid, status);
            list.Add(item);
        }

        public static void Remove(ulong sid)
        {
            ListFriendRequests item = list.Find(x => x.sid == sid);
            list.Remove(item);
        }

        public static void Clear()
        {
            list.Clear();
        }

        public static void UpdateStatus(ulong sid, string status)
        {
            ListFriendRequests item = null;
            try
            {
                item = list.Find(x => x.SteamID == sid);
                item.Status = status;
                friends.RefreshObject(list);
            }
            catch
            {
                // Friends form hasn't been initialized yet, so let's not worry about it
            }
        }

        public static bool Find(ulong sid)
        {
            return list.Any(x => x.SteamID == sid);
        }

        public static ulong GetSID(string name)
        {
            ListFriendRequests item = null;
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

        static internal List<ListFriendRequests> Get()
        {
            List<ListFriendRequests> returnList = new List<ListFriendRequests>();
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "Online")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "LookingToTrade")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "LookingToPlay")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "Busy")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "Away")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "Snooze")
                    returnList.Add(item);
            }
            foreach (ListFriendRequests item in list)
            {
                if (item.status.ToString() == "Offline")
                    returnList.Add(item);
            }
            return returnList;
        }
    }
}
