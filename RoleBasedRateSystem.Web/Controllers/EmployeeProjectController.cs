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
    public class EmployeeProjectController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IEmployeeProjectsBLL _employeeProjectsService;
        private readonly IProjectRoleBLL _projectRoleService;
        private readonly IProjectBLL _projectService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IEmployeeBLL _employeeService;

        public EmployeeProjectController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IProjectRoleBLL projectRoleService,
            IProjectBLL projectService,
            IEmployeeBLL employeeService,
            IEmployeeProjectsBLL employeeProjectsService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _projectService = projectService;
            _projectRoleService = projectRoleService;
        _employeeService = employeeService;
        _employeeProjectsService = employeeProjectsService;
        }
        // GET: /EmployeeProject/
        public async Task<ActionResult> Index()
        {
            var employeeprojects = _employeeProjectsService.Queryable();
            return View(await employeeprojects.ToListAsync());
        }

        // GET: /EmployeeProject/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeProject employeeproject = await _employeeProjectsService.FindAsync(id);
            if (employeeproject == null)
            {
                return HttpNotFound();
            }
            return View(employeeproject);
        }

        // GET: /EmployeeProject/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_employeeService.Queryable().AsEnumerable(), "EmployeeId", "FirstName");
            ViewBag.ProjectId = new SelectList(_projectService.Queryable().ToList(), "ProjectId", "Name");
            ViewBag.ProjectRoleId = new SelectList(_projectRoleService.Queryable().ToList(), "ProjectRoleId", "Name");
            return View();
        }

        // POST: /EmployeeProject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="EmployeeId,ProjectId,ProjectRoleId")] EmployeeProject employeeproject)
        {
            if (ModelState.IsValid)
            {
                _employeeProjectsService.Insert(employeeproject);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeService.Queryable().ToList(), "EmployeeId", "FirstName", employeeproject.EmployeeId);
            ViewBag.ProjectId = new SelectList(_projectService.Queryable().ToList(), "ProjectId", "Name", employeeproject.ProjectId);
            ViewBag.ProjectRoleId = new SelectList(_projectRoleService.Queryable().ToList(), "ProjectRoleId", "Name", employeeproject.ProjectRoleId);
            return View(employeeproject);
        }

        // GET: /EmployeeProject/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeProject employeeproject = await _employeeProjectsService.FindAsync(id);
            if (employeeproject == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_employeeService.Queryable().ToList(), "EmployeeId", "FirstName", employeeproject.EmployeeId);
            ViewBag.ProjectId = new SelectList(_projectService.Queryable().ToList(), "ProjectId", "Name", employeeproject.ProjectId);
            ViewBag.ProjectRoleId = new SelectList(_projectRoleService.Queryable().ToList(), "ProjectRoleId", "Name", employeeproject.ProjectRoleId);
            return View(employeeproject);
        }

        // POST: /EmployeeProject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="EmployeeId,ProjectId,ProjectRoleId")] EmployeeProject employeeproject)
        {
            if (ModelState.IsValid)
            {
                employeeproject.ObjectState = ObjectState.Modified;
                _employeeProjectsService.Update(employeeproject);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_employeeService.Queryable().ToList(), "EmployeeId", "FirstName", employeeproject.EmployeeId);
            ViewBag.ProjectId = new SelectList(_projectService.Queryable().ToList(), "ProjectId", "Name", employeeproject.ProjectId);
            ViewBag.ProjectRoleId = new SelectList(_projectRoleService.Queryable().ToList(), "ProjectRoleId", "Name", employeeproject.ProjectRoleId);
            return View(employeeproject);
        }

        // GET: /EmployeeProject/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeProject employeeproject = await _employeeProjectsService.FindAsync(id);
            if (employeeproject == null)
            {
                return HttpNotFound();
            }
            return View(employeeproject);
        }

        // POST: /EmployeeProject/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EmployeeProject employeeproject = await _employeeProjectsService.FindAsync(id);
            _employeeProjectsService.Delete(employeeproject);
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
