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
    public class ThongKeController : BaseController
    {
        private readonly CmContext _db = new CmContext();
        // GET: /ThongKe/
        public async Task<ActionResult> Index(int chotSoId = 0)
        {
            DotChotSo dotchotso = _db.DotChotSo.Find(chotSoId) ?? _db.DotChotSo.OrderByDescending(m => m.Id).FirstOrDefault();
            if (dotchotso == null)
            {
                return HttpNotFound();
            }

            var dsDaNop = _db.DsNopTruoc.Where(m => m.ChotSoId == dotchotso.Id).ToList();
            var dsUserInChiTieu = _db.UserInChiTieu.Where(m => m.DsChiTieu != null && m.DsChiTieu.ChotSoId == dotchotso.Id).ToList();
            var tongSoLuotAn = dsUserInChiTieu.Count;
            var tongSoTienGao = _db.TienGao.Where(m => m.ChotSoId == dotchotso.Id).ToList().Sum(m => m.SoTien);
            var tbtienGao1LuotAn = tongSoLuotAn > 0 ? tongSoTienGao / tongSoLuotAn : 0;
            var dsNguoiAn = _db.Users.Where(m => m.IsShow).ToList();
            var tienThuaThangTruoc1Ng = (dotchotso.TienThuaThangTruoc ?? 0) / dsNguoiAn.Count;
            var listReturn = dsNguoiAn.OrderBy(m => m.Name).ToList().Select(m => new MyStatistic
            {
                User = m,
                TongDaAn = dsUserInChiTieu.Where(n => n.ForUserId == m.Id).Select(n => n.SoTien).Sum(),
                TongDaNop = dsDaNop.Where(n => n.NguoiNopId == m.Id).Select(n => n.SoTien).Sum(),
                TienGao = tbtienGao1LuotAn * dsUserInChiTieu.Count(n => n.ForUserId == m.Id),
                TienThuaThangTruoc = tienThuaThangTruoc1Ng
            }).ToList();

            var tongthu = dsDaNop.Sum(m => m.SoTien);
            var tongchichuagao = _db.DsChiTieu.Where(m => m.ChotSoId == dotchotso.Id).ToList().Sum(m => m.SoTien);

            // Selection Đợt chốt sổ
            CreateViewBagDotChotSo(dotchotso.Id);
            return View(new ThongKe
            {
                DotChot = dotchotso,
                TongThu = tongthu,
                TongChiChuaGao = tongchichuagao,
                TongTienGao = tongSoTienGao,
                TongChi = tongchichuagao + tongSoTienGao,
                TienThuaThangTruoc = dotchotso.TienThuaThangTruoc ?? 0,
                List = listReturn
            });
        }
        public async Task<ActionResult> MyStatistic(int dotChotId = 0)
        {
            var currentUserId = CurrentUser().Id;
            var dotchotso = await _db.DotChotSo.FindAsync(dotChotId);
            var daNop = await _db.DsNopTruoc.Where(m => m.ChotSoId == dotChotId && m.NguoiNopId == currentUserId).Select(m => m.SoTien).SumAsync();

            var daAn = _db.UserInChiTieu.Where(
                            m => m.ForUserId == currentUserId && m.DsChiTieu != null && m.DsChiTieu.ChotSoId == dotChotId)
                        .Select(m => (decimal?)m.SoTien)
                        .Sum() ?? 0;

            var my = new MyStatistic
            {
                User = CurrentUser(),
                DotChot = dotchotso,
                TongDaAn = daAn,
                TongDaNop = daNop
            };
            return View(my);
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

