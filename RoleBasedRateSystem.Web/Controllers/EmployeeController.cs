using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using DAL;
using BusinessLogicLayer.Providers;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using RoleBasedRateSystem.Web.security;

namespace RoleBasedRateSystem.Web.Controllers
{
    public class EmployeeController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IEmployeeBLL _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDepartmentBLL _departmentService;
        private readonly IDepartmentRoleBLL _departmentRoleService;

        public EmployeeController(
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
        [AuthorizeRole("System Administrator")]
        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = _employeeService.Queryable();//db.Employees.Include(e => e.Department).Include(e => e.DepartmentRole);
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [AuthorizeRole("System Administrator")]
        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.Queryable().AsEnumerable(), "DepartmentId", "Name");
            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable(), "DepartmentRoleId", "Name");
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [AuthorizeRole("System Administrator")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,Address,Phone,DateTimeCreated,DepartmentRoleId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Insert(employee);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_departmentService.Queryable().AsEnumerable(), "DepartmentId", "Name", employee.DepartmentId);
            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable().AsEnumerable(), "DepartmentRoleId", "Name", employee.DepartmentRoleId);
            return View(employee);
        }
        
        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(_departmentService.Queryable().AsEnumerable(), "DepartmentId", "Name", employee.DepartmentId);
            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable().AsEnumerable(), "DepartmentRoleId", "Name", employee.DepartmentRoleId);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [AuthorizeRole("System Administrator")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,Address,Phone,DateTimeCreated,DepartmentRoleId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ObjectState = ObjectState.Modified;
                _employeeService.Update(employee);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departmentService.Queryable().AsEnumerable(), "DepartmentId", "Name", employee.DepartmentId);
            ViewBag.DepartmentRoleId = new SelectList(_departmentRoleService.Queryable().AsEnumerable(), "DepartmentRoleId", "Name", employee.DepartmentRoleId);
            return View(employee);
        }

        [AuthorizeRole("System Administrator")]
        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [AuthorizeRole("System Administrator")]
        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _employeeService.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            employee.ObjectState = ObjectState.Deleted;

            _employeeService.Delete(employee);
            _unitOfWorkAsync.SaveChanges();

            return RedirectToAction("Index");
        }
        [AuthorizeRole("System Administrator", "Sales Executive")]
        // GET: /Employee/RoleEmployee/5
        public ActionResult RoleEmployees(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = _employeeService
                            .Queryable()
                            .Where(x => x.DepartmentRoleId == id)
                            .ToList();
            employees.ForEach(Dept =>
            {
                Dept.DepartmentRole = _departmentRoleService.Queryable().Where(a => a.DepartmentRoleId == Dept.DepartmentRoleId).FirstOrDefault();
            });
            return View(employees.ToList());
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
