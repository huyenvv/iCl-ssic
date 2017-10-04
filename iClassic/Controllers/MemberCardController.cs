﻿using System;
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
    public class MemberCardController : BaseController
    {
        private readonly ILog _log;
        private MemberCardRepository _memberCardRepository;

        public MemberCardController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _memberCardRepository = new MemberCardRepository(_entities);
        }

        // GET: MemberCardes
        public ActionResult Index(MemberCardSearch model)
        {
            var result = _memberCardRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: MemberCardes/NewOrEdit/5
        public ActionResult NewOrEdit(int id = 0)
        {
            var model = _memberCardRepository.GetById(id);
            if (model == null)
            {
                model = new MemberCard();
            }
            CreateListProductTypeViewBag();
            return View(model);
        }

        // POST: MemberCardes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit(MemberCard model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        _memberCardRepository.Insert(model);
                    }
                    else
                    {
                        _memberCardRepository.Update(model);
                    }
                    await _memberCardRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateListProductTypeViewBag();
            return View(model);
        }

        // GET: MemberCardes/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _memberCardRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _memberCardRepository.Delete(obj);

                await _memberCardRepository.SaveAsync();

                ShowMessageSuccess(Message.Update_Successfully);
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetInfoCard(int id)
        {
            var card = _memberCardRepository.GetById(id);
            return PartialView("_InfoCard", card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _memberCardRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
