using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModel.Employee
{
    public class UpdateEmployeeViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
