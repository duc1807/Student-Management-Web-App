using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentApp.Controllers;
using AssignmentApp.Models;

namespace AssignmentApp.Controllers
{
    [Authorize(Roles ="Admin, Staff")]
    public class AdminController : Controller
    {
        AssignmentDBEntities1 db = new AssignmentDBEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Staff()
        {
            var res = db.Staffs.ToList();
            return View(res);
        }

        [HttpPost]
        public ActionResult CreateAccount([Bind(Include = "ID, StaffID, StaffName, StaffEmail, StaffPhone")] Staff staff, string Role)
        {
                db.Staffs.Add(staff);
                db.SaveChanges();

                AuthenController.CreateAccount(staff.StaffID, "staff123", Role);

                return RedirectToAction("Staff");
        }

        public ActionResult Delete(int id)
        {
            var res = db.Staffs.Find(id);
            db.Staffs.Remove(res);
            db.SaveChanges();

            return RedirectToAction("Staff");
        }

        public ActionResult Edit(int id)
        {
            var res = db.Staffs.Find(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID, StaffID, StaffName, StaffPhone, StaffEmail")] Staff staff)
        {
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Staff");
        }

        public ActionResult Tutor()
        {
            var res = db.Tutors.ToList();
            return View("Tutor", res);
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

        public ActionResult TDelete(int id)
        {
            var res = db.Tutors.Find(id);
            db.Tutors.Remove(res);
            db.SaveChanges();

            return RedirectToAction("Tutor");
        }

        public ActionResult TEdit(int id)
        {
            var res = db.Tutors.Find(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult TEdit([Bind(Include = "ID, TutorName, TutorType, WorkingPlace, TutorPhone, TutorEmail")]
                                Tutor tutor)
        {
            db.Entry(tutor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Tutor");
        }

        public ActionResult ViewStaff()
        {
            var res = "cfc3143c-bfcb-432a-be00-bff88a7fb7b1";

            var res2 = from e in db.AspNetUserRoles where e.RoleId.Equals(res) select e.UserId;

            var res3 =  from b in db.AspNetUsers where b.Id.Equals(res2) select b;
            return View(res3.ToList());
        }
        //cfc3143c-bfcb-432a-be00-bff88a7fb7b1
    }
}