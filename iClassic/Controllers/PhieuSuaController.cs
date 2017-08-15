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
    public class PhieuSuaController : BaseController
    {
        private readonly ILog _log;
        private PhieuSuaRepository _PhieuSuaRepository;

        public PhieuSuaController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _PhieuSuaRepository = new PhieuSuaRepository(_entities);
        }

        // GET: PhieuSuaes
        public ActionResult Index(PhieuSuaSearch model)
        {
            var result = _PhieuSuaRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuSuaes/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _PhieuSuaRepository.GetByIdAsync(id);
            if (model == null)
            {
                return View(new PhieuSua());
            }

            return View(model);
        }

        // POST: PhieuSuaes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Name,SoTien,MaVai,Note")] PhieuSua model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _PhieuSuaRepository.Insert(model);
                    }
                    else
                    {
                        _PhieuSuaRepository.Update(model);
                    }
                    await _PhieuSuaRepository.SaveAsync();

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

        // GET: PhieuSuaes/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _PhieuSuaRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _PhieuSuaRepository.Delete(obj);

                await _PhieuSuaRepository.SaveAsync();

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
                _PhieuSuaRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
