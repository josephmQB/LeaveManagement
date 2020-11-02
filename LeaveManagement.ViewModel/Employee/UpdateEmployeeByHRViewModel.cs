using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModel.Employee
{
    public class UpdateEmployeeByHRViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string EmployeeRoles { get; set; }
    }
}
