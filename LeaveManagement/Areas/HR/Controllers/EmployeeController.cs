using LeaveManagement.Filters;
using LeaveManagement.ViewModel.Employee;
using LeaveManagment.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Areas.HR.Controllers
{
    [HrAuthorization]
    public class EmployeeController : Controller
    {
        // GET: HR/Employee
        IEmployeeService es;

        public EmployeeController()
        {

        }
        public EmployeeController(IEmployeeService es)
        {
            this.es = es;
        }
        [ActionName("Register")]
        public ActionResult RegisterEmployee()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterEmployee(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var result = this.es.InsertEmployee(rvm);
                if (result)
                {
                    var evms = this.es.GetEmployees();
                    Session["Employee"] = evms;
                    ViewBag.Registered = true;
                }
                else
                    ViewBag.NotRegistered = true;
                return RedirectToAction("Show");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(string Id)
        {
            EmployeeViewModel evm = this.es.GetEmployeeByID(Id);
            return View(evm);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(EmployeeViewModel evm)
        {
            var result = this.es.DeleteEmployee(evm.Id);
            if (result)
            {
                ViewBag.Deleted = true;
                var evms = this.es.GetEmployees();
                Session["Employee"] = evms;
            }
            else
                ViewBag.NotDeleted = true;
            return RedirectToAction("Show");
        }
        [ActionName("Update")]
        public ActionResult UpdateEmployee(string id)
        {
            EmployeeViewModel evm = this.es.GetEmployeeByID(id);
            UpdateEmployeeByHRViewModel uevm = new UpdateEmployeeByHRViewModel() { Id = evm.Id, EmployeeName = evm.EmployeeName, Address = evm.Address, DateOfBirth = evm.DateOfBirth, Phone = evm.Phone, Email = evm.Email , EmployeeRoles = evm.EmployeeRoles , ImageUrl = evm.ImageUrl};
            return View(uevm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateEmployee(UpdateEmployeeByHRViewModel uevm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    uevm.ImageUrl = base64String;
                }
                var result = this.es.UpdateEmployeeDetailsByHR(uevm);
                if (result)
                    ViewBag.Updated = true;
                else
                    ViewBag.NotUpdated = true;
                return RedirectToAction("Show");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uevm);
            }
        }
        [ActionName("Show")]
        public ActionResult ShowEmployees()
        {
            List<EmployeeViewModel> evms = this.es.GetEmployees();
            return View(evms);
        }

    }
}