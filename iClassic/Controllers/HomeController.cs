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
        private InvoiceRepository _invoiceRepository;

        public HomeController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _invoiceRepository = new InvoiceRepository(_entities);
        }
        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.ChuaMay = _invoiceRepository.Count(CurrentBranchId, TicketStatus.ChuaXuLy);
            model.DaMayChuaTra = _invoiceRepository.Count(CurrentBranchId, TicketStatus.DaXuLy);
            model.DangXuly = _invoiceRepository.Count(CurrentBranchId, TicketStatus.DangXuLy);

            model.ChuaMuaVai = _invoiceRepository.CountChuaMuaVai(CurrentBranchId);
            return View(model);
        }
    }
}