using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using BusinessLogicLayer.Providers;
using Repository.Pattern.UnitOfWork;
using RoleBasedRateSystem.Web.Models;
using System.Web.Security;

namespace RoleBasedRateSystem.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IDepartmentBLL _departmentService;
        private readonly IDepartmentRoleBLL _departmentRoleService;
        private readonly IEmployeeBLL _employeeService;
        private readonly IEmployeeLoginBLL _employeeLoginService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public AuthenticationController()
        {
        }
        public AuthenticationController(
            IDepartmentBLL departmentService,
            IDepartmentRoleBLL departmentRoleService,
            IEmployeeBLL employeeService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IEmployeeLoginBLL employeeLoginService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _employeeLoginService = employeeLoginService;
            _employeeService = employeeService;
            _departmentRoleService = departmentRoleService;
            _departmentService = departmentService;
        }
        // GET: Authentication
        //public ActionResult Login()
        //{
        //    return View();
        //}
        // GET: /Authentication/Login
        //[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Authentication/Login
        [HttpPost]
        public ActionResult Login(EmployeeLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserManagerModel userManager = new UserManagerModel(_employeeLoginService, _employeeService, _unitOfWorkAsync, _departmentRoleService, _departmentService);
                model = userManager.GetEmployeeLoginDetails(model.UserName);

                var employee = _employeeService.Queryable().Where(f => f.EmployeeId == model.EmployeeId).FirstOrDefault();
                employee.EmployeeLogin = model;
                employee.DepartmentRole = _departmentRoleService.Queryable().Where(f => f.DepartmentRoleId == employee.DepartmentRoleId).FirstOrDefault();
                employee.Department = _departmentService.Queryable().Where(f => f.DepartmentId == employee.DepartmentId).FirstOrDefault();

                if (string.IsNullOrEmpty(employee.EmployeeLogin.Password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (model.Password.Equals(employee.EmployeeLogin.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);

                        Session["Employee"] = employee;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        ////[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Employee"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }
        public ActionResult UnAuthorized()
        {
            return View();
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