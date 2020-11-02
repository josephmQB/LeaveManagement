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
            public LeaveController(ILeaveService ls)
            {
                this.ls = ls;
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
                    this.ls.UpdateLeaveStatus(ulsvm);
                    return RedirectToAction("Status", "Leave", new { area = "PM", LeaveID = ulsvm.LeaveID });

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