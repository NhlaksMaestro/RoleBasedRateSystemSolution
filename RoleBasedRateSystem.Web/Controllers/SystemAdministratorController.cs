using RoleBasedRateSystem.Web.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleBasedRateSystem.Web.Controllers
{
    [AuthorizeRole("System Administrator")]
    public class SystemAdministratorController : Controller
    {
        // GET: SystemAdministrator
        public ActionResult Index()
        {
            return View();
        }
    }
}