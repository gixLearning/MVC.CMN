




using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.CMN.DataContexts;
using MVC.CMN.Models;
using MVC.CMN.Models.MessageBoard;

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


        //public ActionResult MessageBoard() {


        //    return View("MessageBoard", StaticData.Boards);
        //}


        public ActionResult NewsLoader()

        {
            using (ForumDbContext context = new ForumDbContext())
            {
                List<Thread> newsitems = new List<Thread>();

                newsitems.AddRange(context.Threads
                        .Include(t => t.Posts)
                        .Include(t => t.UserProfile)
                        .Where(t => t.BoardId == 4)
                        .OrderByDescending(d => d.Created)
                        .Take(5)
                        .ToList()
                        );


                return PartialView("_NewsDisplay", newsitems);

            }
        }











//public ActionResult ShowBoard(string id)
//        {



//            return View("SingleBoard", StaticData.Boards.Find(x => x.Id == Convert.ToInt32(id)));
//        }


//        public ActionResult ShowThread(string id)
//        {



//            return View("SingleThread", StaticData.Threads.Find(x => x.Id == Convert.ToInt32(id)));
//        }




    }
}