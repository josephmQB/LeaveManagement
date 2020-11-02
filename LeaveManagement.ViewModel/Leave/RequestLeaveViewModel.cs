using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModel.Leave
{
    public class RequestLeaveViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int NoOfDays { get; set; }
        [Required]
        public string LeaveDescription { get; set; }
        [Required]
        public string EmployeeID { get; set; }
        [Required]
        public int ProjectManagerID { get; set; }
    }
}
