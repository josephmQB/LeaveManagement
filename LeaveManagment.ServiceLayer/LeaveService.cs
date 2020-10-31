﻿using AutoMapper;
using LeaveManagement.DomainModel;
using LeaveManagement.Repository;
using LeaveManagement.ViewModel.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagment.ServiceLayer
{
    public interface ILeaveService
    {
        int InsertLeave(RequestLeaveViewModel rlvm);
        List<LeaveViewModel> GetLeaveByEmployeeID(string EmpID);
        LeaveViewModel GetLeaveByID(int LeaveID);
        void UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm);
    }
    public class LeaveService : ILeaveService
    {
        ILeaveRepository lr;
        public LeaveService()
        {
            lr = new LeaveRepository();
        }
        public List<LeaveViewModel> GetLeaveByEmployeeID(string EmpID)
        {
            List<Leave> leaves = lr.GetLeavesByEmpolyeeID(EmpID);
            List<LeaveViewModel> lvm = null;
            if (leaves != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                lvm = mapper.Map<List<Leave>,List<LeaveViewModel>>(leaves);
            }
            return lvm;
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

        public void UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateLeaveStatusViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<UpdateLeaveStatusViewModel, Leave>(ulsvm);
            lr.UpdateLeaveStatus(l);
        }
    }
}
