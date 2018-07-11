using MVC.CMN.DataContexts;
using MVC.CMN.Models;
using MVC.CMN.Models.MessageBoard;
using System;
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

                //foreach (var b in context.Boards.Include("Threads")) {
                //    var boarditem = new Board() {
                //        BoardId = b.BoardId,
                //        Name = b.Name,
                //        Description = b.Description,
                //        Threads = b.Threads
                //    };
                //    model.Boarditems.Add(boarditem);
                //}

                //This feels like it could be done in a more efficient way =S
                foreach (var b in context.Boards) {
                    var boarditem = new Board() {
                        BoardId = b.BoardId,
                        Name = b.Name,
                        Description = b.Description,
                        Threads = b.Threads
                        
                    };
                    model.Boarditems.Add(boarditem);
                }

                foreach (var item in model.Boarditems) {
                    item.Threads = context.Threads
                        .Where(t => t.BoardId == item.BoardId)
                        .OrderByDescending(d => d.Created)
                        .Take(3)
                        .ToList();
                }

                foreach (var item in model.Boarditems) {
                    foreach (var thread in item.Threads) {
                        thread.Posts = context.Posts
                        .Where(p => p.ThreadId == thread.ThreadId)
                        .OrderByDescending(d => d.Created)
                        .Take(3)
                        .ToList();
                    }
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

                foreach (var item in board.Threads)
                {
                    item.Posts = context.Posts
                        .Where(p => p.ThreadId == item.ThreadId)
                        .OrderByDescending(d => d.Created)
                        .Take(1)
                        .ToList();
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


        public ActionResult CreateNewThread(string threadtitle, string threadcontent)
        {



                return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewPost(string postcontent, string threadId, string userId)
        {
            using (ForumDbContext context = new ForumDbContext())
            {
                Thread relevantthread = context.Threads.Find(threadId);


                Post post = new Post() { Subject = relevantthread.Subject, Content = postcontent, Thread = relevantthread, ThreadId = Convert.ToInt32(threadId),   };

                context.Posts.Add(post);
                context.SaveChanges();
            }

            return View("Index");
        }





        public ActionResult EditThread(string id)
        {

            return View();
        }

        public ActionResult DeleteThread(string id)
        {

            return View();
        }



        public ActionResult EditPost(string id)
        {

            return View();
        }

        public ActionResult DeletePost(string id)
        {

            return View();
        }




        //public ActionResult CreateNewPost(NewPostViewModel model) {
        //    return View();
        //}
    }

    //public class NewPostViewModel {
    //    public int ThreadId { get; set; }
    //    public string Content { get; set; }
    //}
}