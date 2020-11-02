using LeaveManagement.ViewModel.Employee;
using LeaveManagment.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService es;
        public HomeController(IEmployeeService es)
        {
            this.es = es;
        }
        // GET: Home
        public ActionResult SearchByRole(string Role)
        {
            List<EmployeeViewModel> evms = this.es.GetEmployeesByRoles(Role); 
            return View(evms);
        }
        public ActionResult SearchByName(string Id)
        {
            EmployeeViewModel evm = this.es.GetEmployeeByID(Id);
            return View(evm);
        }
    }
}