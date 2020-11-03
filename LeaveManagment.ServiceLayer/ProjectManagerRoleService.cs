using LeaveManagement.DomainModel;
using LeaveManagement.Repository;
using LeaveManagement.ViewModel.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagment.ServiceLayer
{
    public interface IProjectManagerRoleService
    {
        List<PmViewModel> GetProjectManages();
        PmViewModel GetProjectManagerByEmployeeID(string EmpID);
    }
    public class ProjectManagerRoleService : IProjectManagerRoleService
    {
        IProjectManagerRoleRepository pr;
        public ProjectManagerRoleService()
        {
            pr = new ProjectManagerRoleRepository();
        }

        public PmViewModel GetProjectManagerByEmployeeID(string EmpID)
        {
            ProjectMangerRole projectManger = pr.GetProjectMangerByEmployeeID(EmpID);
            PmViewModel pvm = null;
            if(projectManger != null)
            {
                pvm = new PmViewModel() { PmID = projectManger.PmID, PmName = projectManger.Employee.EmployeeName };
            }
            return pvm;
        }

        public List<PmViewModel> GetProjectManages()
        {
            List<ProjectMangerRole> projectMangers = pr.GetProjectMangers();
            List<PmViewModel> pvms = new List<PmViewModel>();
            if(projectMangers != null)
            {
                foreach (var item in projectMangers)
                {
                    pvms.Add(new PmViewModel() { PmID = item.PmID, PmName = item.Employee.EmployeeName });
                }
                return pvms;
            }
            return null;
        }
    }
}
