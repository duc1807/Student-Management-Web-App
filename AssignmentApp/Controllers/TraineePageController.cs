using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentApp.Models;
namespace AssignmentApp.Controllers
{
    public class TraineePageController : Controller
    {
        AssignmentDBEntities1 db = new AssignmentDBEntities1();
        // GET: TraineePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            //get UserName form login
            var userName = User.Identity.Name;
            //var student = db.Student.Where(s => s.StudentID.Equals(userName));
            var res = (from s in db.Trainees where s.TraineeID.Equals(userName) select s).FirstOrDefault();
            return View(res);
        }

        public ActionResult MyCourse()
        {
            //var userName = User.Identity.Name;
            //var res = from e in db.Enrollments where e.Trainee.TraineeName.Equals(userName) select e;
            //return View(res.ToList());
            var userName = User.Identity.Name;
            var enrollments = from e in db.Enrollments
                              where e.TraineeID.Equals(userName)
                              select e;
            return View(enrollments.ToList());
        }

        public ActionResult Detail(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }
    }
}