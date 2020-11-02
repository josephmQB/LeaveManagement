using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagment.ServiceLayer;
using Microsoft.AspNet.Identity;

namespace LeaveManagement.Filters
{
    public class SpecialPermissionAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IHrRoleService hr = new HrRoleService();
            var user = hr.GetHRByEmployeeID(filterContext.HttpContext.User.Identity.GetUserId());
            var SpecialPermission = user.IsSpecialPermission;
            if (filterContext.HttpContext.User.IsInRole("HR") == false && SpecialPermission == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}