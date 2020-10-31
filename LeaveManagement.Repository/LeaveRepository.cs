using LeaveManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Repository
{
    public interface ILeaveRepository
    {
        void InsertLeave(Leave l);
        Leave GetLeavesByID(int LeaveID);
        List<Leave> GetLeavesByEmpolyeeID(string EmpID);
        void UpdateLeaveStatus(Leave l);
        int GetLastestLeaveId();

    }
    public class LeaveRepository : ILeaveRepository
    {
        LeaveManagementDbContext db;
        public LeaveRepository()
        {
            db = new LeaveManagementDbContext();
        }
        public List<Leave> GetLeavesByEmpolyeeID(string EmpID)
        {
            List<Leave> leave = db.Leaves.Where(temp => temp.EmployeeID == EmpID).ToList();
            return leave;
        }

        public Leave GetLeavesByID(int LeaveID)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveID == LeaveID).FirstOrDefault();
            return leave;
        }

        public void InsertLeave(Leave l)
        {
            db.Leaves.Add(l);
            db.SaveChanges();
        }

        public void UpdateLeaveStatus(Leave l)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if (leave != null)
            {
                leave.LeaveStatus = l.LeaveStatus;
                db.SaveChanges();
            }
        }
        public int GetLastestLeaveId()
        {
            int leaveId = db.Leaves.Select(temp => temp.LeaveID).Max();
            return leaveId;
        }
    }
}
