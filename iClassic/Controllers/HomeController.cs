﻿using iClassic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iClassic.Controllers
{
    [Override.Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(new DashboardModel() { GraphData = string.Empty });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}