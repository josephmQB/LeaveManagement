using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DomainModel
{
   public  class LeaveManagementDbContext : IdentityDbContext<Employee>
    {
        public LeaveManagementDbContext():base("LeaveManagementDatabase")
        {

        }
        public DbSet<Leave> Leaves { get; set; }
    }
}
