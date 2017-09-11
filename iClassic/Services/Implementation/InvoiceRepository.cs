using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using iClassic.Helper;

namespace iClassic.Services.Implementation
{
    public class InvoiceRepository : GenericRepository<Invoice>, IDisposable
    {
        private iClassicEntities context;
        public InvoiceRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Invoice GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Invoice> Search(InvoiceSearch model)
        {
            var list = Where(m => m.BranchId == model.BranchId);

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m =>
                        m.Customer.TenKH.ToUpper().Contains(model.SearchText) ||
                        m.Customer.SDT.ToUpper().Contains(model.SearchText) ||
                        m.Code.ToUpper().Contains(model.SearchText));
            }

            if (model.Status.HasValue)
            {
                list = list.Where(m => m.Status == model.Status);
            }

            if (model.FromDate.HasValue)
            {
                list = list.Where(m => model.FromDate <= m.NgayThu ||
                                       model.FromDate <= m.NgayTra
                );
            }
            if (model.ToDate.HasValue)
            {
                list = list.Where(m => m.NgayThu <= model.ToDate ||
                                       m.NgayTra <= model.ToDate
                );
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "TOTAL":
                        list = list.OrderBy(m => m.Total);
                        break;
                    case "DATCOC":
                        list = list.OrderBy(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderBy(m => m.Customer.TenKH);
                        break;
                    case "STATUS":
                        list = list.OrderBy(m => m.Status);
                        break;
                    case "NGAYTHU":
                        list = list.OrderBy(m => m.NgayThu);
                        break;
                    case "NGAYTRA":
                        list = list.OrderBy(m => m.NgayTra);
                        break;
                    default:
                        list = list.OrderBy(m => m.Created);
                        break;
                }
            }
            else
            {
                switch (sortNameUpper)
                {
                    case "TOTAL":
                        list = list.OrderByDescending(m => m.Total);
                        break;
                    case "DATCOC":
                        list = list.OrderByDescending(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderByDescending(m => m.Customer.TenKH);
                        break;
                    case "STATUS":
                        list = list.OrderByDescending(m => m.Status);
                        break;
                    case "NGAYTHU":
                        list = list.OrderByDescending(m => m.NgayThu);
                        break;
                    case "NGAYTRA":
                        list = list.OrderByDescending(m => m.NgayTra);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Created);
                        break;
                }
            }
            return list;
        }

        public IQueryable<Invoice> GetByDateRange(int branchId, DateTime? startDate, DateTime? endDate)
        {
            var statusDone = (byte)TicketStatus.DaTraChoKhach;
            return Where(m => m.BranchId == branchId && m.Status == statusDone && (!startDate.HasValue || startDate <= m.NgayTra) && (!endDate.HasValue || m.NgayTra <= endDate));
        }        

        public int Count(int branchId, TicketStatus status)
        {
            var stt = (byte)status;
            return Where(m => m.BranchId == branchId && m.Status == stt).Count();
        }

        public override void Insert(Invoice model)
        {
            model.Created = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreateBy;

            model.PhieuSanXuat.ToList().ForEach(t =>
            {
                var objForUpdate = model.PhieuSanXuat.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.HasVai = false;
                objForUpdate.Status = (int)TicketStatus.ChuaXuLy;
                objForUpdate.DonGia = t.MaVaiId.HasValue
                    ? context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == t.MaVaiId && m.ProductTypeId == t.ProductTypeId).Price ?? 0
                    : context.ProductType.FirstOrDefault(m => m.Id == t.ProductTypeId).Price;
            });
            model.PhieuSua.ToList().ForEach(t =>
            {
                var objForUpdate = model.PhieuSua.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.Status = (int)TicketStatus.ChuaXuLy;
                objForUpdate.SoTien = t.Type == (byte)PhieuSuaType.BaoHanh ? 0 : t.SoTien;
            }
            );
            base.Insert(model);
        }

        public override void Update(Invoice model)
        {
            var obj = GetById(model.Id);
            obj.Total = model.Total;
            obj.DatCoc = model.DatCoc;
            obj.CustomerId = model.CustomerId;
            obj.NgayThu = model.NgayThu;
            obj.ChietKhau = model.ChietKhau;
            model.ModifiedBy = model.ModifiedBy;
            model.ModifiedDate = DateTime.Now;
            obj.Status = model.Status;
            obj.NgayTra = model.NgayTra;
            obj.BranchId = model.BranchId;

            #region Phiếu sản xuất
            var listNew = model.PhieuSanXuat.Where(m => !obj.PhieuSanXuat.Any(n => n.Id == m.Id));
            var lisUpdate = model.PhieuSanXuat.Where(m => obj.PhieuSanXuat.Any(n => n.Id == m.Id));
            var listRemove = obj.PhieuSanXuat.Where(m => !model.PhieuSanXuat.Any(n => n.Id == m.Id));

            listNew.ToList().ForEach(t =>
            {
                t.DonGia = t.MaVaiId.HasValue
                ? context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == t.MaVaiId && m.ProductTypeId == t.ProductTypeId).Price ?? 0
                : context.ProductType.FirstOrDefault(m => m.Id == t.ProductTypeId).Price;
                obj.PhieuSanXuat.Add(t);
            }
            );
            lisUpdate.ToList().ForEach(t =>
            {
                var objForUpdate = obj.PhieuSanXuat.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.MaVaiId = t.MaVaiId;
                objForUpdate.ProductTypeId = t.ProductTypeId;
                objForUpdate.SoLuong = t.SoLuong;
                objForUpdate.TenSanPham = t.TenSanPham;
                objForUpdate.Status = t.Status;
                objForUpdate.ThoDoId = t.ThoDoId;
                objForUpdate.ThoCatId = t.ThoCatId;
                objForUpdate.ThoMayId = t.ThoMayId;                
                objForUpdate.DonGia = t.MaVaiId.HasValue
                    ? context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == t.MaVaiId && m.ProductTypeId == t.ProductTypeId).Price ?? 0
                    : context.ProductType.FirstOrDefault(m => m.Id == t.ProductTypeId).Price;
            });
            listRemove.ToList().ForEach(t => obj.PhieuSanXuat.Remove(t));
            #endregion

            #region Phiếu Sửa
            var listNewPhieusua = model.PhieuSua.Where(m => !obj.PhieuSua.Any(n => n.Id == m.Id));
            var lisUpdatePhieusua = model.PhieuSua.Where(m => obj.PhieuSua.Any(n => n.Id == m.Id));
            var listRemovePhieusua = obj.PhieuSua.Where(m => !model.PhieuSua.Any(n => n.Id == m.Id));

            listNewPhieusua.ToList().ForEach(t =>
                {
                    t.SoTien = t.Type == (byte)PhieuSuaType.BaoHanh ? 0 : t.SoTien;
                    obj.PhieuSua.Add(t);
                }
            );
            lisUpdatePhieusua.ToList().ForEach(t =>
            {
                var objForUpdate = obj.PhieuSua.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.NoiDung = t.NoiDung;
                objForUpdate.ProblemType = t.ProblemType;
                objForUpdate.ProblemTypeOther = !string.IsNullOrEmpty(t.ProblemTypeOther) ? t.ProblemTypeOther : string.Empty;
                objForUpdate.Type = t.Type;
                objForUpdate.Status = t.Status;
                objForUpdate.ThoId = t.ThoId;
                objForUpdate.SoTien = t.Type == (byte)PhieuSuaType.BaoHanh ? 0 : t.SoTien;
            });
            listRemovePhieusua.ToList().ForEach(t => obj.PhieuSua.Remove(t));
            #endregion

            base.Update(obj);
        }
    }
}