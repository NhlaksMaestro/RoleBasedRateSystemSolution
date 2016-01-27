using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DAL;
using BusinessLogicLayer.Providers;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using RoleBasedRateSystem.Web.security;
using RoleBasedRateSystem.Web.Models.ViewModels;
using System.Net;

namespace RoleBasedRateSystem.Web.Controllers
{
    [AuthorizeRole("Sales Executive")]
    public class SalesExecutiveController : Controller
    {
        private readonly IEmployeeBLL _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDepartmentBLL _departmentService;
        private readonly IDepartmentRoleBLL _departmentRoleService;

        public SalesExecutiveController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IEmployeeBLL employeeService,
            IDepartmentBLL departmentService,
            IDepartmentRoleBLL departmentRoleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _departmentRoleService = departmentRoleService;
        }
        public ActionResult SearchRates(int employeeId)
        {
            ViewBag.Message = "Sales Executive page.";
            var Rates = _departmentRoleService.Queryable().ToList();
            var employee = _employeeService.Queryable().Where(f => f.EmployeeId == employeeId).FirstOrDefault();
            var vm = new MainViewModel();
            vm.Employee = employee;
            vm.Rates = Rates;
            vm.MaximumRate = 0;
            vm.MinimumRate = 0;
            return View(vm);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SearchRates([Bind(Include = "MinimumRate, MaximumRate")] MainViewModel viewModel)
        {
            ViewBag.Message = "Sales Executive page.";
            var Rates = _departmentRoleService
                        .Queryable()
                        .Where(x => x.RatePerHour >= viewModel.MinimumRate && x.RatePerHour <= viewModel.MaximumRate)
                        .ToList();

            var vm = new MainViewModel();
            vm.Employee = null;
            vm.Rates = Rates;
            vm.MaximumRate = viewModel.MaximumRate;
            vm.MinimumRate = viewModel.MinimumRate;
            return View(vm);
        }
        public PartialViewResult RatesPartialView(IEnumerable<DepartmentRole> rates)
        {
            ViewBag.Message = "Sales Executive page.";

            var employee = (Session["Employee"] as Employee);
            if (employee == null)
            {
            }

            var Rates = rates.ToList();
            Rates.ForEach(rate =>
            {
                rate.Employees = _employeeService.Queryable().Where(a => a.DepartmentRoleId == rate.DepartmentRoleId).ToList();
            });

            return PartialView(Rates);
        }
        public ActionResult SearchEmployeesInRole(int? departmentRoleId)
        {
            ViewBag.Message = "Sales Executive page.";

            if (departmentRoleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = _employeeService
                            .Queryable()
                            .Where(x => x.DepartmentRoleId == departmentRoleId)
                            .ToList();
            employees.ForEach(Dept =>
            {
                Dept.DepartmentRole = _departmentRoleService.Queryable().Where(a => a.DepartmentRoleId == Dept.DepartmentRoleId).FirstOrDefault();
            });
            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable().AsEnumerable(), "DepartmentRoleId", "Name", departmentRoleId);
            var vm = new MainViewModel();
            vm.Employees = employees;
            return View(vm);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SearchEmployeesInRole([Bind(Include = "departmentRoleId")] MainViewModel viewModel)
        {
            ViewBag.Message = "Sales Executive page.";
            var employees = _employeeService
                        .Queryable()
                        .Where(x => x.DepartmentRoleId == viewModel.DepartmentRoleId)
                        .ToList();

            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable().AsEnumerable(), "DepartmentRoleId", "Name", viewModel.DepartmentRoleId);
            var vm = new MainViewModel();
            vm.Employees = employees;
            return View(vm);
        }
        public PartialViewResult SearchEmployeesInRolePartialView(IEnumerable<Employee> employees)
        {
            ViewBag.Message = "Sales Executive page.";

            var employee = (Session["Employee"] as Employee);
            if (employee == null)
            {
            }

            var Employees = employees.ToList();
            return PartialView(Employees);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}