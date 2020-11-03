using LeaveManagment.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LeaveManagement.ApiControllers
{
        // GET: Account
        public class AccountController : ApiController
        {
            IEmployeeService es;
            public AccountController(IEmployeeService es)
            {
                this.es = es;
            }
            public AccountController()
            {

            }
            public string Get(string Email)
            {
                if (this.es.GetEmployeeByEmail(Email) != null)
                {
                    return "Found";
                }
                else
                {
                    return "Not Found";
                }
            }
        }
}