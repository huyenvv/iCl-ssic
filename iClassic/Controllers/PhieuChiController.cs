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
        private PhieuChiRepository _PhieuChiRepository;

        public PhieuChiController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _PhieuChiRepository = new PhieuChiRepository(_entities);
        }

        // GET: PhieuChies
        public ActionResult Index(PhieuChiSearch model)
        {
            var result = _PhieuChiRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuChies/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _PhieuChiRepository.GetByIdAsync(id);
            if (model == null)
            {
                return View(new PhieuChi());
            }

            return View(model);
        }

        // POST: PhieuChies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Name,Address,SDT")] PhieuChi model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        _PhieuChiRepository.Insert(model);
                    }
                    else
                    {
                        _PhieuChiRepository.Update(model);
                    }
                    await _PhieuChiRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
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
                var obj = await _PhieuChiRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _PhieuChiRepository.Delete(obj);

                await _PhieuChiRepository.SaveAsync();

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
                _PhieuChiRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
