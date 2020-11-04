using AutoMapper;
using LeaveManagement.DomainModel;
using LeaveManagement.Repository;
using LeaveManagement.ViewModel.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagment.ServiceLayer
{
    public interface ILeaveService
    {
        int InsertLeave(RequestLeaveViewModel rlvm);
        List<LeaveViewModel> GetLeaveByEmployeeID(string EmpID);
        List<LeaveViewModel> GetLeaves();
        List<LeaveViewModel> GetLeaveByPmID(int PmID);
        LeaveViewModel GetLeaveByID(int LeaveID);
        bool UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm);
        bool SendEmail(string toEmail, string subject, string emailBody);
    }
    public class LeaveService : ILeaveService
    {
        ILeaveRepository lr;
        public LeaveService()
        {
            lr = new LeaveRepository();
        }
        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senerEmail = "Your email";
                string senderPassword = "Your password";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senerEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senerEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public List<LeaveViewModel> GetLeaveByEmployeeID(string EmpID)
        {
            List<Leave> leaves = lr.GetLeavesByEmpolyeeID(EmpID);
            List<LeaveViewModel> lvms = new List<LeaveViewModel>();
            if (leaves != null)
            {
                foreach (var item in leaves)
                    lvms.Add(new LeaveViewModel() { LeaveID = item.LeaveID, NoOfDays = item.NoOfDays, StartDate = item.StartDate, EndDate = item.EndDate, LeaveDescription = item.LeaveDescription, LeaveStatus = item.LeaveStatus, ProjectManagerName = item.ProjectMangerRole.Employee.EmployeeName, Remarks = item.Remarks, EmployeeName = item.Employee.EmployeeName, EmployeeID = item.EmployeeID });
                return lvms;
            }
            return null;
        }
        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> leaves = lr.GetLeaves();
            List<LeaveViewModel> lvms = new List<LeaveViewModel>();
            if (leaves != null)
            {
                foreach (var item in leaves)
                    lvms.Add(new LeaveViewModel() { LeaveID = item.LeaveID, NoOfDays = item.NoOfDays, StartDate = item.StartDate, EndDate = item.EndDate, LeaveDescription = item.LeaveDescription, LeaveStatus = item.LeaveStatus, ProjectManagerName = item.ProjectMangerRole.Employee.EmployeeName, Remarks = item.Remarks, EmployeeName = item.Employee.EmployeeName, EmployeeID = item.EmployeeID });
                return lvms;
            }
            return null;
        }
        public List<LeaveViewModel> GetLeaveByPmID(int PmID)
        {
            List<Leave> leaves = lr.GetLeavesByPmID(PmID);
            List<LeaveViewModel> lvms = new List<LeaveViewModel>();
            if (leaves != null)
            {
                foreach (var item in leaves)
                    lvms.Add(new LeaveViewModel() { LeaveID = item.LeaveID, NoOfDays = item.NoOfDays, StartDate = item.StartDate, EndDate = item.EndDate, LeaveDescription = item.LeaveDescription, LeaveStatus = item.LeaveStatus, ProjectManagerName = item.ProjectMangerRole.Employee.EmployeeName, Remarks = item.Remarks, EmployeeName = item.Employee.EmployeeName , EmployeeID = item.EmployeeID });
                return lvms;
            }
            return null;
        }
        public LeaveViewModel GetLeaveByID(int LeaveID)
        {
            Leave l = lr.GetLeavesByID(LeaveID);
            LeaveViewModel lvm = null;
            if (l != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                lvm = mapper.Map<Leave, LeaveViewModel>(l);
                lvm.EmployeeName = l.Employee.EmployeeName;
                lvm.ProjectManagerName = l.ProjectMangerRole.Employee.EmployeeName;
            }
           return lvm;
        }

        public int InsertLeave(RequestLeaveViewModel rlvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RequestLeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<RequestLeaveViewModel, Leave>(rlvm);
            l.LeaveStatus = "Pending";
            lr.InsertLeave(l);
            int leaveId = lr.GetLastestLeaveId();
            return leaveId;
        }

        public bool UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateLeaveStatusViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<UpdateLeaveStatusViewModel, Leave>(ulsvm);
            var result = lr.UpdateLeaveStatus(l);
            return result;
        }
    }
}
