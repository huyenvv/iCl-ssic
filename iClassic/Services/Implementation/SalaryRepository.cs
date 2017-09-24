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
    public class SalaryRepository : GenericRepository<Salary>, IDisposable
    {
        private iClassicEntities context;
        public SalaryRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Salary GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<Salary> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Salary> Search(SalarySearch model)
        {
            var list = GetAll();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.WorkerId.HasValue && m.Tho.Name.Contains(model.SearchText) ||
                        m.AspNetUsers!=null && (
                        m.AspNetUsers.Name.Contains(model.SearchText) ||
                        m.AspNetUsers.UserName.Contains(model.SearchText)
                        ) ||
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
                    //case "NAME":
                    //    list = list.OrderBy(m => m.Name);
                    //    break;
                    //case "TYPE":
                    //    list = list.OrderBy(m => m.Type);
                    //    break;
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
                    //case "NAME":
                    //    list = list.OrderByDescending(m => m.Name);
                    //    break;
                    //case "TYPE":
                    //    list = list.OrderByDescending(m => m.Type);
                    //    break;
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }

        public override void Update(Salary model)
        {
            var obj = GetById(model.Id);
            obj.FromDate = model.FromDate;
            obj.ToDate = model.ToDate;
            obj.Note = model.Note;
            obj.TotalSalary = model.TotalSalary;
            obj.WorkerId = model.WorkerId;
            obj.EmployeeId = model.EmployeeId;
            base.Update(obj);
        }
    }
}