using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModel.Employee
{
    public class UpdatePasswordViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("Password")]
        public string NewConfirmPassword { get; set; }
    }
}
