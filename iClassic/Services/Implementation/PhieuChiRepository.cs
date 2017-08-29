using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace iClassic.Services.Implementation
{
    public class PhieuChiRepository : GenericRepository<PhieuChi>, IDisposable
    {
        private iClassicEntities context;
        public PhieuChiRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public PhieuChi GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<PhieuChi> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<PhieuChi> Search(PhieuChiSearch model)
        {
            var list = Where(m => m.BranchId == model.BranchId);

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.MucChi.ToUpper().Contains(model.SearchText) ||
                        m.NguoiNhanPhieu.ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "MUCCHI":
                        list = list.OrderBy(m => m.Id);
                        break;
                    case "SOTIEN":
                        list = list.OrderBy(m => m.SoTien);
                        break;
                    case "NGUOINHANPHIEU":
                        list = list.OrderBy(m => m.NguoiNhanPhieu);
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
                    case "MUCCHI":
                        list = list.OrderByDescending(m => m.Id);
                        break;
                    case "SOTIEN":
                        list = list.OrderByDescending(m => m.SoTien);
                        break;
                    case "NGUOINHANPHIEU":
                        list = list.OrderByDescending(m => m.NguoiNhanPhieu);
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

        public IQueryable<PhieuChi> GetByDateRange(int branchId, DateTime? startDate, DateTime? endDate)
        {
            return Where(m => m.BranchId == branchId && (!startDate.HasValue || startDate <= m.Created) && (!endDate.HasValue || m.Created <= endDate));
        }

        public override void Insert(PhieuChi model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(PhieuChi model)
        {
            var obj = GetById(model.Id);
            obj.MucChi = model.MucChi;
            obj.SoTien = model.SoTien;
            obj.NguoiNhanPhieu = model.NguoiNhanPhieu;
            obj.BranchId = model.BranchId;
            base.Update(obj);
        }
    }
}