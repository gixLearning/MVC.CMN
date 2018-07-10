using MVC.CMN.DataContexts;
using MVC.CMN.Models.MessageBoard;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC.CMN.Controllers {

    public class MessageBoardController : Controller {

        // GET: MessageBoard
        public ActionResult Index() {
            var model = new MessageBoardViewModel();
            using (ForumDbContext context = new ForumDbContext()) {
                //foreach (var b in context.Boards) {
                //    var boarditem = new Boarditem() {
                //        Title = b.Name,
                //        Description = b.Description,
                //        Id = b.BoardId
                //    };
                //    model.Boarditems.Add(boarditem);
                //}
                foreach (var b in context.Boards.Include("Threads")) {
                    var boarditem = new Board() {
                        BoardId = b.BoardId,
                        Name = b.Name,
                        Description = b.Description,
                        Threads = b.Threads
                    };
                    model.Boarditems.Add(boarditem);
                }
            }
            return View(model);
        }

        public ActionResult ShowBoard(int id) {
            Board board;
            using (ForumDbContext context = new ForumDbContext()) {
                System.Diagnostics.Debug.WriteLine("FIRST CONTEXT QUERY BEGIN");
                board = context.Boards.Where(b => b.BoardId == id).FirstOrDefault();
                //board = context.Boards.Include(t => t.Threads).Where(b => b.BoardId == id).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("FIRST CONTEXT QUERY DONE");

                if (board != null) {
                    board.Threads = context.Threads
                    .Where(t => t.BoardId == board.BoardId)
                    .OrderByDescending(d => d.Created)
                    .Take(5)
                    .ToList();
                }
                else {
                    return RedirectToAction("Index");
                }
                return View("SingleBoard", board);
            }
        }

        public ActionResult ShowThread(int id) {
            using (ForumDbContext context = new ForumDbContext()) {
                Thread thread = context.Threads.Where(t => t.ThreadId == id).Include(p => p.Posts).FirstOrDefault();

                if (thread != null) {
                    return View("SingleThread", thread);
                }
            }
            return RedirectToAction("Index");
        }
    }
}