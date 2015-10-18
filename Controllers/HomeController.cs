using Barker.Data;
using Barker.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barker.Controllers
{
    public class HomeController : BaseAuthenticatedController
    {
        private BarkerData db = new BarkerData();

        public ActionResult Index()
        {
            var vm = new HomeVM();
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            if(self != null)
            {
                var returnBarks = new List<Bark>();
                var barks = db.Barks.Where(b => b.UserId.Equals(self.Id));

                var followersBarks = db.UserFollowings.Where(x => x.UserId == self.Id).Select(t => t.UserFollowed.Barks);
                returnBarks = followersBarks.SelectMany(c => c).ToList();
                returnBarks.AddRange(barks);

                vm.Barks = returnBarks.ToList();
                vm.Followers = db.UserFollowings.Where(x => x.FollowingId == self.Id).Count();
                vm.Following = db.UserFollowings.Where(x => x.UserId == self.Id).Count();
                vm.UserName = self.UserName;

                return View("Index", vm);
            }


            return RedirectToAction("Login", "Account");




            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}