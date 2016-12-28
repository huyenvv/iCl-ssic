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
    public class TienGaoController : BaseController
    {
        private readonly CmContext _db = new CmContext();
        // GET: /TienGao/
        public async Task<ActionResult> Index(int dotChotId = 0)
        {
            CreateViewBagDotChotSo();
            var dotChotGanNhat = await _db.DotChotSo.FirstOrDefaultAsync(m => m.Id == dotChotId) ??
                                 await _db.DotChotSo.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            if (dotChotGanNhat == null) return View(await _db.TienGao.ToListAsync());

            var tienGao = _db.TienGao.Where(m => m.ChotSoId == dotChotGanNhat.Id).OrderBy(m => m.CreateDate).ThenBy(m => m.Id);
            CreateViewBagDotChotSo(dotChotGanNhat.Id);
            return View(await tienGao.ToListAsync());
        }

        // GET: /TienGao/Create
        public async Task<ActionResult> CreateOrEdit(int id = 0)
        {
            TienGao tienGao = await _db.TienGao.FindAsync(id);
            if (tienGao == null)
            {
                tienGao = new TienGao
                {
                    CreateDate = DateTime.Now,
                };
            }
            CreateViewBagDotChotSo(tienGao.ChotSoId);
            return View(tienGao);
        }

        // POST: /TienGao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit([Bind(Include = "Id,Title,SoTien,ChotSoId")] TienGao tienGao)
        {
            if (ModelState.IsValid)
            {
                if (tienGao.ChotSoId > 0)
                {
                    tienGao.Title = Common.Encode(tienGao.Title);
                    tienGao.CreateBy = User.Identity.GetUserId();
                    tienGao.CreateDate = DateTime.Now;

                    if (tienGao.Id > 0)
                    {
                        _db.Entry(tienGao).State = EntityState.Modified;
                    }
                    else
                    {
                        _db.TienGao.Add(tienGao);
                    }
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", new { dotChotId = tienGao.ChotSoId });
                }
                ModelState.AddModelError("ChotSoId", "Chưa chọn đợt chốt sổ");
            }
            CreateViewBagDotChotSo(tienGao.ChotSoId);
            return View(tienGao);
        }


        // GET: /TienGao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            TienGao tienGao = await _db.TienGao.FindAsync(id);
            if (tienGao == null)
            {
                return HttpNotFound();
            }

            _db.TienGao.Remove(tienGao);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { dotChotId = tienGao.ChotSoId });
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
