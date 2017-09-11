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
    public class ThoRepository : GenericRepository<Tho>, IDisposable
    {
        private iClassicEntities context;
        public ThoRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Tho GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<Tho> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Tho> Search(ThoSearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||                        
                        m.Id.ToString().ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "ID":
                        list = list.OrderBy(m => m.Id);
                        break;                    
                    case "NAME":
                        list = list.OrderBy(m => m.Name);
                        break;
                    case "TYPE":
                        list = list.OrderBy(m => m.Type);
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
                    case "ID":
                        list = list.OrderByDescending(m => m.Id);
                        break;
                    case "NAME":
                        list = list.OrderByDescending(m => m.Name);
                        break;
                    case "TYPE":
                        list = list.OrderByDescending(m => m.Type);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }        

        public override void Update(Tho model)
        {
            var obj = GetById(model.Id);
            obj.Type = model.Type;
            obj.Name = model.Name;
            base.Update(obj);
        }
    }
}