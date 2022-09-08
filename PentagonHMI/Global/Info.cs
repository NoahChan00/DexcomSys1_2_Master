using Logix;
using System.Linq;

namespace PentagonHMI.Info
{
    public static class SQL
    {
        public static string ServerName => @Classes.GlobalFunctions.ServerName;
        public static string DatabaseName => @Classes.GlobalFunctions.DatabaseName;
        public static bool IntegratedSecurity => Classes.GlobalFunctions.IntegratedSecurity;
        public static bool PersistSecurityInfo => Classes.GlobalFunctions.PersistSecurityInfo;
        public static string UserID => @Classes.GlobalFunctions.UserID;
        public static string Password => @Classes.GlobalFunctions.Password;

        public static class Select
        {
            public static string Config => "SELECT TOP 1 [INFO] FROM [CONFIG] WHERE [ITEM] = '{0}'";
            public static string TagName => "SELECT TOP 1 [ADDRESS] FROM [TAGNAME] WHERE [NAME] = '{0}'";
        }
    }

    public static class OPC
    {
        public enum Key
        {
            Setting,
            MainView,
            Engineering,
            OEE,
            Lifter,
            TStation
        }

        public static string IP => Classes.GlobalFunctions.PLC_IPAddress;

        public static Controller Ctrl = new Controller(IP);

        public static TagGroup[] Ary_TagGroup = new TagGroup[10];

        public static void Add_TagGroup(ref TagGroup Tgrp)
        {
            for(int n = 0; n < Ary_TagGroup.Count(); n++)
                if(Ary_TagGroup[n] == null || Ary_TagGroup[n].Tags.Count == 0)
                {
                    Ary_TagGroup[n] = Tgrp;
                    return;
                }
        }

        public static void Initialize()
        {
            Ctrl.Connect();

            for(int n = 0; n < Ary_TagGroup.Count(); n++)
            {
                if(Ary_TagGroup[n] == null)
                    Ary_TagGroup[n] = new TagGroup { Active = false };
            }
        }

        public static void Update()
        {
            //DateTime Dt = DateTime.Now;
            Ctrl.GroupRead(Ary_TagGroup);
            //TimeSpan wow = DateTime.Now - Dt;
            //double n = wow.TotalSeconds;
        }

        public static class TagGroups
        {
            public static TagGroup Setting = new TagGroup { MyObject = Key.Setting, Active = false };
            public static TagGroup OEE = new TagGroup { MyObject = Key.OEE, Active = false };
            public static TagGroup Lifter = new TagGroup { MyObject = Key.Lifter, Active = true };
            public static TagGroup TStation = new TagGroup { MyObject = Key.TStation, Active = false };
        }
    }
}