using System;
using System.Threading.Tasks;
using System.Linq;
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

            var tomorrow = DateTime.Now.Date.AddDays(1);
            model.SapPhaiTra = _invoiceRepository.Where(m=>m.BranchId == CurrentBranchId 
            && m.Status != (byte)TicketStatus.DaTraChoKhach).AsEnumerable().Where(m=> m.NgayTra.Date <= tomorrow).Count();

            model.SapDenHanThu = _invoiceRepository.Where(m => m.BranchId == CurrentBranchId
            && m.Status != (byte)TicketStatus.DaTraChoKhach).AsEnumerable().Where(m => m.NgayThu.HasValue && m.NgayThu.Value.Date <= tomorrow).Count();
            return View(model);
        }
    }
}