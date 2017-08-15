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
    public class LoaiVaiRepository : GenericRepository<LoaiVai>, IDisposable
    {
        private iClassicEntities context;
        public LoaiVaiRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public LoaiVai GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<LoaiVai> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<LoaiVai> Search(LoaiVaiSearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||
                        m.MaVai.ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "NAME":
                        list = list.OrderBy(m => m.Name);
                        break;
                    case "MAVAI":
                        list = list.OrderBy(m => m.MaVai);
                        break;
                    case "SOTIEN":
                        list = list.OrderBy(m => m.SoTien);
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
                    case "NAME":
                        list = list.OrderByDescending(m => m.Name);
                        break;
                    case "MAVAI":
                        list = list.OrderByDescending(m => m.MaVai);
                        break;
                    case "SOTIEN":
                        list = list.OrderByDescending(m => m.SoTien);
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

        public override void Insert(LoaiVai model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(LoaiVai model)
        {
            var obj = GetById(model.Id);
            obj.MaVai = model.MaVai;
            obj.Name = model.Name;
            obj.SoTien = model.SoTien;
            obj.Note = model.Note;
            obj.BranchId = model.BranchId;
            base.Update(obj);
        }
    }
}