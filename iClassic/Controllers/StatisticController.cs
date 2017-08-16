using iClassic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iClassic.Controllers
{
    [Override.Authorize(RoleList.Admin, RoleList.SupperAdmin)]
    public class StatisticController : BaseController
    {
        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}