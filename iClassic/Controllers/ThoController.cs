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
    [Override.Authorize(RoleList.SupperAdmin, RoleList.Admin)]
    public class ThoController : BaseController
    {
        private readonly ILog _log;
        private ThoRepository _thoRepository;

        public ThoController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _thoRepository = new ThoRepository(_entities);
        }

        // GET: Thoes
        public ActionResult Index(ThoSearch model)
        {
            var result = _thoRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: Thoes/NewOrEdit/5
        public ActionResult NewOrEdit(int id = 0)
        {
            var model = _thoRepository.GetById(id);
            if (model == null)
            {
                return View(new Tho());
            }

            return View(model);
        }

        // POST: Thoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Name,Type")] Tho model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        _thoRepository.Insert(model);
                    }
                    else
                    {
                        _thoRepository.Update(model);
                    }
                    await _thoRepository.SaveAsync();

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

        // GET: Thoes/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _thoRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _thoRepository.Delete(obj);

                await _thoRepository.SaveAsync();

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
                _thoRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
