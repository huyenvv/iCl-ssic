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
        private PhieuSanXuatRepository _phieuSanXuatRepository;
        private LoaiVaiRepository _loaiVaiRepository;
        private ThoRepository _thoRepository;
        private MemberCardRepository _memberCardRepository;

        public InvoiceController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _invoiceRepository = new InvoiceRepository(_entities);
            _phieuSanXuatRepository = new PhieuSanXuatRepository(_entities);
            _loaiVaiRepository = new LoaiVaiRepository(_entities);
            _thoRepository = new ThoRepository(_entities);
            _memberCardRepository = new MemberCardRepository(_entities);
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
                    NgayTra = DateTime.Now.AddDays(SoNgayTraSauKhiSua),
                    BranchId = CurrentBranchId,
                    ChietKhauType = (byte)ChietKhauType.SoTien
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
            CreateDanhSachThoViewBag();
            CreateDanhSachHangTheViewBag();
            return View(model);
        }

        // POST: Invoicees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Total,ChietKhau,ChietKhauType,DatCoc,NgayThu,NgayTra,Status,CustomerId,BranchId,PhieuSanXuat,PhieuSua")] Invoice model)
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
            CreateDanhSachThoViewBag();
            CreateDanhSachHangTheViewBag();
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
        public ActionResult ChangeStatusMuaVai(int id, int idPhieusx, bool status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _invoiceRepository.GetById(id);
                    if (model == null)
                        return RedirectToAction("Index");

                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = DateTime.Now;
                    _phieuSanXuatRepository.ChangeStatusMuaVai(idPhieusx, status);
                    _phieuSanXuatRepository.Save();

                    ShowMessageSuccess(Message.Update_Successfully);
                    return RedirectToAction("Details", new { id = id });
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult Proccess(int id, int status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _invoiceRepository.GetById(id);
                    if (model == null)
                        return RedirectToAction("Index");

                    model.ModifiedBy = CurrentUserId;
                    _invoiceRepository.ChangeStatus(model, status);
                    _invoiceRepository.Save();

                    ShowMessageSuccess(Message.Update_Successfully);
                    return RedirectToAction("Details", new { id = id });
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Details", new { id = id });
        }
        // GET: Invoicees/Delete/5
        [Override.Authorize(RoleList.Admin, RoleList.SupperAdmin)]
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

        public ActionResult Print(int id)
        {
            var model = _invoiceRepository.GetById(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        public ActionResult PrintPhieuSanXuat(int id)
        {
            var model = _invoiceRepository.GetById(id);
            if (model == null)
                return RedirectToAction("Index");

            CreateListProductTypeViewBag();
            return View(model);
        }

        public ActionResult PrintPhieuSua(int id)
        {
            var model = _invoiceRepository.GetById(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        private void CreateLoaiVaiViewBag()
        {
            ViewBag.MaVaiId = _loaiVaiRepository.GetByBranchId(CurrentBranchId);
        }

        private void CreateDanhSachThoViewBag()
        {
            ViewBag.ListThoMayDo = _thoRepository.GetAll();
        }

        private void CreateDanhSachHangTheViewBag()
        {
            ViewBag.ListHangThe = _memberCardRepository.GetAll();
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
