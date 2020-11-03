using LeaveManagement.Filters;
using LeaveManagement.ViewModel.Leave;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Areas.PM.Controllers
{
    [PmAuthorization]
    public class LeaveController : Controller
    {
        ILeaveService ls;
        IProjectManagerRoleService ps;
        IEmployeeService es;
        public LeaveController(ILeaveService ls, IProjectManagerRoleService ps,IEmployeeService es)
        {
            this.ls = ls;
            this.ps = ps;
            this.es = es;
        }
        // GET: PM/Leave
        [ActionName("Status")]
        public ActionResult LeaveStatus(int LeaveId)
        {
            LeaveViewModel lvm = this.ls.GetLeaveByID(LeaveId);
            return View(lvm);
        }
        [HttpPost]
        [ActionName("Status")]
        public ActionResult LeaveStatus(UpdateLeaveStatusViewModel ulsvm)
        {
            if (ModelState.IsValid)
            {
                var res = this.ls.UpdateLeaveStatus(ulsvm);
                if(res)
                {
                    var lvm = this.ls.GetLeaveByID(ulsvm.LeaveID);
                    var evm = this.es.GetEmployeeByID(lvm.EmployeeID);
                    string subject = "Leave" + lvm.LeaveStatus;
                    string body = "<p>Hi " + lvm.EmployeeName + ", </p><br/><p>Your leave has been " + lvm.LeaveStatus + " by " + Session["CurrentUserName"] + ".</p>";
                    var result = this.ls.SendEmail(evm.Email, subject, body);
                    return RedirectToAction("Status", "Leave", new { area = "PM", LeaveID = ulsvm.LeaveID });
                }
                else
                {
                    return RedirectToAction("Status", "Leave", new { area = "PM", LeaveID = ulsvm.LeaveID });
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        [ActionName("Show")]
        public ActionResult ShowLeaveRequest()
        {
            var Pm = this.ps.GetProjectManagerByEmployeeID(User.Identity.GetUserId());
            List<LeaveViewModel> lvms = this.ls.GetLeaveByPmID(Pm.PmID);
            return View(lvms);
        }
    }
}