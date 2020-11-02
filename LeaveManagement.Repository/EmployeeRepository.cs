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
        List<Employee> GetEmployeesByRoles(string role);
        Employee GetEmployeeById(string id);
        Employee GetEmployeeByEmail(string Email);
        void Login(IAuthenticationManager authenticationManager, string Email, string Password);
        bool UpdatePassword(string Id, string CurrentPassword, string NewPassword);
        void UpdateImageUrl(string Id, string ImageUrl);
        bool UpdateEmpolyeeDetailsByHR(Employee e);

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        LeaveManagementDbContext db;
        ApplicationUserStore userStore;
        ApplicationUserManager userManager;
        IHrRoleRepository hr;
        IProjectManagerRoleRepository pr;
        public EmployeeRepository()
        {
            db = new LeaveManagementDbContext();
            userStore = new ApplicationUserStore(db);
            userManager = new ApplicationUserManager(userStore);
            hr = new HrRoleRepository();
            pr = new ProjectManagerRoleRepository();
        }

        public List<Employee> GetEmployeesByRoles(string role)
        {
            List<Employee> employees = userManager.Users.Where(temp => temp.EmployeeRoles == role).ToList();
            return employees;
        }

        public bool UpdatePassword(string Id, string CurrentPassword, string NewPassword)
        {
            IdentityResult result = userManager.ChangePassword(Id, CurrentPassword, NewPassword);
            return result.Succeeded;
        }
        public Employee GetEmployeeByEmail(string Email)
        {
            Employee e = userManager.FindByEmail(Email);
            return e;
        }

        public void Login(IAuthenticationManager authenticationManager, string Email, string Password)
        {
            Employee e = userManager.FindByEmail(Email);
            if (userManager.CheckPassword(e, Password))
            {
                var userIdentity = userManager.CreateIdentity(e,DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
            }
        }

        public bool DeleteEmployee(string id)
        {
            Employee e = userManager.FindById(id);
            userManager.RemoveFromRoles(e.Id, e.EmployeeRoles);
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
            List<Employee> employees = userManager.Users.Select(temp => temp).ToList();
            return employees;
        }

        public bool InsertEmployee(Employee e)
        {
            IdentityResult result = userManager.Create(e);
            if (result.Succeeded)
            {
                if(e.EmployeeRoles == "HR")
                {
                    HrRole hR = new HrRole();
                    hR.EmployeeID = e.Id ;
                    hr.InsertHR(hR);
                }
                else if(e.EmployeeRoles == "PM")
                {
                    ProjectMangerRole projectManger = new ProjectMangerRole();
                    projectManger.EmployeeID = e.Id;
                    pr.InsertPM(projectManger);
                }
                userManager.AddToRole(e.Id, e.EmployeeRoles);
                return true;
            }
            return false;
        }

        public bool UpdateEmpolyeeDetails(Employee e)
        {
            Employee employee = userManager.FindById(e.Id);
            employee.EmployeeName = e.EmployeeName;
            employee.DateOfBirth = e.DateOfBirth;
            employee.Phone = e.Phone;
            employee.Address = e.Address;
            IdentityResult result = userManager.Update(employee);
            return result.Succeeded;
        }
        public bool UpdateEmpolyeeDetailsByHR(Employee e)
        {
            Employee employee = userManager.FindById(e.Id);
            employee.Email = e.Email;
            employee.ImageUrl = e.ImageUrl;
            employee.EmployeeRoles = e.EmployeeRoles;
            employee.EmployeeName = e.EmployeeName;
            employee.DateOfBirth = e.DateOfBirth;
            employee.Phone = e.Phone;
            employee.Address = e.Address;
            IdentityResult result = userManager.Update(employee);
            return result.Succeeded;
        }
        public void UpdateImageUrl(string Id, string ImageUrl)
        {
            var e = userManager.FindById(Id);
            e.ImageUrl = ImageUrl;
            db.SaveChanges();
        }

    }
}
