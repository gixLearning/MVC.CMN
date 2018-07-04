using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.CMN.DataContexts;
using MVC.CMN.Models.MessageBoard;

namespace MVC.CMN.Controllers
{
    public class MessageBoardController : Controller
    {
        // GET: MessageBoard
        public ActionResult Index()
        {
            var model = new MessageBoardViewModel();
            using(ForumDbContext context = new ForumDbContext()) {
                model.BoardsCount = context.Boards.Count();
            }


            return View(model);
        }
    }
}