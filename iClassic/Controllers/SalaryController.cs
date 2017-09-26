using System;
using System.Threading.Tasks;
using System.Net;
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
    public class SalaryController : BaseController
    {
        private readonly ILog _log;
        private SalaryRepository _salaryRepository;
        private UsersRepository _usersRepository;
        private ThoRepository _thoRepository;
        private PhieuSanXuatRepository _phieuSanXuatRepository;

        public SalaryController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _salaryRepository = new SalaryRepository(_entities);
            _usersRepository = new UsersRepository(_entities);
            _thoRepository = new ThoRepository(_entities);
            _phieuSanXuatRepository = new PhieuSanXuatRepository(_entities);
        }

        // GET: Salaryes
        public ActionResult Index(SalarySearch model)
        {
            var result = _salaryRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: Salaryes/NewOrEdit/5
        public ActionResult NewOrEdit(int id = 0)
        {
            var model = _salaryRepository.GetById(id);
            if (model == null)
            {
                model = new Salary()
                {
                    Note = "Lương tháng " + DateTime.Now.ToString("MM - yyyy"),
                    FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    ToDate = DateTime.Now
                };
            }

            CreateViewBag(model.EmployeeId, model.WorkerId);
            return View(model);
        }

        public ActionResult ChangeStatus(int id, bool status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _salaryRepository.GetById(id);
                    if (model == null)
                        return RedirectToAction("Index");

                    model.IsPaid = status;
                    _salaryRepository.Save();

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

        // POST: Salaryes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit(Salary model, SalaryType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (type == SalaryType.Employee)
                    {
                        model.WorkerId = null;
                    }
                    else
                    {
                        model.EmployeeId = null;
                    }
                    if (model.Id == 0)
                    {
                        _salaryRepository.Insert(model);
                    }
                    else
                    {
                        _salaryRepository.Update(model);
                    }
                    await _salaryRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateViewBag(model.EmployeeId, model.WorkerId);
            return View(model);
        }

        // GET: Salaryes/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _salaryRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _salaryRepository.Delete(obj);

                await _salaryRepository.SaveAsync();

                ShowMessageSuccess(Message.Update_Successfully);
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetPhieuSanXuat(int thoId, DateTime startDate, DateTime endDate)
        {
            var tho = _thoRepository.GetById(thoId);
            if (tho == null) return null;

            endDate = endDate.AddDays(1).AddMilliseconds(-1);
            IQueryable<PhieuSanXuat> listPhieuSanXuat;
            switch ((ThoTypes)tho.Type)
            {
                case ThoTypes.Cat:
                    listPhieuSanXuat = _phieuSanXuatRepository.Where(m =>
                        m.Invoice.Status == (byte)TicketStatus.DaTraChoKhach &&
                        m.ThoCatId == thoId &&
                        m.Invoice.NgayTra >= startDate && m.Invoice.NgayTra <= endDate
                    );
                    ViewBag.Luong1Sp = tho.Salary;
                    return PartialView("_PhieuSanXuatThoCat", listPhieuSanXuat);

                case ThoTypes.May:
                case ThoTypes.CatMay:
                    listPhieuSanXuat = _phieuSanXuatRepository.Where(m =>
                        m.Invoice.Status == (byte)TicketStatus.DaTraChoKhach &&
                        m.ThoMayId == thoId &&
                        m.Invoice.NgayTra >= startDate && m.Invoice.NgayTra <= endDate
                    );
                    ViewBag.LuongPhanTramSp = tho.SalaryPercent;
                    return PartialView("_PhieuSanXuatThoMay", listPhieuSanXuat);
            }
            return null;
        }
        private void CreateViewBag(string employeeId = "", int? workerId = 0)
        {
            var users = _usersRepository.Where(m => m.BranchId == CurrentBranchId);
            ViewBag.EmployeeId = users;

            var workers = _thoRepository.GetAll();
            ViewBag.WorkerId = new SelectList(workers, "Id", "Name", workerId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salaryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
