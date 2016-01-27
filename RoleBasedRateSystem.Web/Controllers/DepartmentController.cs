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
    [AuthorizeRole("System Administrator")]
    public class DepartmentController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IDepartmentBLL _departmentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DepartmentController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IDepartmentBLL departmentService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _departmentService = departmentService;
        }
        // GET: /Department/
        public ActionResult Index()
        {
            var employees = _departmentService.Queryable();
            return View(employees);
        }

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentService.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DepartmentId,Name,MailStop,DateTimeCreated")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Insert(department);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department =_departmentService.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DepartmentId,Name,MailStop,DateTimeCreated")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.ObjectState = ObjectState.Modified;
                _departmentService.Update(department);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: /Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentService.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = _departmentService.Find(id);
            _departmentService.Delete(department);
            _unitOfWorkAsync.SaveChanges();
            return RedirectToAction("Index");
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
