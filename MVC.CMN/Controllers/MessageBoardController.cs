using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.CMN.DataContexts;
using MVC.CMN.Models;
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
                
                foreach (var b in context.Boards) {
                    var boarditem = new Boarditem() {
                        Title = b.Name,
                        Description = b.Description,
                        Id = b.BoardId
                    };
                    model.Boarditems.Add(boarditem);
                }
            }


            return View(model);
        }
    }
}