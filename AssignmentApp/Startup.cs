using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(AssignmentApp.Startup))]

namespace AssignmentApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Authen/Login")

            });
            CreateUserRoles();
        }

        private void CreateUserRoles()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);

                var user = new IdentityUser() { UserName = "admin" };
                var result = manager.Create(user, "admin123");

                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Staff"))
            {
                var role = new IdentityRole("Staff");
                roleManager.Create(role);
            }


            if (!roleManager.RoleExists("Tutor"))
            {
                var role = new IdentityRole("Tutor");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole("Student");
                roleManager.Create(role);
            }
        }
    }
}
