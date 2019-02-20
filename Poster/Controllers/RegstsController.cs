using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Poster;

namespace Poster.Controllers
{
    public class RegstsController : Controller
    {
        private ChatterEntities2 db = new ChatterEntities2();

        // GET: Regsts
        public ActionResult Index()
        {
            return View(db.Regsts.ToList());
        }

        // GET: Regsts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regst regst = db.Regsts.Find(id);
            if (regst == null)
            {
                return HttpNotFound();
            }
            return View(regst);
        }

        // GET: Regsts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regsts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Password")] Regst regst,string name,string email,string pass)
        {
            if (ModelState.IsValid)
            {
                regst.Name = name;
                regst.Email = email;
                regst.Password = pass;

                db.Regsts.Add(regst);
                if (regst != null)
                {
                    using (MailMessage mm = new MailMessage("sahayajency1995@gmail.com", regst.Email))
                    {
                        mm.Subject = "AccountActivation";
                        mm.Body = "Hello! " + regst.Name + " " + "you are successfully registered";
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential network = new NetworkCredential("sahayajency1995@gmail.com", "sahayaraj");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = network;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regst);
        }

        // GET: Regsts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regst regst = db.Regsts.Find(id);
            if (regst == null)
            {
                return HttpNotFound();
            }
            return View(regst);
        }

        // POST: Regsts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password")] Regst regst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regst);
        }

        // GET: Regsts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regst regst = db.Regsts.Find(id);
            if (regst == null)
            {
                return HttpNotFound();
            }
            return View(regst);
        }

        // POST: Regsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Regst regst = db.Regsts.Find(id);
            db.Regsts.Remove(regst);
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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Regst user, string email, string pass)
        {
            user.Email = email;
            user.Password = pass;
            var obj = db.Regsts.ToList();
            var usr = obj.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (usr != null)
            {
                Session["Id"] = usr.Id.ToString();
                Session["Email"] = usr.Email.ToString();
                return RedirectToAction("UserPage", "Home");
            }
            else if (user.Email == "admin@gmail.com" && user.Password == "admin123")
            {
                return View("HomePage", "Home");
            }

            return View();
        }
    }
}
