using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStepBlog.Models;

namespace WebStepBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments/CreateComment/5

        public ActionResult CreateComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Comments/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AddComment([Bind(Include = "Id,Name,Email,Body")] Comment comment, int postId)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(postId);
                Comment inputComment = new Comment();
                inputComment.Body = comment.Body;
                if (User.Identity.IsAuthenticated)
                {
                    string user = User.Identity.GetUserId();
                    ApplicationUser currentUser = db.Users.FirstOrDefault(u => u.Id == user);
                    inputComment.Name = currentUser.UserName;
                    inputComment.Email = currentUser.Email;
                    inputComment.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                }
                else
                {
                    inputComment.Name = comment.Name;
                    inputComment.Email = comment.Email;
                }
                post.Comments.Add(inputComment);
                db.SaveChanges();
                return RedirectToAction("CreateComment", new { id = postId });
            }
            return View("CreateComment", comment);
        }
    }
}