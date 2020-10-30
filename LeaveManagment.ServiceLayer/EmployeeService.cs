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
        void InsertEmployee(RegisterViewModel rvm);
        void UpdateEmployeeDetails(UpdateEmployeeViewModel uevm);
        void DeleteEmployee(String ID);
        List<EmployeeViewModel> GetEmployees();
        EmployeeViewModel GetEmployeeByEmail(string Email);
        EmployeeViewModel GetEmployeeByID(String ID);
        EmployeeViewModel GetEmployeeByEmailAndPassword(LoginViewModel lvm);
        void Login(IAuthenticationManager authenticationManager, EmployeeViewModel evm);
        void UpdatePassword(UpdatePasswordViewModel upvm);
         void UpdateImageUrl(UpdateImageUrlViewModel uiuvm);
    }
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository er;
        public EmployeeService()
        {
            er = new EmployeeRepository();
        }

        public void DeleteEmployee(string ID)
        {
            var result = er.DeleteEmployee(ID);
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

        public EmployeeViewModel GetEmployeeByEmailAndPassword(LoginViewModel lvm)
        {
            Employee e = er.GetEmployeeByEmailAndPassword(lvm.Email,lvm.Password);
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

        public void InsertEmployee(RegisterViewModel rvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<RegisterViewModel, Employee>(rvm);
            e.UserName = rvm.Email;
            e.PasswordHash = Crypto.HashPassword(rvm.Password);
            var result = er.InsertEmployee(e);
        }

        public void Login(IAuthenticationManager authenticationManager, EmployeeViewModel evm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EmployeeViewModel, Employee>(evm);
            er.Login(authenticationManager, e);
        }

        public void UpdateEmployeeDetails(UpdateEmployeeViewModel uevm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateEmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<UpdateEmployeeViewModel, Employee>(uevm);
            var result = er.UpdateEmpolyeeDetails(e);
        }

        public void UpdatePassword(UpdatePasswordViewModel upvm)
        {
            var result = er.UpdatePassword(upvm.Id, upvm.CurrentPassword, upvm.NewPassword);
        }

        public void UpdateImageUrl(UpdateImageUrlViewModel uiuvm)
        {
            er.UpdateImageUrl(uiuvm.Id, uiuvm.ImageUrl);
        }
    }
}
