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
    public class ProjectRoleController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IProjectRoleBLL _projectRoleService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public ProjectRoleController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IProjectRoleBLL projectRoleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _projectRoleService = projectRoleService;
        }

        // GET: /ProjectRole/
        public async Task<ActionResult> Index()
        {
            var projects = _projectRoleService.Queryable();
            return View(await projects.ToListAsync());
        }

        // GET: /ProjectRole/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRole projectrole = await _projectRoleService.FindAsync(id);
            if (projectrole == null)
            {
                return HttpNotFound();
            }
            return View(projectrole);
        }

        // GET: /ProjectRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProjectRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ProjectRoleId,Name,Description,DateTimeCreated")] ProjectRole projectrole)
        {
            if (ModelState.IsValid)
            {
                _projectRoleService.Update(projectrole);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projectrole);
        }

        // GET: /ProjectRole/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRole projectrole = await _projectRoleService.FindAsync(id);
            if (projectrole == null)
            {
                return HttpNotFound();
            }
            return View(projectrole);
        }

        // POST: /ProjectRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ProjectRoleId,Name,Description,DateTimeCreated")] ProjectRole projectrole)
        {
            if (ModelState.IsValid)
            {
                projectrole.ObjectState = ObjectState.Modified;
                _projectRoleService.Insert(projectrole);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectrole);
        }

        // GET: /ProjectRole/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRole projectrole = await _projectRoleService.FindAsync(id);
            if (projectrole == null)
            {
                return HttpNotFound();
            }
            return View(projectrole);
        }

        // POST: /ProjectRole/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectRole projectrole = await _projectRoleService.FindAsync(id);
            _projectRoleService.Delete(projectrole);
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
