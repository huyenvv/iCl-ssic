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
    [Override.Authorize(RoleList.SupperAdmin, RoleList.Admin)]
    public class GiaoNhanController : BaseController
    {
        private readonly ILog _log;
        private InvoiceRepository _invoiceRepository;
        private PhieuSanXuatRepository _phieuSanXuatRepository;
        private PhieuSuaRepository _phieuSuaRepository;

        public GiaoNhanController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _invoiceRepository = new InvoiceRepository(_entities);
            _phieuSanXuatRepository = new PhieuSanXuatRepository(_entities);
            _phieuSuaRepository = new PhieuSuaRepository(_entities);
        }

        // GET: Thoes
        public ActionResult Index(ThoSearch model)
        {
            var result = _invoiceRepository.Where(m=>m.Status != (byte)TicketStatus.DaTraChoKhach).OrderByDescending(m => m.Id);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChangeStatusPhieuSanXuat(int id, PhieuSanXuatStatus status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _phieuSanXuatRepository.GetById(id);
                    if (model == null)
                        return RedirectToAction("Index");

                    model.StatusGiaoNhan = (byte)status;
                    model.Invoice.ModifiedDate = DateTime.Now;
                    model.Invoice.ModifiedBy = CurrentUserId;
                    _phieuSanXuatRepository.Save();

                    ShowMessageSuccess(Message.Update_Successfully);
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatusPhieuSua(int id, PhieuSuaStatus status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _phieuSuaRepository.GetById(id);
                    if (model == null)
                        return RedirectToAction("Index");

                    model.StatusGiaoNhan = (byte)status;
                    model.Invoice.ModifiedDate = DateTime.Now;
                    model.Invoice.ModifiedBy = CurrentUserId;
                    _phieuSuaRepository.Save();

                    ShowMessageSuccess(Message.Update_Successfully);
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _invoiceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
