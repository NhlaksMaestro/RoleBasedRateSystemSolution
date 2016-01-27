using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Mvc;
using Entities;
using BusinessLogicLayer.Providers;
using Repository.Pattern.UnitOfWork;
using RoleBasedRateSystem.Web.security;
using RoleBasedRateSystem.Web.Models.ViewModels;

namespace RoleBasedRateSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeBLL _employeeService;
        private readonly IDepartmentRoleBLL _departmentRoleService;
        private readonly IDepartmentBLL _departmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public HomeController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IEmployeeBLL employeeService,
            IDepartmentRoleBLL departmentRoleService,
            IDepartmentBLL departmentService)
        {
            _departmentRoleService = departmentRoleService;
            _employeeService = employeeService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _departmentService = departmentService;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Employee Details";
            var Rates = _departmentRoleService.Queryable().ToList();
            var employee = (Session["Employee"] as Employee);
            var vm = new MainViewModel();
            vm.Employee = employee;
            vm.Rates = Rates;
            vm.MaximumRate = 0;
            vm.MinimumRate = 0;
            return View(vm);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }S
    }
}