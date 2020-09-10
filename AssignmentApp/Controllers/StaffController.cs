using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssignmentApp.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        AssignmentDBEntities1 db = new AssignmentDBEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tutor()
        {
            var res = db.Tutors.ToList();
            return View(res);
        }

        public ActionResult TutorEdit(int id)
        {
            var res = db.Tutors.Find(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult TutorEdit([Bind(Include = "ID, TutorName, TutorType, WorkingPlace, TutorPhone, TutorEmail")]
                                Tutor tutor, string Password)
        {
            db.Entry(tutor).State = EntityState.Modified;
            db.SaveChanges();

            var userstore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userstore);

            AuthenController.UpdatePassword(tutor.TutorEmail, Password);

            return RedirectToAction("Tutor");
        }

        public ActionResult TutorDelete(int id)
        {
            var res = db.Tutors.Find(id);
            db.Tutors.Remove(res);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateTutorAccount([Bind(Include = "ID, TutorName, TutorType, WorkingPlace, TutorPhone, TutorEmail")]
                                Tutor tutor, string Role)
        {
            db.Tutors.Add(tutor);
            db.SaveChanges();

            AuthenController.CreateAccount(tutor.TutorEmail, "tutor123123", Role);

            return RedirectToAction("Tutor");
        }
    }
}