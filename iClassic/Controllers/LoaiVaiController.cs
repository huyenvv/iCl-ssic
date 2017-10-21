using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iClassic.Models;
using iClassic.Services.Implementation;
using PagedList;
using log4net;
using iClassic.Helper;
using Newtonsoft.Json;
using System.Linq;

namespace iClassic.Controllers
{
    [Override.Authorize]
    public class LoaiVaiController : BaseController
    {
        private readonly ILog _log;
        private LoaiVaiRepository _LoaiVaiRepository;

        public LoaiVaiController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _LoaiVaiRepository = new LoaiVaiRepository(_entities);
        }

        // GET: LoaiVaies
        public ActionResult Index(LoaiVaiSearch model)
        {
            model.BranchId = CurrentBranchId;

            var result = _LoaiVaiRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: LoaiVaies/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _LoaiVaiRepository.GetByIdAsync(id);
            if (model == null)
            {
                model = new LoaiVai { BranchId = CurrentBranchId };
            }
            CreateListProductTypeViewBag();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_NewOrEditPartial", model);
            }
            return View(model);
        }

        // POST: LoaiVaies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,Name,ProductTypeLoaiVai,PhieuSanXuat,MaVai,Note,BranchId")] LoaiVai model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.BranchId = CurrentBranchId;
                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _LoaiVaiRepository.Insert(model);
                    }
                    else
                    {
                        _LoaiVaiRepository.Update(model);
                    }
                    await _LoaiVaiRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    if (Request.IsAjaxRequest())
                    {
                        var data = JsonConvert.SerializeObject(new { model.Id, model.MaVai, model.Name, ProductTypeLoaiVai = model.ProductTypeLoaiVai.Select(n => new { n.ProductTypeId, n.Price }) });
                        return Content("<script>addLoaiVaiSuccessed(" + data + ");</script>");
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateListProductTypeViewBag();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_NewOrEditPartial", model);
            }
            return View(model);
        }

        // GET: LoaiVaies/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _LoaiVaiRepository.GetByIdAsync(id);
                if (obj == null || !IsValidBranch(obj.BranchId))
                {
                    return HttpNotFound();
                }
                _LoaiVaiRepository.Delete(obj);

                await _LoaiVaiRepository.SaveAsync();

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
                _LoaiVaiRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
