using System;
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
    public class DsNopTruocController : BaseController
    {
        private readonly CmContext _db = new CmContext();
        // GET: /DsNopTruoc/
        public async Task<ActionResult> Index(int dotChotId = 0)
        {
            CreateViewBagDotChotSo();
            var dotChotGanNhat = await _db.DotChotSo.FirstOrDefaultAsync(m => m.Id == dotChotId) ??
                                 await _db.DotChotSo.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            if (dotChotGanNhat == null) return View(await _db.DsNopTruoc.ToListAsync());

            var dsnoptruoc = _db.DsNopTruoc.Where(m => m.ChotSoId == dotChotGanNhat.Id).OrderBy(m => m.CreateDate).ThenBy(m => m.Id);
            CreateViewBagDotChotSo(dotChotGanNhat.Id);
            return View(await dsnoptruoc.ToListAsync());
        }

        // GET: /DsNopTruoc/Create
        public async Task<ActionResult> CreateOrEdit(int id = 0)
        {
            DsNopTruoc dsnoptruoc = await _db.DsNopTruoc.FindAsync(id);
            if (dsnoptruoc == null)
            {
                dsnoptruoc = new DsNopTruoc
                {
                    CreateDate = DateTime.Now,
                };
            }
            ViewBag.NguoiNopId = new SelectList(_db.Users.Where(m => m.IsShow).OrderBy(m => m.Name).ToList().Select(m => new { m.Id, Name = Common.Decode(m.Name) }), "Id", "Name");
            CreateViewBagDotChotSo(dsnoptruoc.ChotSoId);
            return View(dsnoptruoc);
        }

        // POST: /DsNopTruoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit([Bind(Include = "Id,Title,SoTien,NguoiNopId,ChotSoId")] DsNopTruoc dsnoptruoc)
        {
            if (ModelState.IsValid)
            {
                if (dsnoptruoc.ChotSoId > 0)
                {
                    dsnoptruoc.Title = Common.Encode(dsnoptruoc.Title);
                    dsnoptruoc.CreateBy = User.Identity.GetUserId();
                    dsnoptruoc.CreateDate = DateTime.Now;

                    if (dsnoptruoc.Id > 0)
                    {
                        _db.Entry(dsnoptruoc).State = EntityState.Modified;
                    }
                    else
                    {
                        _db.DsNopTruoc.Add(dsnoptruoc);
                    }
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", new { dotChotId = dsnoptruoc.ChotSoId });
                }
                ModelState.AddModelError("ChotSoId", "Chưa chọn đợt chốt sổ");
            }

            ViewBag.NguoiNopId = new SelectList(_db.Users.Where(m => m.IsShow).OrderBy(m => m.Name).ToList().Select(m => new { m.Id, Name = Common.Decode(m.Name) }), "Id", "Name");
            CreateViewBagDotChotSo(dsnoptruoc.ChotSoId);
            return View(dsnoptruoc);
        }


        // GET: /DsNopTruoc/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DsNopTruoc dsnoptruoc = await _db.DsNopTruoc.FindAsync(id);
            if (dsnoptruoc == null)
            {
                return HttpNotFound();
            }

            _db.DsNopTruoc.Remove(dsnoptruoc);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { dotChotId = dsnoptruoc.ChotSoId });
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
