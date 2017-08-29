using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iClassic.Models;
using iClassic.Services.Implementation;
using PagedList;
using log4net;
using iClassic.Helper;

namespace iClassic.Controllers
{
    [Override.Authorize]
    public class HomeController : BaseController
    {
        private readonly ILog _log;
        private PhieuSuaRepository _phieuSuaRepository;
        private PhieuSanXuatRepository _phieuSanXuatRepository;

        public HomeController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _phieuSuaRepository = new PhieuSuaRepository(_entities);
            _phieuSanXuatRepository = new PhieuSanXuatRepository(_entities);
        }
        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.ChuaMay = _phieuSanXuatRepository.Count(CurrentUser.BranchId, TicketStatus.ChuaXuLy);
            model.DaMayChuaTra = _phieuSanXuatRepository.Count(CurrentUser.BranchId, TicketStatus.DaXuLy);

            model.ChuaSua = _phieuSuaRepository.Count(CurrentUser.BranchId, TicketStatus.ChuaXuLy);
            model.DaSuaChuaTra = _phieuSuaRepository.Count(CurrentUser.BranchId, TicketStatus.DaXuLy);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}