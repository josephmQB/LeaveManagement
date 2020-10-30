using LeaveManagement.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LeaveManagement.Repository
{
    public interface IEmployeeRepository
    {
        bool InsertEmployee(Employee e);
        bool UpdateEmpolyeeDetails(Employee e);
        bool DeleteEmployee(string id);
        List<Employee> GetEmployees();
        Employee GetEmployeeById(string id);
        Employee GetEmployeeByEmail(string Email);
        Employee GetEmployeeByEmailAndPassword(string Email, string Password);
        void Login(IAuthenticationManager authenticationManager, Employee e);
        bool UpdatePassword(string Id, string CurrentPassword, string NewPassword);

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        LeaveManagementDbContext db;
        ApplicationUserStore userStore;
        ApplicationUserManager userManager;
        public EmployeeRepository()
        {
            db = new LeaveManagementDbContext();
            userStore = new ApplicationUserStore(db);
            userManager = new ApplicationUserManager(userStore);
        }
      
        public bool UpdatePassword(string Id, string CurrentPassword, string NewPassword)
        {
            IdentityResult result = userManager.ChangePassword(Id, CurrentPassword, NewPassword);
            return result.Succeeded;
        }
        public void Login(IAuthenticationManager authenticationManager,Employee e)
        {
            var userIdentity = userManager.CreateIdentity(e, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
        }
        public Employee GetEmployeeByEmail(string Email)
        {
            Employee e = userManager.FindByEmail(Email);
            return e;
        }

        public Employee GetEmployeeByEmailAndPassword(string Email, string Password)
        {
            Employee e = userManager.FindByEmail(Email);
            if (userManager.CheckPassword(e, Password))
                return e;
            else
                return null;
        }

        public bool DeleteEmployee(string id)
        {
            Employee e = userManager.FindById(id);
            userManager.RemoveFromRoles(e.Id, "Employee");
            IdentityResult result = userManager.Delete(e);
            return result.Succeeded;
        }

        public Employee GetEmployeeById(string id)
        {
            Employee e = userManager.FindById(id);
            return e;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = userManager.Users.Where(temp => temp.Address == "").ToList();
            return employees;
        }

        public bool InsertEmployee(Employee e)
        {
            IdentityResult result = userManager.Create(e);
            if (result.Succeeded)
            {
                userManager.AddToRole(e.Id, "Employee");
                return true;
            }
            return false;
        }

        public bool UpdateEmpolyeeDetails(Employee e)
        {
            IdentityResult result = userManager.Update(e);
            return result.Succeeded;
        }

    }
}
