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
    public class FollowersController : BaseAuthenticatedController
    {
        private BarkerData db = new BarkerData();

        // GET: Followers
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            var userFollowers = db.UserFollowings.Where(x => x.FollowingId == self.Id);
            return View(userFollowers.ToList());
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
            return RedirectToAction("Index","Users");
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
            return RedirectToAction("Index","Users");
        }

    }
}
