using LeaveManagement.DomainModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Repository
{
    public interface IProjectManagerRoleRepository
    {
        void InsertPM(ProjectMangerRole pm);
        ProjectMangerRole GetProjectMangerByID(int PmID);
        ProjectMangerRole GetProjectMangerByEmployeeID(string EmpID);
        List<ProjectMangerRole> GetProjectMangers();
    }
    public class ProjectManagerRoleRepository : IProjectManagerRoleRepository
    {
        LeaveManagementDbContext db;
        public ProjectManagerRoleRepository()
        {
            db = new LeaveManagementDbContext();
        }
        public ProjectMangerRole GetProjectMangerByEmployeeID(string EmpID)
        {
            ProjectMangerRole projectManger = db.ProjectMangerRoles.Where(temp => temp.EmployeeID == EmpID).FirstOrDefault();
            return projectManger;
        }

        public ProjectMangerRole GetProjectMangerByID(int PmID)
        {
            ProjectMangerRole projectManger = db.ProjectMangerRoles.Where(temp => temp.PmID == PmID).FirstOrDefault();
            return projectManger;
        }

        public List<ProjectMangerRole> GetProjectMangers()
        {
            List<ProjectMangerRole> projectMangers = db.ProjectMangerRoles.Select(temp => temp).ToList();
            return projectMangers;
        }

        public void InsertPM(ProjectMangerRole pm)
        {
            db.ProjectMangerRoles.Add(pm);
            db.SaveChanges();
        }
    }
}
