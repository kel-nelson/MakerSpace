using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makerspace.Utils
{
    public static class App_Utils
    {
        public static string App_Get_Title()
        {
            return App_Setting_Get("App.Title");
        }

        public static string App_Get_Name()
        {
            return App_Setting_Get("App.Name");
        }

        public static string App_Setting_Get(string name)
        {
            return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings[name]);
        }
    }
}