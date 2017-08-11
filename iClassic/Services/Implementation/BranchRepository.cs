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
    public class BranchRepository : GenericRepository<Branch>, IDisposable
    {
        private iClassicEntities context;
        public BranchRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Branch GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Branch> Search(BranchSearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||
                        m.Address.ToUpper().Contains(model.SearchText) ||
                        m.Id.ToString().ToUpper().Contains(model.SearchText) ||
                        m.SDT.ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "ID":
                        list = list.OrderBy(m => m.Id);
                        break;
                    case "ADDRESS":
                        list = list.OrderBy(m => m.Address);
                        break;
                    case "NAME":
                        list = list.OrderBy(m => m.Name);
                        break;
                    case "SDT":
                        list = list.OrderBy(m => m.Address);
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
                    case "ID":
                        list = list.OrderByDescending(m => m.Id);
                        break;
                    case "ADDRESS":
                        list = list.OrderByDescending(m => m.Address);
                        break;
                    case "NAME":
                        list = list.OrderByDescending(m => m.Name);
                        break;
                    case "SDT":
                        list = list.OrderByDescending(m => m.Address);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Created);
                        break;
                }
            }
            return list;
        }

        public override void Insert(Branch model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(Branch model)
        {
            var obj = GetById(model.Id);
            obj.Address = model.Address;
            obj.Name = model.Name;
            obj.SDT = model.SDT;
            base.Update(obj);
        }
    }
}