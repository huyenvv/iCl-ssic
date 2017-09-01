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
    public class CustomerRepository : GenericRepository<Customer>, IDisposable
    {
        private iClassicEntities context;
        public CustomerRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Customer GetById(int Id)
        {
            return FirstOrDefault(m => m.Id == Id);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Customer> GetByBranchId(int branchId)
        {
            return Where(m => m.BranchId == branchId);
        }

        public IQueryable<Customer> Search(CustomerSearch model)
        {
            var list = GetAll();

            if (model.BranchId > 0)
            {
                list = list.Where(m => m.BranchId == model.BranchId);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m =>
                        m.MaKH.ToUpper().Contains(model.SearchText) ||
                        m.TenKH.ToUpper().Contains(model.SearchText) ||
                        m.SDT.ToUpper().Contains(model.SearchText) ||
                        m.Address.ToString().ToUpper().Contains(model.SearchText)
                        );
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "MAKH":
                        list = list.OrderBy(m => m.MaKH);
                        break;
                    case "TENKH":
                        list = list.OrderBy(m => m.TenKH);
                        break;
                    case "SDT":
                        list = list.OrderBy(m => m.SDT);
                        break;
                    case "ADDRESS":
                        list = list.OrderBy(m => m.Address);
                        break;
                    //case "SODO":
                    //    list = list.OrderBy(m => m.SoDo);
                    //    break;
                    case "CREATEDATE":
                        list = list.OrderBy(m => m.Created);
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
                    case "MAKH":
                        list = list.OrderByDescending(m => m.MaKH);
                        break;
                    case "TENKH":
                        list = list.OrderByDescending(m => m.TenKH);
                        break;
                    case "SDT":
                        list = list.OrderByDescending(m => m.SDT);
                        break;
                    case "ADDRESS":
                        list = list.OrderByDescending(m => m.Address);
                        break;
                    //case "SODO":
                    //    list = list.OrderByDescending(m => m.SoDo);
                    //    break;
                    case "CREATEDATE":
                        list = list.OrderByDescending(m => m.Created);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }

        public override void Insert(Customer model)
        {
            model.Created = DateTime.Now;
            base.Insert(model);
        }

        public override void Update(Customer model)
        {
            var obj = GetById(model.Id);
            obj.TenKH = model.TenKH;
            obj.SDT = model.SDT;
            obj.Address = model.Address;
            
            if (!string.IsNullOrWhiteSpace(model.Image))
            {
                obj.Image = model.Image;
            }
            obj.Note = model.Note;
            obj.BranchId = model.BranchId;
            obj.DangNguoi = model.DangNguoi;
            obj.KenhQC = model.KenhQC;

            var listNew = model.ProductTypeValue.Where(m => !obj.ProductTypeValue.Any(n => n.ProductTypeFieldId == m.ProductTypeFieldId));
            var lisUpdate = model.ProductTypeValue.Where(m => obj.ProductTypeValue.Any(n => n.ProductTypeFieldId == m.ProductTypeFieldId));
            var listRemove = obj.ProductTypeValue.Where(m => !model.ProductTypeValue.Any(n => n.ProductTypeFieldId == m.ProductTypeFieldId));

            listNew.ToList().ForEach(t => obj.ProductTypeValue.Add(t));
            lisUpdate.ToList().ForEach(t => {
                var objForUpdate = obj.ProductTypeValue.FirstOrDefault(m => m.ProductTypeFieldId == t.ProductTypeFieldId);
                objForUpdate.Value = t.Value;
            });
            listRemove.ToList().ForEach(t => obj.ProductTypeValue.Remove(t));
            base.Update(obj);
        }
    }
}