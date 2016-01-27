using BusinessLogicLayer.Providers;
using Repository.Pattern.UnitOfWork;
using RoleBasedRateSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleBasedRateSystem.Web.security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {

        private readonly string[] userAssignedRoles;
        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IEmployeeBLL _employeeService = DependencyResolver.Current.GetService<IEmployeeBLL>();
            IEmployeeLoginBLL _employeeLoginService = DependencyResolver.Current.GetService<IEmployeeLoginBLL>();
            IUnitOfWorkAsync _unitOfWorkAsync = DependencyResolver.Current.GetService<IUnitOfWorkAsync>();
            IDepartmentRoleBLL _departmentRoleService = DependencyResolver.Current.GetService<IDepartmentRoleBLL>();
            IDepartmentBLL _departmentService = DependencyResolver.Current.GetService<IDepartmentBLL>();
            bool authorize = false;

            foreach (var roles in userAssignedRoles)
            {
                UserManagerModel userManager = new UserManagerModel(_employeeLoginService, _employeeService, _unitOfWorkAsync, _departmentRoleService, _departmentService);

                authorize = userManager.IsUserInRole(httpContext.User.Identity.Name, roles);
                if (authorize)
                {
                    _employeeService = null;
                    _employeeLoginService = null;
                    _unitOfWorkAsync = null;
                    _departmentRoleService = null;
                    return authorize;
                }
            }

            _employeeService = null;
            _employeeLoginService = null;
            _unitOfWorkAsync = null;
            _departmentRoleService = null;
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Authentication/UnAuthorized");
        }
    }
}