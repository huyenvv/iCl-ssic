﻿using System;
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
    public class PhieuSanXuatController : BaseController
    {
        private readonly ILog _log;
        private PhieuSanXuatRepository _phieuSanXuatRepository;
        private LoaiVaiRepository _loaiVaiRepository;

        public PhieuSanXuatController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _phieuSanXuatRepository = new PhieuSanXuatRepository(_entities);
            _loaiVaiRepository = new LoaiVaiRepository(_entities);
        }

        // GET: PhieuSanXuates
        public ActionResult Index(PhieuSanXuatSearch model)
        {
            var result = _phieuSanXuatRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuSanXuates/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _phieuSanXuatRepository.GetByIdAsync(id);
            if (model == null)
            {
                model = new PhieuSanXuat { NgayThu = DateTime.Now.AddDays(SoNgayThuSauKhiLam), NgayLay = DateTime.Now.AddDays(SoNgayThuSauKhiLam), SoLuong = 1 };
            }            
            CreateCustomerViewBag(model.KhachHangId, CurrentUser.BranchId);
            CreateLoaiVaiViewBag(model.MaVaiId, CurrentUser.BranchId);
            return View(model);
        }

        // POST: PhieuSanXuates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,TenSanPham,MaVaiId,DonGia,SoLuong,DatCoc,NgayThu,NgayLay,Status,KhachHangId")] PhieuSanXuat model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _phieuSanXuatRepository.Insert(model);
                    }
                    else
                    {
                        _phieuSanXuatRepository.Update(model);
                    }
                    await _phieuSanXuatRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateCustomerViewBag(model.KhachHangId, CurrentUser.BranchId);
            CreateLoaiVaiViewBag(model.MaVaiId, model.Customer.BranchId);
            return View(model);
        }

        // GET: PhieuSanXuates/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _phieuSanXuatRepository.GetByIdAsync(id);
                if (obj == null || !User.IsInRole(RoleList.SupperAdmin) && CurrentUser.BranchId != obj.Customer.BranchId)
                {
                    return HttpNotFound();
                }
                _phieuSanXuatRepository.Delete(obj);

                await _phieuSanXuatRepository.SaveAsync();

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

        private void CreateLoaiVaiViewBag(int maVaiId, int branchId)
        {
            ViewBag.MaVaiId = _loaiVaiRepository.GetByBranchId(branchId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _phieuSanXuatRepository.Dispose();
                _loaiVaiRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
