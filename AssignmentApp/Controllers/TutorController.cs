using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentApp.Models;

namespace AssignmentApp.Controllers
{
    public class TutorController : Controller
    {
        AssignmentDBEntities1 db = new AssignmentDBEntities1();
        // GET: Tutor
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Profile()
        {
            //get UserName form login
            var userName = User.Identity.Name;
            //var student = db.Student.Where(s => s.StudentID.Equals(userName));
            var res = (from s in db.Tutors where s.TutorEmail.Equals(userName) select s).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Profile([Bind(Include = "ID, TutorName, TutorType, WorkingPlace, TutorPhone, TutorEmail")] Tutor tutor)
        {
            db.Entry(tutor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MyCourse()
        {
            var userName = User.Identity.Name;
            var res = from e in db.Courses where e.Topic.Tutor.TutorEmail.Equals(userName) select e;
            return View(res.ToList());
        }
    }
}