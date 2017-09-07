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
            var list = Where(m => m.Invoice.BranchId == model.BranchId);

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.TenSanPham.ToUpper().Contains(model.SearchText) ||                        
                        m.LoaiVai.MaVai.ToUpper().Contains(model.SearchText) ||
                        m.LoaiVai.Name.ToUpper().Contains(model.SearchText));
            }

            if (model.Status.HasValue)
            {
                list = list.Where(m => m.Status == model.Status);
            }            

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {                    
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
                    default:
                        list = list.OrderBy(m => m.Id);
                        break;
                }
            }
            else
            {
                switch (sortNameUpper)
                {
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
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }       
        

        public void ChangeStatusMuaVai(int id, bool status)
        {
            var obj = GetById(id);
            obj.HasVai = status;
            base.Update(obj);
        }
    }
}