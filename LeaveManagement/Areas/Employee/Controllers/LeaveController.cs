using LeaveManagement.Filters;
using LeaveManagement.ViewModel.Leave;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Areas.Employee.Controllers
{
    [EmployeeAuthorization]
    public class LeaveController : Controller
    {
        // GET: Employee/Leave
        ILeaveService ls;
        IProjectManagerRoleService ps;
        public LeaveController(ILeaveService ls, IProjectManagerRoleService ps)
        {
            this.ls = ls;
            this.ps = ps;
        }
        // GET: Leave
        [ActionName("Request")]
        public ActionResult LeaveRequest()
        {
            ViewBag.empId = User.Identity.GetUserId();
            ViewBag.projectManagers = this.ps.GetProjectManages();
            return View();
        }
        [HttpPost]
        [ActionName("Request")]
        public ActionResult LeaveRequest(RequestLeaveViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                this.ls.InsertLeave(rlvm);
                return RedirectToAction("Show");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        [ActionName("Show")]
        public ActionResult ShowLeaveRequests()
        {
           var empId = User.Identity.GetUserId();
            List<LeaveViewModel> lvms = this.ls.GetLeaveByEmployeeID(empId);
            return View(lvms);
        }
    }
}