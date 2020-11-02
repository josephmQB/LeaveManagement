using System;
using System.Threading.Tasks;
using LeaveManagement.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(LeaveManagement.Startup))]

namespace LeaveManagement
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRolesAndUsers();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new LeaveManagementDbContext()));
            var appDbContext = new LeaveManagementDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);


            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("PM"))
            {
                var role = new IdentityRole();
                role.Name = "PM";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("HR"))
            {
                var role = new IdentityRole();
                role.Name = "HR";
                roleManager.Create(role);
            }
        }
    }
}
