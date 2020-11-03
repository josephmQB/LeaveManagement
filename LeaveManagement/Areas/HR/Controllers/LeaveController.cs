using LeaveManagement.Filters;
using LeaveManagement.ViewModel.Leave;
using LeaveManagment.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Areas.HR.Controllers
{
    [SpecialPermissionAuthorization]
    public class LeaveController : Controller
    {
        // GET: HR/Leave
            ILeaveService ls;
        IEmployeeService es;
            public LeaveController(ILeaveService ls,IEmployeeService es)
            {
                this.ls = ls;
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
                    if (res)
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
                List<LeaveViewModel> lvms = this.ls.GetLeaves();
                return View(lvms);
            }
        }
    }