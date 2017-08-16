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
    public class PhieuChiController : BaseController
    {
        private readonly ILog _log;
        private PhieuChiRepository _phieuChiRepository;

        public PhieuChiController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _phieuChiRepository = new PhieuChiRepository(_entities);
        }

        // GET: PhieuChies
        public ActionResult Index(PhieuChiSearch model)
        {
            var result = _phieuChiRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            CreateBrachViewBag(model.BranchId);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuChies/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _phieuChiRepository.GetByIdAsync(id);
            if (model == null)
            {
                model = new PhieuChi();
            }

            CreateBrachViewBag(model.BranchId);
            return View(model);
        }

        // POST: PhieuChies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,MucChi,SoTien,NguoiNhanPhieu,ChiNhanhId")] PhieuChi model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _phieuChiRepository.Insert(model);
                    }
                    else
                    {
                        _phieuChiRepository.Update(model);
                    }
                    await _phieuChiRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateBrachViewBag(model.BranchId);
            return View(model);
        }

        // GET: PhieuChies/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _phieuChiRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _phieuChiRepository.Delete(obj);

                await _phieuChiRepository.SaveAsync();

                ShowMessageSuccess(Message.Update_Successfully);
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
                _phieuChiRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
