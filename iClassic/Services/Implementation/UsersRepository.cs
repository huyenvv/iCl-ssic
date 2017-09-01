using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using iClassic.Helper;

namespace iClassic.Services.Implementation
{
    public class UsersRepository : GenericRepository<AspNetUsers>, IDisposable
    {
        private iClassicEntities context;
        public UsersRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public AspNetUsers GetById(string Id)
        {
            return FirstOrDefault(m => m.Id == Id);
        }

        public async Task<AspNetUsers> GetByIdAsync(string id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<AspNetUsers> Search(EmployeeSearch model, bool isSupperAdmin)
        {
            var list = Where(m => !m.AspNetRoles.Any(n => n.Name == RoleList.SupperAdmin));
            if (!isSupperAdmin)
            {
                list = Where(m => m.BranchId == model.BranchId && m.AspNetRoles.Any(n => n.Name == RoleList.Employee));
            }

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m => m.Name.ToUpper().Contains(model.SearchText) ||
                        m.UserName.ToUpper().Contains(model.SearchText) ||
                        m.PhoneNumber.ToString().ToUpper().Contains(model.SearchText) ||
                        m.Email.ToUpper().Contains(model.SearchText));
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "NAME":
                        list = list.OrderBy(m => m.Name);
                        break;
                    case "PHONENUMBER":
                        list = list.OrderBy(m => m.PhoneNumber);
                        break;
                    case "EMAIL":
                        list = list.OrderBy(m => m.Email);
                        break;
                    case "USERNAME":
                        list = list.OrderBy(m => m.UserName);
                        break;
                    default:
                        list = list.OrderBy(m => m.UserName);
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
                    case "PHONENUMBER":
                        list = list.OrderByDescending(m => m.PhoneNumber);
                        break;
                    case "EMAIL":
                        list = list.OrderByDescending(m => m.Email);
                        break;
                    case "USERNAME":
                        list = list.OrderByDescending(m => m.UserName);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.UserName);
                        break;
                }
            }
            return list;
        }
    }
}