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
            var list = GetAll();

            //if (!string.IsNullOrWhiteSpace(model.SearchText))
            //{
            //    model.SearchText = model.SearchText.ToUpper();
            //    list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||
            //            m.Address.ToUpper().Contains(model.SearchText) ||
            //            m.Id.ToString().ToUpper().Contains(model.SearchText) ||
            //            m.SDT.ToUpper().Contains(model.SearchText));
            //}

            //var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            //if (model.SortOrder == SortDirection.Ascending)
            //{
            //    switch (sortNameUpper)
            //    {
            //        case "ID":
            //            list = list.OrderBy(m => m.Id);
            //            break;
            //        case "ADDRESS":
            //            list = list.OrderBy(m => m.Address);
            //            break;
            //        case "NAME":
            //            list = list.OrderBy(m => m.Name);
            //            break;
            //        case "SDT":
            //            list = list.OrderBy(m => m.Address);
            //            break;
            //        default:
            //            list = list.OrderBy(m => m.Created);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (sortNameUpper)
            //    {
            //        case "ID":
            //            list = list.OrderByDescending(m => m.Id);
            //            break;
            //        case "ADDRESS":
            //            list = list.OrderByDescending(m => m.Address);
            //            break;
            //        case "NAME":
            //            list = list.OrderByDescending(m => m.Name);
            //            break;
            //        case "SDT":
            //            list = list.OrderByDescending(m => m.Address);
            //            break;
            //        default:
            //            list = list.OrderByDescending(m => m.Created);
            //            break;
            //    }
            //}
            return list;
        }

        public override void Insert(PhieuSanXuat model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(PhieuSanXuat model)
        {
            var obj = GetById(model.Id);
            obj.DonGia = model.DonGia;
            obj.DatCoc = model.DatCoc;
            obj.KhachHangId = model.KhachHangId;
            obj.MaVaiId = model.MaVaiId;
            obj.NgayThu = model.NgayThu;
            obj.SoLuong = model.SoLuong;
            obj.TenSanPham = model.TenSanPham;
            obj.Status = model.Status;
            obj.NgayLay = model.NgayLay;
            base.Update(obj);
        }
    }
}