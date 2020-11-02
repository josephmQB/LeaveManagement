using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagement.Filters
{
    public class PmAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("PM") == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}