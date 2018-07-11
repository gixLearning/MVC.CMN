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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateNewThread(string threadtitle, string threadcontent, int boardId, string userId)
        {

            using (ForumDbContext context = new ForumDbContext())
            {
                Board board = context.Boards.Find(boardId);

                Thread thread = new Thread() { Subject = threadtitle, BoardId = boardId, Board = board, Created = DateTime.UtcNow, CreatedBy = userId };
                context.Threads.Add(thread);



                Post post = new Post() { Subject = thread.Subject, Content = threadcontent, Thread = thread, ThreadId = thread.ThreadId, Created = DateTime.UtcNow, CreatedBy = userId  };


                thread.Posts.Add(post);


                //Generera ett post-objekt också, sen lägg till i thread
                context.Posts.Add(post);
                context.SaveChanges();


                return RedirectToAction("ShowBoard", new { id = boardId });

            }


        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateNewPost(string postcontent, int threadId, string userId)
        {
            using (ForumDbContext context = new ForumDbContext())
            {
                Thread thread = context.Threads.Find(threadId);

                Post post = new Post() { Subject = thread.Subject, Content = postcontent, Thread = thread, ThreadId = threadId, Created = DateTime.UtcNow,  CreatedBy = userId };


                thread.Posts.Add(post);

                context.Posts.Add(post);
                context.SaveChanges();


                return RedirectToAction("ShowThread", new { id = threadId });

            }
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



    }

}