using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comments.Models;

namespace Comments.Controllers
{
    public class ComsController : Controller
    {
        private temp1Entities db = new temp1Entities();

        // GET: Coms
        public ActionResult Index()
        {
            return View(db.Coms.ToList());
        }

        // GET: Coms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Com com = db.Coms.Find(id);
            if (com == null)
            {
                return HttpNotFound();
            }
            return View(com);
        }

        // GET: Coms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Password,Email,Age")] Com com)
        {
            if (ModelState.IsValid)
            {
                db.Coms.Add(com);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(com);
        }

        // GET: Coms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Com com = db.Coms.Find(id);
            if (com == null)
            {
                return HttpNotFound();
            }
            return View(com);
        }

        // POST: Coms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Password,Email,Age")] Com com)
        {
            if (ModelState.IsValid)
            {
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(com);
        }

        // GET: Coms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Com com = db.Coms.Find(id);
            if (com == null)
            {
                return HttpNotFound();
            }
            return View(com);
        }

        // POST: Coms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Com com = db.Coms.Find(id);
            db.Coms.Remove(com);
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
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Comments()
        {
            return View();
        }
        public ActionResult login(Com reg, string Email, string Password)
        {

            reg.Email = Email;
            reg.Password = Password;
            var Register = db.Coms.ToList();
            var user = Register.SingleOrDefault(usr => usr.Email == reg.Email && usr.Password == reg.Password);
            if (user != null)
            {
                Session["Email"] = user.Email.ToString();
                Session["Password"] = user.Password.ToString();
                return RedirectToAction("Comments", "Coms");
            }
            return View();
        }
    }
}
