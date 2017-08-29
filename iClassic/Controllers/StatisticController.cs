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
            BranchPermisson(ref model);
            var jsonData = GetGraphJson(model);
            ViewBag.JsonData = jsonData;
            CreateBranchViewBag(model.BranchId);
            return View();
        }

        // For call Ajax
        public JsonResult GetGraph(StatisticSearch model)
        {
            BranchPermisson(ref model);
            var jsonData = GetGraphJson(model);
            return Json(new { Data = jsonData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profit(StatisticSearch model)
        {
            BranchPermisson(ref model);
            var data = _reportServices.GetProfit(model);
            CreateBranchViewBag(model.BranchId);
            ViewBag.SearchModel = model;
            return View(data);
        }

        public ActionResult CustomerVip(StatisticSearch model)
        {
            BranchPermisson(ref model);
            var data = _reportServices.GetCustomerVip(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            CreateBranchViewBag(model.BranchId);
            return View(data.OrderByDescending(m=>m.SoLanMay).ThenByDescending(m=>m.SoLanSua).ToPagedList(pageNumber, pageSize));
        }


        private void BranchPermisson(ref StatisticSearch model)
        {
            if(model.BranchId == 0)
            {
                model.BranchId = CurrentUser.BranchId;
            }

            if (!User.IsInRole(RoleList.SupperAdmin))
            {
                model.BranchId = CurrentUser.BranchId;
            }
        }

        private string GetGraphJson(StatisticSearch model)
        {
            var type = model.Type.HasValue ? model.Type.Value : ReportTypes.SevenDaysRecent;
            var data = _reportServices.Graph(model);
            return JsonConvert.SerializeObject(data);
        }
    }
}