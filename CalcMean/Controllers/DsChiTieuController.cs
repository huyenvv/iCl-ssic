using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CalcMean.Models;
using Microsoft.AspNet.Identity;

namespace CalcMean.Controllers
{
    [Authorize]
    public class DsChiTieuController : BaseController
    {
        private readonly CmContext _db = new CmContext();
        // GET: /DsChiTieu/
        public async Task<ActionResult> Index(int dotChotId = 0)
        {
            CreateViewBagDotChotSo();
            var dotChotGanNhat = await _db.DotChotSo.FirstOrDefaultAsync(m => m.Id == dotChotId) ??
                                 await _db.DotChotSo.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            if (dotChotGanNhat == null) return View(await _db.DsChiTieu.ToListAsync());

            var dschitieu = _db.DsChiTieu.Where(m => m.ChotSoId == dotChotGanNhat.Id);
            CreateViewBagDotChotSo(dotChotGanNhat.Id);
            return View(await dschitieu.OrderBy(m => m.CreateDate).ThenBy(m=>m.Id).Include(d => d.DotChotSo).Include(m => m.UserInChiTieu).ToListAsync());
        }

        public async Task<ActionResult> CreateOrEdit(int id = 0)
        {
            DsChiTieu dsChiTieu = await _db.DsChiTieu.Include(m => m.UserInChiTieu).FirstOrDefaultAsync(m => m.Id == id);
            if (dsChiTieu == null)
            {
                dsChiTieu = new DsChiTieu
                {
                    CreateDate = DateTime.Now,
                    CreateBy = CurrentUser().Id
                };
            }

            var dsUser = _db.Users.Where(m => m.IsShow).OrderBy(m => m.Name);
            ViewBag.NguoiTieuId = new SelectList(dsUser.ToList().Select(m => new { m.Id, Name = Common.Decode(m.Name) }), "Id", "Name");
            CreateViewBagDotChotSo(dsChiTieu.ChotSoId);
            ViewBag.ListUser = dsUser;
            ViewBag.usersEaten = dsChiTieu.UserInChiTieu != null ? dsChiTieu.UserInChiTieu.Select(m => m.ForUserId.ToString()).ToList() : new List<string>();
            return View(dsChiTieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit([Bind(Include = "Id,Title,SoTien,UserTieuId,ChotSoId,CreateBy,CreateDate")] DsChiTieu dsChiTieu, List<string> usersEaten)
        {
            if (ModelState.IsValid)
            {
                if (dsChiTieu.ChotSoId > 0)
                {
                    if (usersEaten != null && usersEaten.Count > 0)
                    {
                        dsChiTieu.Title = Common.Encode(dsChiTieu.Title);
                        dsChiTieu.CreateBy = CurrentUser().Id;
                        dsChiTieu.CreateDate = dsChiTieu.CreateDate;
                        var listUserInChiTieuNew = usersEaten.Select(m => new UserInChiTieu
                        {
                            ForUserId = m,
                            SoTien = dsChiTieu.SoTien / (decimal)usersEaten.Count,
                            ChiTieuId = dsChiTieu.Id
                        }).ToList();

                        if (dsChiTieu.Id > 0)
                        {
                            var listUserInChiTieu = _db.UserInChiTieu.Where(m => m.ChiTieuId == dsChiTieu.Id);
                            _db.UserInChiTieu.RemoveRange(listUserInChiTieu);
                            await _db.SaveChangesAsync();

                            _db.UserInChiTieu.AddRange(listUserInChiTieuNew);
                            _db.Entry(dsChiTieu).State = EntityState.Modified;
                        }
                        else
                        {
                            dsChiTieu.UserInChiTieu = listUserInChiTieuNew;
                            _db.DsChiTieu.Add(dsChiTieu);
                        }
                        await _db.SaveChangesAsync();
                        return RedirectToAction("Index", new { dotChotId = dsChiTieu.ChotSoId });
                    }
                    ModelState.AddModelError("", "Chưa chọn người ăn");
                }
                else
                {
                    ModelState.AddModelError("ChotSoId", "Chưa chọn đợt chốt sổ");
                }
            }

            var dsUser = _db.Users.Where(m => m.IsShow).OrderBy(m => m.Name);
            ViewBag.NguoiTieuId = new SelectList(dsUser.ToList().Select(m => new { m.Id, Name = Common.Decode(m.Name) }), "Id", "Name");
            CreateViewBagDotChotSo(dsChiTieu.ChotSoId);
            ViewBag.ListUser = dsUser;
            ViewBag.usersEaten = usersEaten;
            return View(dsChiTieu);
        }


        // GET: /DsChiTieu/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DsChiTieu dsChiTieu = await _db.DsChiTieu.FindAsync(id);
            if (dsChiTieu == null)
            {
                return HttpNotFound();
            }

            _db.DsChiTieu.Remove(dsChiTieu);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { dotChotId = dsChiTieu.ChotSoId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
