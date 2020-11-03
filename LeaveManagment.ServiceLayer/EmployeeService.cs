using AutoMapper;
using LeaveManagement.DomainModel;
using LeaveManagement.Repository;
using LeaveManagement.ViewModel.Employee;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace LeaveManagment.ServiceLayer
{
    public interface IEmployeeService
    {
        bool InsertEmployee(RegisterViewModel rvm);
        bool UpdateEmployeeDetails(UpdateEmployeeViewModel uevm);
        bool DeleteEmployee(String ID);
        List<EmployeeViewModel> GetEmployees();
        List<EmployeeViewModel> GetEmployeesByRoles(string role);
        EmployeeViewModel GetEmployeeByEmail(string Email);
        EmployeeViewModel GetEmployeeByID(String ID);
        bool Login(IAuthenticationManager authenticationManager, LoginViewModel lvm);
        bool UpdatePassword(UpdatePasswordViewModel upvm);
         bool UpdateImageUrl(UpdateImageUrlViewModel uiuvm);
        bool UpdateEmployeeDetailsByHR(UpdateEmployeeByHRViewModel uevm);
    }
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository er;
        IHrRoleRepository hr;
        IProjectManagerRoleRepository pr;
        public EmployeeService()
        {
            er = new EmployeeRepository();
            hr = new HrRoleRepository();
            pr = new ProjectManagerRoleRepository();
        }

        public bool DeleteEmployee(string ID)
        {
            var result = er.DeleteEmployee(ID);
            return result;
        }

        public List<EmployeeViewModel> GetEmployeesByRoles(string role)
        {
            List<Employee> employees = er.GetEmployeesByRoles(role);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evms = mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);
            return evms;
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            List<Employee> employees = er.GetEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evms = mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);
            return evms;
        }

        public EmployeeViewModel GetEmployeeByEmail(string Email)
        {
            Employee e = er.GetEmployeeByEmail(Email);
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                EmployeeViewModel evm = mapper.Map<Employee, EmployeeViewModel>(e);
                return evm;
            }
            return null;
        }

        public EmployeeViewModel GetEmployeeByID(string ID)
        {
            Employee e = er.GetEmployeeById(ID);
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                EmployeeViewModel evm = mapper.Map<Employee, EmployeeViewModel>(e);
                return evm;
            }
            return null;
        }

        public bool InsertEmployee(RegisterViewModel rvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<RegisterViewModel, Employee>(rvm);
            e.UserName = rvm.Email;
            e.PasswordHash = Crypto.HashPassword(rvm.Password);
            var result = er.InsertEmployee(e);
            if(result)
            {
                if (e.EmployeeRoles == "HR")
                {
                    HrRole hR = new HrRole();
                    hR.EmployeeID = e.Id;
                    hR.IsSpecialPermission = rvm.IsSpecialPermission;
                    hr.InsertHR(hR);
                }
                else if (e.EmployeeRoles == "PM")
                {
                    ProjectMangerRole projectManger = new ProjectMangerRole();
                    projectManger.EmployeeID = e.Id;
                    pr.InsertPM(projectManger);
                }
            }
            return result;
        }

        public bool Login(IAuthenticationManager authenticationManager, LoginViewModel lvm)
        {
          
            var result = er.Login(authenticationManager, lvm.Email,lvm.Password);
            return result;
        }

        public bool UpdateEmployeeDetails(UpdateEmployeeViewModel uevm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateEmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<UpdateEmployeeViewModel, Employee>(uevm);
            var result = er.UpdateEmpolyeeDetails(e);
            return result;
        }
        public bool UpdateEmployeeDetailsByHR(UpdateEmployeeByHRViewModel uevm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateEmployeeByHRViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<UpdateEmployeeByHRViewModel, Employee>(uevm);
            var result = er.UpdateEmpolyeeDetailsByHR(e);
            return result;
        }

        public bool UpdatePassword(UpdatePasswordViewModel upvm)
        {
            var result = er.UpdatePassword(upvm.Id, upvm.CurrentPassword, upvm.NewPassword);
            return result;
        }

        public bool UpdateImageUrl(UpdateImageUrlViewModel uiuvm)
        {
            var result = er.UpdateImageUrl(uiuvm.Id, uiuvm.ImageUrl);
            return result;
        }
    }
}
