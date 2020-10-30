
using LeaveManagement.ViewModel.Employee;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LeaveManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        IEmployeeService es;

        public AccountController()
        {
                
        }
        public AccountController(IEmployeeService es)
        {
            this.es = es;
        }
        public ActionResult Index()
        {
            return View();
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
                this.es.InsertEmployee(rvm);
                return RedirectToAction("Profile", "Account");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        [ActionName("Profile")]
        public ActionResult MyProfile()
        {
            var Id = User.Identity.GetUserId();
            EmployeeViewModel evm = this.es.GetEmployeeByID(Id);
            return View(evm);
        }
        [ActionName("Update")]
        public ActionResult UpdateEmplyee(string id )
        {
            EmployeeViewModel evm = this.es.GetEmployeeByID(id);
            UpdateEmployeeViewModel uevm = new UpdateEmployeeViewModel() { EmployeeId = evm.Id, EmployeeName = evm.EmployeeName, Address = evm.Address, DateOfBirth = evm.DateOfBirth, Phone = evm.Phone, Email = evm.Email };
            return View(uevm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel uevm)
        {
            if (ModelState.IsValid)
            {
                this.es.UpdateEmployeeDetails(uevm);
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uevm);
            }
        }
        [HttpPost]
        [ActionName("Upload")]
        public ActionResult UpdateImage(UpdateImageUrlViewModel uivm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    uivm.ImageUrl = base64String;
                }
                this.es.UpdateImageUrl(uivm);
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uivm);
            }

        }
    }
}