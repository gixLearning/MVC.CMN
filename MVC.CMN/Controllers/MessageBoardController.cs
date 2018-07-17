using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC.CMN.Attributes;
using MVC.CMN.DataContexts;
using MVC.CMN.Models;
using MVC.CMN.Models.MessageBoard;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC.CMN.Controllers {

    [Authorize]
    public class MessageBoardController : Controller {

        // GET: MessageBoard
        [AllowAnonymous]
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
                        .Include(t => t.UserProfile)
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
                        //.Take(3)
                        .ToList();
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ShowBoard(int id, int _index) {
            Board board;
            using (ForumDbContext context = new ForumDbContext()) {
                System.Diagnostics.Debug.WriteLine("FIRST CONTEXT QUERY BEGIN");
                board = context.Boards.Where(b => b.BoardId == id).FirstOrDefault();
                //board = context.Boards.Include(t => t.Threads).Where(b => b.BoardId == id).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("FIRST CONTEXT QUERY DONE");

                if (board != null) {
                    board.Threads = context.Threads
                    .Include(b => b.UserProfile)
                    .Where(t => t.BoardId == board.BoardId)
                    .OrderByDescending(d => d.Created)
                    //.Take(5)
                    .ToList();
                }
                else {
                    return RedirectToAction("Index");
                }

                foreach (var item in board.Threads) {
                    item.Posts = context.Posts
                        .Include(t => t.UserProfile)
                        .Where(p => p.ThreadId == item.ThreadId)
                        .OrderByDescending(d => d.Created)
                        //.Take(1)
                        .ToList();
                }

                return View("SingleBoard", new ShowBoardViewModel() { theBoard = board, index = _index });
            }
        }

        [AllowAnonymous]
        public ActionResult ShowThread(int id, int _index) {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            using (ForumDbContext context = new ForumDbContext()) {
                System.Diagnostics.Debug.WriteLine("THREAD QUERY");
                //Thread thread = context.Threads.Where(t => t.ThreadId == id).Include(p => p.Posts).FirstOrDefault();
                Thread thread = context.Threads
                    .Include(p => p.Posts)
                    .Include(p => p.Board)
                    .Include(p => p.Posts.Select(e => e.UserProfile))
                    //.Include(b => b.Board)
                    .Where(t => t.ThreadId == id)
                    .FirstOrDefault();

                if (thread != null) {
                    return View("SingleThread", new ShowThreadViewModel() { theThread = thread, index = _index });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        public ActionResult CreateNewBoard(string boardtitle, string boardcontent, string userId)   //userId not used because boards don't have creators. Maybe later?
        {
            using (ForumDbContext context = new ForumDbContext()) {
                Board board = new Board() { Name = boardtitle, Description = boardcontent };

                context.Boards.Add(board);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewThread(string threadtitle, string threadcontent, int boardId, string userId) {
            using (ForumDbContext context = new ForumDbContext()) {
                Board board = context.Boards.Find(boardId);

                Thread thread = new Thread() { Subject = threadtitle, BoardId = boardId, Board = board, Created = DateTime.UtcNow, CreatedBy = userId };
                context.Threads.Add(thread);

                Post post = new Post() { Subject = thread.Subject, Content = threadcontent, Thread = thread, ThreadId = thread.ThreadId, Created = DateTime.UtcNow, CreatedBy = userId };
                thread.Posts.Add(post);

                //Generera ett post-objekt också, sen lägg till i thread
                context.Posts.Add(post);
                context.SaveChanges();

                return RedirectToAction("ShowBoard", new { id = boardId, _index = 0 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewPost(string postcontent, int threadId, string userId) {
            using (ForumDbContext context = new ForumDbContext()) {
                Thread thread = context.Threads.Find(threadId);

                Post post = new Post() { Subject = thread.Subject, Content = postcontent, Thread = thread, ThreadId = threadId, Created = DateTime.UtcNow, CreatedBy = userId };
                thread.Posts.Add(post);
                context.Posts.Add(post);
                context.SaveChanges();

                return RedirectToAction("ShowThread", new { id = threadId, _index = 0 });
            }
        }

        [HttpPost]
        public ActionResult InsertEditBoardView(int boardId, string name, string description) {
            EditBoardViewModel ebvm = new EditBoardViewModel() { BoardId = boardId, Name = name, Description = description };

            return PartialView("_EditBoard", ebvm);
        }

        [HttpPost]
        public ActionResult InsertEditThreadView(int threadId, int boardId, string userId, string subject) {
            EditThreadViewModel etvm = new EditThreadViewModel() { ThreadId = threadId, BoardId = boardId, UserId = userId, Subject = subject };

            return PartialView("_EditThread", etvm);
        }

        [HttpPost]
        public ActionResult InsertEditPostView(int threadId, int postId, string userId, string content) {
            EditPostViewModel epvm = new EditPostViewModel() { ThreadId = threadId, PostId = postId, UserId = userId, Content = content };

            return PartialView("_EditPost", epvm);
        }

        [HttpPost]
        public ActionResult InsertQuotePostView(int threadId, string userId, string content)
        {
            QuotePostViewModel qpvm = new QuotePostViewModel() { ThreadId = threadId, UserId = userId, Content = content };

            return PartialView("_QuotePost", qpvm);
        }
        
        [Authorize(Roles = RoleTypes.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBoard(int boardId, string boardname, string boarddescription) {
            using (ForumDbContext context = new ForumDbContext()) {
                context.Boards.Find(boardId).Name = boardname;
                context.Boards.Find(boardId).Description = boarddescription;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [AuthorizeRoles(RoleTypes.Admin, RoleTypes.Moderator)]
        [ValidateAntiForgeryToken]
        public ActionResult EditThread(int threadId, int boardId, string userId, string threadtitle) {
            using (ForumDbContext context = new ForumDbContext()) {
                context.Threads.Find(threadId).Subject = threadtitle;
                context.SaveChanges();

                return RedirectToAction("ShowBoard", new { id = boardId, _index = 0 });
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult EditPost(string postcontent, int threadId, int postId, string userId) {
            using (ForumDbContext context = new ForumDbContext()) {
                context.Posts.Find(postId).Content = postcontent;
                context.SaveChanges();

                return RedirectToAction("ShowThread", new { id = threadId, _index = 0 });
            }
        }

        [Authorize(Roles = RoleTypes.Admin)]
        public ActionResult DeleteBoard(int id) {
            using (ForumDbContext context = new ForumDbContext()) {
                Board board = context.Boards.Find(id);

                if (board != null) {
                    context.Boards.Remove(board);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        [AuthorizeRoles(RoleTypes.Admin, RoleTypes.Moderator)]
        public ActionResult DeleteThread(int id) {
            using (ForumDbContext context = new ForumDbContext()) {
                Thread thread = context.Threads.Find(id);

                if (thread != null) {
                    context.Threads.Remove(thread);
                    context.SaveChanges();
                }

                return RedirectToAction("ShowBoard", new { id = thread.BoardId, _index = 0 });
            }
        }

        public ActionResult DeletePost(int id) {
            using (ForumDbContext context = new ForumDbContext()) {
                bool ThreadWasDeleted = false;

                Post post = context.Posts.Find(id);

                int threadId = post.ThreadId;

                if (post != null) {
                    context.Posts.Remove(post);




                    //Is it the last post? Then delete the thread too.
                if (context.Posts.Where(p => p.ThreadId == threadId).Count() == 1) {
                        context.Threads.Remove(context.Threads.Find(threadId));
                        ThreadWasDeleted = true;
                }
                    context.SaveChanges();
                }

                if(ThreadWasDeleted == true) {
                    return RedirectToAction("Index");
                }
                else {
                return RedirectToAction("ShowThread", new { id = post.ThreadId, _index = 0 });
                }

            }
        }
    }
}