using BusinessLogicLayer.Providers;
using Entities;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleBasedRateSystem.Web.Models
{
    public class UserManagerModel
    {
        private readonly IEmployeeBLL _employeeService;
        private readonly IEmployeeLoginBLL _employeeLoginService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDepartmentRoleBLL _departmentRoleService;
        public UserManagerModel()
        {
        }
        public UserManagerModel(
            IEmployeeLoginBLL employeeLoginService,
            IEmployeeBLL employeeService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IDepartmentRoleBLL departmentRoleService,
            IDepartmentBLL departmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _employeeLoginService = employeeLoginService;
            _employeeService = employeeService;
            _departmentRoleService = departmentRoleService;
        }
        public EmployeeLogin GetEmployeeLoginDetails(string userName)
        {
            var employeeLoginDetails = _employeeLoginService.Queryable()
                .Where(f => f.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();

            if (employeeLoginDetails != null)
            {
                return employeeLoginDetails;
            }
            else
            { return null; }
        }
        public bool IsUserInRole(string userName, string roleName)
        {
            var employeeLoginDetails = _employeeLoginService.Queryable()
                .Where(f => f.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            if (employeeLoginDetails != null)
            {
                var employee = _employeeService.Find(employeeLoginDetails.EmployeeId);
                employee.DepartmentRole = _departmentRoleService.Queryable().Where(f => f.DepartmentRoleId == employeeLoginDetails.Employee.DepartmentRoleId).FirstOrDefault();
                employee.EmployeeLogin = employeeLoginDetails;
                if (employee.DepartmentRole != null)
                {
                    if (employee.DepartmentRole.Name == roleName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
