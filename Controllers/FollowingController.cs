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
    public class FollowingController : BaseAuthenticatedController
    {
        private BarkerData db = new BarkerData();

        // GET: Following
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            var userFollowings = db.UserFollowings.Where(x => x.UserId == self.Id);
           
            return View(userFollowings.ToList());
        }
        public ActionResult ViewUser(int? id)
        {
            return RedirectToAction("Index", "Barks");
        }

       
    }
}
