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
    public class PhieuSuaRepository : GenericRepository<PhieuSua>, IDisposable
    {
        private iClassicEntities context;
        public PhieuSuaRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public PhieuSua GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<PhieuSua> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<PhieuSua> Search(PhieuSuaSearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.NoiDung.ToUpper().Contains(model.SearchText) ||
                        m.Customer.TenKH.ToUpper().Contains(model.SearchText) ||
                        m.Customer.SDT.ToUpper().Contains(model.SearchText));
            }

            if (model.Status.HasValue)
            {
                list = list.Where(m => m.Status == model.Status);
            }

            if (model.FromDate.HasValue)
            {
                list = list.Where(m => model.FromDate <= m.NgayNhan ||
                                       model.FromDate <= m.NgayTra
                );
            }
            if (model.ToDate.HasValue)
            {
                list = list.Where(m => m.NgayNhan <= model.ToDate ||
                                       m.NgayTra <= model.ToDate
                );
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "STATUS":
                        list = list.OrderBy(m => m.Status);
                        break;
                    case "KHACHHANGID":
                        list = list.OrderBy(m => m.Customer.TenKH);
                        break;
                    case "NGAYNHAN":
                        list = list.OrderBy(m => m.NgayNhan);
                        break;
                    case "NGAYTRA":
                        list = list.OrderBy(m => m.NgayTra);
                        break;
                    case "NOIDUNG":
                        list = list.OrderBy(m => m.NoiDung);
                        break;
                    case "CREATED":
                        list = list.OrderBy(m => m.Created);
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
                    case "STATUS":
                        list = list.OrderByDescending(m => m.Status);
                        break;
                    case "KHACHHANGID":
                        list = list.OrderByDescending(m => m.Customer.TenKH);
                        break;
                    case "NGAYNHAN":
                        list = list.OrderByDescending(m => m.NgayNhan);
                        break;
                    case "NGAYTRA":
                        list = list.OrderByDescending(m => m.NgayTra);
                        break;
                    case "NOIDUNG":
                        list = list.OrderByDescending(m => m.NoiDung);
                        break;
                    case "CREATED":
                        list = list.OrderByDescending(m => m.Created);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Created);
                        break;
                }
            }
            return list;
        }

        public IQueryable<PhieuSua> GetByDateRange(int branchId, DateTime? startDate, DateTime? endDate)
        {
            var statusDone = (byte)TicketStatus.DaTraChoKhach;
            return Where(m => m.Customer.BranchId== branchId && m.Status == statusDone && (!startDate.HasValue || startDate <= m.NgayTra) && (!endDate.HasValue || m.NgayTra <= endDate));
        }        

        public int Count(int branchId, TicketStatus status)
        {
            var stt = (byte)status;
            return Where(m => m.Customer.BranchId == branchId && m.Status == stt).Count();
        }

        public override void Insert(PhieuSua model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(PhieuSua model)
        {
            var obj = GetById(model.Id);
            obj.KhachHangId = model.KhachHangId;
            obj.SoTien = model.SoTien;
            obj.NgayNhan = model.NgayNhan;
            obj.NgayTra = model.NgayTra;
            obj.NoiDung = model.NoiDung;
            obj.Status = model.Status;
            base.Update(obj);
        }
    }
}