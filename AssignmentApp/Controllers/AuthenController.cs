using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AssignmentApp.Controllers
{
    public class AuthenController : Controller
    {
        // GET: Authen
        public ActionResult Register()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc)
        {
            // Default UserStore contructor uses the default connection string name: Default connection

            return View(acc);
        }


        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (User.IsInRole("Staff"))
            {
                return RedirectToAction("Index", "Staff");
            }
            else if (User.IsInRole("Tutor"))
            {
                return RedirectToAction("Index", "Tutor");
            }
            else
            {
                return RedirectToAction("Index", "TraineePage");
            }

        }

        public static void CreateAdminAccount()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = "ducadmin" };
            var result = manager.Create(user, "admin123");
            Console.WriteLine(result.ToString());
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, "Admin");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public static void CreateAccount(string Username, string Password, string Role)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = Username };
            var result = manager.Create(user, Password);
            Console.WriteLine(result.ToString());
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, Role);
            }
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account acc)
        {
            var userstore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userstore);

            var user = manager.Find(acc.Username, acc.Password);

            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties { }, userIdentity);
               // manager.ChangePassword(user.Id, )

                //if (manager.IsInRole(user.Id, "Staff"))
                //{
                //    return RedirectToAction("Staff", "Admin");
                //}
                //if (manager.IsInRole(user.Id, "Tutor"))
                //{
                //    return RedirectToAction("Tutor", "Admin");
                //}
                return RedirectToAction("Index", "Authen");
            }
            return RedirectToAction("Login", "Authen");
        }


        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return RedirectToAction("Login");
        }

        public static void UpdatePassword(string userName,string newpassword) {

            var userstore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userstore);

            var user = manager.FindByName(userName);
            //manager.ResetPassword(user.Id, null,newpassword);
            manager.RemovePassword(user.Id);
            manager.AddPassword(user.Id, newpassword);
           // manager.ChangePassword(user.Id, newpassword);

        }
    }
}