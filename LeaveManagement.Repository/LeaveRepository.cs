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
        List<Leave> GetLeaves();
        List<Leave> GetLeavesByPmID(int PmID);
        bool UpdateLeaveStatus(Leave l);
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
        public List<Leave> GetLeavesByPmID(int PmID)
        {
            List<Leave> leave = db.Leaves.Where(temp => temp.ProjectManagerID == PmID).ToList();
            return leave;
        }
        public List<Leave> GetLeaves()
        {
            List<Leave> leave = db.Leaves.Select(temp => temp).ToList();
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

        public bool UpdateLeaveStatus(Leave l)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if (leave != null)
            {
                leave.LeaveStatus = l.LeaveStatus;
                leave.Remarks = l.Remarks;
                var entries = db.SaveChanges();
                if (entries != 0)
                    return true;
            }
            return false;
        }
        public int GetLastestLeaveId()
        {
            int leaveId = db.Leaves.Select(temp => temp.LeaveID).Max();
            return leaveId;
        }
    }
}
