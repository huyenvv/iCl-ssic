using iClassic.Helper;
using iClassic.Models;
using iClassic.Services.Implementation;
using log4net;
using Newtonsoft.Json;
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
        private readonly ILog _log;
        private readonly ReportServices _reportServices;
        public StatisticController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _reportServices = new ReportServices(_entities);
        }
        // GET: Statistic
        public ActionResult Index(StatisticSearch model)
        {
            var jsonData = GetGraphJson(model);
            ViewBag.JsonData = jsonData;
            return View();
        }

        public JsonResult GetGraph(StatisticSearch model)
        {
            var jsonData = GetGraphJson(model);
            return Json(new { Data = jsonData }, JsonRequestBehavior.AllowGet);
        }

        private string GetGraphJson(StatisticSearch model)
        {
            var type = model.Type.HasValue ? model.Type.Value : ReportTypes.SevenDaysRecent;
            var data = _reportServices.Graph(type);
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Profit(StatisticSearch model)
        {
            var data = _reportServices.GetProfit(model);
            ViewBag.SearchModel = model;
            return View(data);
        }

        public ActionResult CustomerVip(StatisticSearch model)
        {
            var data = _reportServices.GetCustomerVip(model);
            ViewBag.SearchModel = model;
            return View(data);
        }
    }
}