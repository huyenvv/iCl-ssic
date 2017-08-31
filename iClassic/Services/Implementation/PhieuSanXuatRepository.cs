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
    public class PhieuSanXuatRepository : GenericRepository<PhieuSanXuat>, IDisposable
    {
        private iClassicEntities context;
        public PhieuSanXuatRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public PhieuSanXuat GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<PhieuSanXuat> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<PhieuSanXuat> Search(PhieuSanXuatSearch model)
        {
            var list = Where(m => m.BranchId == model.BranchId);

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.TenSanPham.ToUpper().Contains(model.SearchText) ||
                        m.Customer.TenKH.ToUpper().Contains(model.SearchText) ||
                        m.Customer.SDT.ToUpper().Contains(model.SearchText) ||
                        m.LoaiVai.MaVai.ToUpper().Contains(model.SearchText) ||
                        m.LoaiVai.Name.ToUpper().Contains(model.SearchText));
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
                    case "TIENCONG":
                        list = list.OrderBy(m => m.TienCong);
                        break;
                    case "DATCOC":
                        list = list.OrderBy(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderBy(m => m.Customer.TenKH);
                        break;
                    case "MAVAIID":
                        list = list.OrderBy(m => m.LoaiVai.Name);
                        break;
                    case "SOLUONG":
                        list = list.OrderBy(m => m.SoLuong);
                        break;
                    case "TENSANPHAM":
                        list = list.OrderBy(m => m.TenSanPham);
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
                    case "TIENCONG":
                        list = list.OrderByDescending(m => m.TienCong);
                        break;
                    case "DATCOC":
                        list = list.OrderByDescending(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderByDescending(m => m.Customer.TenKH);
                        break;
                    case "MAVAIID":
                        list = list.OrderByDescending(m => m.LoaiVai.Name);
                        break;
                    case "SOLUONG":
                        list = list.OrderByDescending(m => m.SoLuong);
                        break;
                    case "TENSANPHAM":
                        list = list.OrderByDescending(m => m.TenSanPham);
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

        public IQueryable<PhieuSanXuat> GetByDateRange(int branchId, DateTime? startDate, DateTime? endDate)
        {
            var statusDone = (byte)TicketStatus.DaTraChoKhach;
            return Where(m => m.BranchId == branchId && m.Status == statusDone && (!startDate.HasValue || startDate <= m.NgayTra) && (!endDate.HasValue || m.NgayTra <= endDate));
        }

        public int Count(int branchId, TicketStatus status)
        {
            var stt = (byte)status;
            return Where(m => m.BranchId == branchId && m.Status == stt).Count();
        }

        public override void Insert(PhieuSanXuat model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(PhieuSanXuat model)
        {
            var obj = GetById(model.Id);
            obj.TienCong = model.TienCong;
            obj.DatCoc = model.DatCoc;
            obj.CustomerId = model.CustomerId;
            obj.MaVaiId = model.MaVaiId;
            obj.NgayThu = model.NgayThu;
            obj.SoLuong = model.SoLuong;
            obj.TenSanPham = model.TenSanPham;
            obj.Status = model.Status;
            obj.NgayTra = model.NgayTra;
            obj.BranchId = model.BranchId;
            base.Update(obj);
        }
    }
}