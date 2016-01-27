using BusinessLogicLayer.Providers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace RoleBasedRateSystem.Web.Controllers
{
    public class AutomationController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IEmployeeBLL _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDepartmentBLL _departmentService;
        private readonly IDepartmentRoleBLL _departmentRoleService;
        private readonly IEmployeeLoginBLL _employeeLoginService;

        public AutomationController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IEmployeeLoginBLL employeeLoginService,
            IEmployeeBLL employeeService,
            IDepartmentBLL departmentService,
            IDepartmentRoleBLL departmentRoleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _departmentRoleService = departmentRoleService;
            _employeeLoginService = employeeLoginService;
        }
        // GET: RunSystemAdministrator
        public ActionResult RunSystemAdministrator()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:4601/");
            var emp = _employeeService
                             .Queryable()
                             .Where(x => x.DepartmentRoleId == 9)
                             .FirstOrDefault();
            emp.EmployeeLogin = _employeeLoginService
                                .Queryable()
                                .Where(x => x.EmployeeId == emp.EmployeeId)
                                .FirstOrDefault();
            IWebElement UserName = driver.FindElement(By.Name("UserName"));
            IWebElement Password = driver.FindElement(By.Name("Password"));
            var loginButton = driver.FindElement(By.XPath("/html/body/div/div/div/section/form/button"));

            UserName.SendKeys(emp.EmployeeLogin.UserName);
            Password.SendKeys(emp.EmployeeLogin.Password);
            loginButton.Click();

            Timer timer = new Timer(1000);
            timer.Start();

            timer.Stop();
            return View();
        }
        // GET: RunSystemAdministrator
        public ActionResult RunSalesExecutive()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:4601/");
            var emp = _employeeService
                             .Queryable()
                             .Where(x => x.DepartmentRoleId == 10)
                             .FirstOrDefault ();
            emp.EmployeeLogin = _employeeLoginService
                                .Queryable()
                                .Where(x => x.EmployeeId == emp.EmployeeId)
                                .FirstOrDefault();
            IWebElement UserName = driver.FindElement(By.Name("UserName"));
            IWebElement Password = driver.FindElement(By.Name("Password"));
            var loginButton = driver.FindElement(By.XPath("/html/body/div/div/div/section/form/button"));

            UserName.SendKeys(emp.EmployeeLogin.UserName);
            Password.SendKeys(emp.EmployeeLogin.Password);
            loginButton.Click();

            Timer timer = new Timer(1000);
            timer.Start();

            timer.Stop();
            return View();
        }
        // GET: RunUser
        public ActionResult RunUser()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:4601/");
            var emp = _employeeService
                             .Queryable()
                             .Where(x => x.DepartmentRoleId != 10 || x.DepartmentRoleId != 9)
                             .FirstOrDefault();
            emp.EmployeeLogin = _employeeLoginService
                                .Queryable()
                                .Where(x => x.EmployeeId == emp.EmployeeId)
                                .FirstOrDefault();
            IWebElement UserName = driver.FindElement(By.Name("UserName"));
            IWebElement Password = driver.FindElement(By.Name("Password"));
            var loginButton = driver.FindElement(By.XPath("/html/body/div/div/div/section/form/button"));

            UserName.SendKeys(emp.EmployeeLogin.UserName);
            Password.SendKeys(emp.EmployeeLogin.Password);
            loginButton.Click();

            Timer timer = new Timer(1000);
            timer.Start();

            timer.Stop();
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