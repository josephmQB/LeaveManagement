using LeaveManagement.DomainModel;
using LeaveManagement.Repository;
using LeaveManagement.ViewModel.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagment.ServiceLayer
{
    public interface IHrRoleService
    {
        HRVeiwModel GetHRByEmployeeID(string EmpID);
    }
    public class HrRoleService : IHrRoleService
    {
        IHrRoleRepository hr;
        public HrRoleService()
        {
            hr = new HrRoleRepository();
        }
        public HRVeiwModel GetHRByEmployeeID(string EmpID)
        {
            HrRole hR = hr.GetHRByEmployeeID(EmpID);
            HRVeiwModel hvm = null;
            if (hR != null)
                hvm = new HRVeiwModel() { HrID = hR.HrID, IsSpecialPermission = hR.IsSpecialPermission };
            return hvm;
        }
    }
}
