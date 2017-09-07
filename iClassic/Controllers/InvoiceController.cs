using System;
using System.Linq;
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
    public class InvoiceController : BaseController
    {
        private readonly ILog _log;
        private InvoiceRepository _invoiceRepository;
        private LoaiVaiRepository _loaiVaiRepository;

        public InvoiceController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _invoiceRepository = new InvoiceRepository(_entities);
            _loaiVaiRepository = new LoaiVaiRepository(_entities);
        }

        // GET: Invoicees
        public ActionResult Index(InvoiceSearch model)
        {
            model.BranchId = CurrentBranchId;
            var result = _invoiceRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: Invoicees/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _invoiceRepository.GetByIdAsync(id);
            if (model == null)
            {
                model = new Invoice
                {
                    NgayThu = DateTime.Now.AddDays(SoNgayThuSauKhiLam),
                    NgayTra = DateTime.Now.AddDays(SoNgayThuSauKhiLam),
                    BranchId = CurrentBranchId
                };
            }
            else
            {
                if (model.Status != (byte)TicketStatus.ChuaXuLy)
                    return RedirectToAction("Details", new { id = id });

            }
            CreateCustomerViewBag(model.CustomerId);
            CreateLoaiVaiViewBag();
            CreateListProductTypeViewBag();
            return View(model);
        }

        // POST: Invoicees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Total,ChietKhau,DatCoc,NgayThu,NgayTra,Status,CustomerId,BranchId,PhieuSanXuat,PhieuSua")] Invoice model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.BranchId = CurrentBranchId;
                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _invoiceRepository.Insert(model);
                    }
                    else
                    {
                        model.ModifiedBy = CurrentUserId;
                        model.ModifiedDate = DateTime.Now;
                        _invoiceRepository.Update(model);
                    }
                    await _invoiceRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateCustomerViewBag(model.CustomerId);
            CreateLoaiVaiViewBag();
            CreateListProductTypeViewBag();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _invoiceRepository.GetById(id);
            if (model == null)
                return RedirectToAction("Index");

            if (model.Status == (byte)TicketStatus.ChuaXuLy)
                return RedirectToAction("NewOrEdit", new { id = id });

            return View(model);
        }

        // GET: Invoicees/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _invoiceRepository.GetByIdAsync(id);
                if (obj == null || !IsValidBranch(obj.BranchId))
                {
                    return HttpNotFound();
                }
                _invoiceRepository.Delete(obj);

                await _invoiceRepository.SaveAsync();

                ShowMessageSuccess(Message.Update_Successfully);
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        public ViewResult PrintPhieuHen()
        {
            var list = _entities.Customer.AsQueryable();
            return View(list);
        }

        private void CreateLoaiVaiViewBag()
        {
            ViewBag.MaVaiId = _loaiVaiRepository.GetByBranchId(CurrentBranchId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _invoiceRepository.Dispose();
                _loaiVaiRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
