﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.CMN.Models;

namespace MVC.CMN.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult MessageBoard() {


            return View();
        }

        public ActionResult DynamicNewsitem(int id)
        {

            Newsitem model = StaticData.NewsBase.Find(x => x.Id == id);

            return PartialView("_DynamicNewsitem", model);
        }

    }
}