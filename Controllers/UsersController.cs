using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Barker.Data;
using Microsoft.AspNet.Identity;

namespace Barker.Controllers
{
    public class UsersController : BaseAuthenticatedController
    {
        private BarkerData db = new BarkerData();

        //GET: Users
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            var following = db.UserFollowings.Where(y => y.UserId.Equals(self.Id)).Select(x => x.UserFollowed).ToList();
            following.ForEach(f => f.FollowingText = "Following");
            var returnUsers = new List<AspNetUser>();
            returnUsers.AddRange(following);
            var diffs = db.AspNetUsers.ToList().Except(following).ToList();
            diffs.ForEach(f => f.FollowingText = "Follow");
            returnUsers.AddRange(diffs);
            return View(returnUsers);
        }

       // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUser);
        }

        //GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

       // POST: Users/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser AspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(AspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AspNetUser);
        }

       // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUser);
        }
       // TODO update to follow
        public ActionResult Follow(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = User.Identity.GetUserName();
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);

            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            var following = new UserFollowing();
            following.UserId = self.Id;
            following.FollowingId = AspNetUser.Id;
            //following.Self = self;
           // following.UserFollowed = AspNetUser;
            db.Entry(following).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Unfollow(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = User.Identity.GetUserName();
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);

            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            var remove = db.UserFollowings.Where(x => x.UserId.Equals(self.Id) && x.FollowingId.Equals(AspNetUser.Id)).FirstOrDefault();
            db.UserFollowings.Remove(remove);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
      //  POST: Users/Edit/5
      //   To protect from overposting attacks, please enable the specific properties you want to bind to, for 
       //  more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser AspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AspNetUser);
        }

       // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUser);
        }

        //POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(AspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
