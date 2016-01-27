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
using System.Threading.Tasks;
using RoleBasedRateSystem.Web.security;

namespace RoleBasedRateSystem.Web.Controllers
{
    [AuthorizeRole("System Administrator")]
    public class ProjectController : Controller
    {
        private readonly IProjectBLL _projectService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ProjectController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IProjectBLL projectService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _projectService = projectService;
        }
        // GET: /Project/
        public async Task<ActionResult> Index()
        {
            var employees = _projectService.Queryable();
            return View(await employees.ToListAsync());
        }

        // GET: /Project/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _projectService.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ProjectId,Name,Description,DateTimeCreated")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Insert(project);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: /Project/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _projectService.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ProjectId,Name,Description,DateTimeCreated")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.ObjectState = ObjectState.Modified;
                _projectService.Update(project);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _projectService.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Project project = await _projectService.FindAsync(id);
            _projectService.Delete(project);
            await _unitOfWorkAsync.SaveChangesAsync();
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
