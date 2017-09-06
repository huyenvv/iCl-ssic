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
    public class ProductTypeRepository : GenericRepository<ProductType>, IDisposable
    {
        private iClassicEntities context;
        public ProductTypeRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public ProductType GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<ProductType> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<ProductType> Search(ProductTypeSearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||
                        m.Note.ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "ID":
                        list = list.OrderBy(m => m.Id);
                        break;
                    case "PRICE":
                        list = list.OrderBy(m => m.Price);
                        break;
                    case "NAME":
                        list = list.OrderBy(m => m.Name);
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
                    case "PRICE":
                        list = list.OrderByDescending(m => m.Price);
                        break;
                    case "NAME":
                        list = list.OrderByDescending(m => m.Name);
                        break;                    
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }

        public override void Insert(ProductType model)
        {
            base.Insert(model);
        }

        public override void Update(ProductType model)
        {
            var obj = GetById(model.Id);
            obj.Note = model.Note;
            obj.Name = model.Name;
            var listNew = model.ProductTyeField.Where(m => !obj.ProductTyeField.Any(n => n.Id == m.Id));
            var lisUpdate = model.ProductTyeField.Where(m => obj.ProductTyeField.Any(n => n.Id == m.Id));
            var listRemove = obj.ProductTyeField.Where(m => !model.ProductTyeField.Any(n => n.Id == m.Id));

            listNew.ToList().ForEach(t => obj.ProductTyeField.Add(t));
            lisUpdate.ToList().ForEach(t => {
                var objForUpdate = obj.ProductTyeField.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.Name = t.Name;
                objForUpdate.Note = t.Note;
            });
            listRemove.ToList().ForEach(t => obj.ProductTyeField.Remove(t));

            base.Update(obj);
        }
    }
}