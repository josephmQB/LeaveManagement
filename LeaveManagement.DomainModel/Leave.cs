using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.DomainModel
{
    public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfDays { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveDescription { get; set; }
        public string EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
