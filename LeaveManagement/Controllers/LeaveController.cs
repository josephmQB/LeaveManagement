using LeaveManagement.ViewModel.Leave;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Controllers
{
    public class LeaveController : Controller
    {
        ILeaveService ls;
        public LeaveController(ILeaveService ls)
        {
            this.ls = ls;
        }
        // GET: Leave
        [ActionName("Request")]
        public ActionResult LeaveRequest()
        {
            ViewBag.empId = User.Identity.GetUserId();
            return View();
        }
        [HttpPost]
        [ActionName("Request")]
        public ActionResult LeaveRequest(RequestLeaveViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                this.ls.InsertLeave(rlvm);
                return RedirectToAction("Profile", "Account");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}