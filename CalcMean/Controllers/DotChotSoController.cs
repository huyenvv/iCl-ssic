using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web.Mvc;
using CalcMean.Models;
using Microsoft.AspNet.Identity;

namespace CalcMean.Controllers
{
    [Authorize]
    public class DotChotSoController : BaseController
    {
        private readonly CmContext _db = new CmContext();
        // GET: /DotChotSo/
        public ActionResult Index()
        {
            return View(_db.DotChotSo.Include(m => m.TienGao).OrderByDescending(m => m.Id).ToList().Select(m => new DotChotSo
            {
                Id = m.Id,
                Title = m.Title,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                IsChot = m.IsChot,
                TienThuaThangTruoc = m.TienThuaThangTruoc,
                TongChi = _db.DsChiTieu.Where(n => n.ChotSoId == m.Id).ToList().Sum(n => n.SoTien),
                TongThu = _db.DsNopTruoc.Where(n => n.ChotSoId == m.Id).ToList().Sum(n => n.SoTien),
                TienGao = m.TienGao
            }).ToList());
        }

        public async Task<ActionResult> CreateOrEdit(int id = 0)
        {
            DotChotSo dsDotChotSo = await _db.DotChotSo.FindAsync(id);
            if (dsDotChotSo == null)
            {
                dsDotChotSo = new DotChotSo
                {
                    StartDate = DateTime.Now,
                    IsChot = false,
                    CreateBy = CurrentUser().Id
                };
            }
            return View(dsDotChotSo);
        }

        // POST: /DsDotChotSo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit([Bind(Include = "Id,Title,IsChot,StartDate,CreateBy,TienThuaThangTruoc")] DotChotSo dotChotSo)
        {
            if (ModelState.IsValid)
            {
                dotChotSo.Title = Common.Encode(dotChotSo.Title);
                dotChotSo.CreateBy = User.Identity.GetUserId();
                dotChotSo.StartDate = DateTime.Now;
                dotChotSo.IsChot = false;
                dotChotSo.TienThuaThangTruoc = dotChotSo.TienThuaThangTruoc;

                if (dotChotSo.Id > 0)
                {
                    _db.Entry(dotChotSo).State = EntityState.Modified;
                }
                else
                {
                    _db.DotChotSo.Add(dotChotSo);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dotChotSo);
        }

        // GET: /DotChotSo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DotChotSo dotchotso = await _db.DotChotSo.FindAsync(id);
            if (dotchotso == null)
            {
                return HttpNotFound();
            }

            _db.DotChotSo.Remove(dotchotso);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ChotSo(int? id)
        {
            DotChotSo dotchotso = await _db.DotChotSo.FindAsync(id);
            if (dotchotso == null || dotchotso.IsChot)
            {
                return HttpNotFound();
            }

            var tongThu = await _db.DsNopTruoc.Where(m => m.ChotSoId == id).Select(m => m.SoTien).SumAsync();
            var tongChi = await _db.DsChiTieu.Where(m => m.ChotSoId == id).Select(m => m.SoTien).SumAsync();
            dotchotso.TongThu = tongThu;
            dotchotso.TongChi = tongChi;
            dotchotso.UpdateBy = CurrentUser().Id;
            dotchotso.IsChot = true;
            dotchotso.EndDate = DateTime.Now;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "ThongKe", new { dotChotId = id });
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
