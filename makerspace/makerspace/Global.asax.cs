﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace makerspace
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static List<string> Demo_Accounts = new List<string>();
        protected void Application_Start()
        {
            foreach(String username in Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["App.Accounts.Demo"]).Split(';'))
            {
                Demo_Accounts.Add(username);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
