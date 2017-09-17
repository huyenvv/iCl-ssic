using iClassic.Helper;
using iClassic.Models;
using iClassic.Services.Implementation;
using log4net;
using Newtonsoft.Json;
using PagedList;
using System.Linq;
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
            model.BranchId = CurrentBranchId;
            var jsonData = GetGraphJson(model);
            ViewBag.JsonData = jsonData;
            return View();
        }

        // For call Ajax
        public JsonResult GetGraph(StatisticSearch model)
        {
            model.BranchId = CurrentBranchId;
            var jsonData = GetGraphJson(model);
            return Json(new { Data = jsonData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profit(StatisticSearch model)
        {
            model.BranchId = CurrentBranchId;
            var data = _reportServices.GetProfit(model);
            ViewBag.SearchModel = model;
            return View(data);
        }        

        public ActionResult CustomerVip(StatisticSearch model)
        {
            model.BranchId = CurrentBranchId;
            var data = _reportServices.GetCustomerVip(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(data.OrderByDescending(m=>m.SoLanMay).ThenByDescending(m=>m.SoLanSua).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetErrorsInProcessing(StatisticSearch model)
        {
            model.BranchId = CurrentBranchId;
            var data = _reportServices.GetErrorsInProcessing(model);
            ViewBag.SearchModel = model;
            return View(data);
        }

        public ActionResult CustomerChanelAvertising(StatisticSearch model)
        {
            model.BranchId = CurrentBranchId;
            var data = _reportServices.CustomerChanelAvertising(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(data);
        }

        private string GetGraphJson(StatisticSearch model)
        {
            var type = model.Type.HasValue ? model.Type.Value : ReportTypes.SevenDaysRecent;
            var data = _reportServices.Graph(model);
            return JsonConvert.SerializeObject(data);
        }
    }
}