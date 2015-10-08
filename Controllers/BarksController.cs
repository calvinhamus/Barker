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
    public class BarksController : Controller
    {
        private BarkerData db = new BarkerData();

        // GET: Barks comment
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            var returnBarks = new List<Bark>();
            var barks = db.Barks.Where(b => b.UserId.Equals(self.Id));

            var followersBarks = db.UserFollowings.Where(x => x.UserId == self.Id).Select(t=>t.UserFollowed.Barks);
            returnBarks = followersBarks.SelectMany(c => c).ToList();
            returnBarks.AddRange(barks);
       
            return View(returnBarks);
        }

        // GET: Barks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bark bark = db.Barks.Find(id);
            if (bark == null)
            {
                return HttpNotFound();
            }
            return View(bark);
        }

        // GET: Barks/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Barks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Text,DateTimePosted")] Bark bark)
        {
            var user = User.Identity.GetUserName();
            AspNetUser self = db.AspNetUsers.Where(x => x.UserName.Equals(user)).FirstOrDefault();
           // bark.AspNetUser = self;
            bark.UserId = self.Id;
            bark.DateTimePosted = DateTime.Now;
          //  if (ModelState.IsValid)
         //   {
                db.Barks.Add(bark);
                db.SaveChanges();
                return RedirectToAction("Index");
          //  }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bark.UserId);
            return View(bark);
        }

        // GET: Barks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bark bark = db.Barks.Find(id);
            if (bark == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bark.UserId);
            return View(bark);
        }

        // POST: Barks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Text,DateTimePosted")] Bark bark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bark.UserId);
            return View(bark);
        }

        // GET: Barks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bark bark = db.Barks.Find(id);
            if (bark == null)
            {
                return HttpNotFound();
            }
            return View(bark);
        }

        // POST: Barks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bark bark = db.Barks.Find(id);
            db.Barks.Remove(bark);
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
