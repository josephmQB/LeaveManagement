using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DomainModel
{
    public class ApplicationUserStore : UserStore<Employee>
    {
        public ApplicationUserStore(LeaveManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
