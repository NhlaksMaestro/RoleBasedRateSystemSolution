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
    public class DepartmentRoleController : Controller
    {
        //private RoleBasedSystemDBContext db = new RoleBasedSystemDBContext();
        private readonly IDepartmentRoleBLL _departmentRoleService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public DepartmentRoleController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IDepartmentRoleBLL departmentRoleService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _departmentRoleService = departmentRoleService;
        }
        // GET: /DepartmentRole/
        public async Task<ActionResult> Index()
        {
            var departmentRoles = _departmentRoleService.Queryable();
            return View(await departmentRoles.ToListAsync());
        }

        // GET: /DepartmentRole/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentRole departmentrole = await _departmentRoleService.FindAsync(id);
            if (departmentrole == null)
            {
                return HttpNotFound();
            }
            return View(departmentrole);
        }

        // GET: /DepartmentRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DepartmentRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="DepartmentRoleId,Name,Description,DateTimeCreated,RatePerHour")] DepartmentRole departmentrole)
        {
            if (ModelState.IsValid)
            {
                _departmentRoleService.Insert(departmentrole);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(departmentrole);
        }

        // GET: /DepartmentRole/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentRole departmentrole = await _departmentRoleService.FindAsync(id);
            if (departmentrole == null)
            {
                return HttpNotFound();
            }
            return View(departmentrole);
        }

        // POST: /DepartmentRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="DepartmentRoleId,Name,Description,DateTimeCreated,RatePerHour")] DepartmentRole departmentrole)
        {
            if (ModelState.IsValid)
            {
                departmentrole.ObjectState = ObjectState.Modified;
                _departmentRoleService.Update(departmentrole);
                await _unitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(departmentrole);
        }

        // GET: /DepartmentRole/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentRole departmentrole = await _departmentRoleService.FindAsync(id);
            if (departmentrole == null)
            {
                return HttpNotFound();
            }
            return View(departmentrole);
        }

        // POST: /DepartmentRole/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DepartmentRole departmentrole = await _departmentRoleService.FindAsync(id);
            _departmentRoleService.Delete(departmentrole);
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
