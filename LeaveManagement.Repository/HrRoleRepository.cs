using LeaveManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Repository
{
    public interface IHrRoleRepository
    {
        void InsertHR(HrRole hr);
        HrRole GetHRByID(int HrID);
        HrRole GetHRByEmployeeID(string EmpID);
        List<HrRole> GetHR();
    }
    public class HrRoleRepository : IHrRoleRepository
    {
        LeaveManagementDbContext db;
        public HrRoleRepository()
        {
            db = new LeaveManagementDbContext();
        }
        public List<HrRole> GetHR()
        {
            List<HrRole> hrs = db.HrRoles.Select(temp => temp).ToList();
            return hrs;
        }

        public HrRole GetHRByID(int HrID)
        {
            HrRole hr = db.HrRoles.Where(temp => temp.HrID == HrID).FirstOrDefault();
            return hr;
        }

        public HrRole GetHRByEmployeeID(string EmpID)
        {
            HrRole hr = db.HrRoles.Where(temp => temp.EmployeeID == EmpID).FirstOrDefault();
            return hr;
        }

        public void InsertHR(HrRole hr)
        {
            db.HrRoles.Add(hr);
            db.SaveChanges(); ;
        }
    }
}
