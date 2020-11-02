using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModel.Employee
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmployeeRoles { get; set; }
        public string ImageUrl { get; set; }
    }
}
