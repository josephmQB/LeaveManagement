using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DomainModel
{
    public class ApplicationUserManager : UserManager<Employee>
    {
        public ApplicationUserManager(IUserStore<Employee> store) : base(store)
        {
        }
    }
}
