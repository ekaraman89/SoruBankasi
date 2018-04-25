﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SoruBankasi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(name: "Home", url: "", defaults: new { controller = "Home", action = "Index" });
            routes.MapRoute(name: "Login", url: "GirisYap", defaults: new { controller = "Auth", action = "Login" });
            routes.MapRoute(name: "Logout", url: "CiksiYap", defaults: new { controller = "Auth", action = "Logout" });
        }
    }
}
