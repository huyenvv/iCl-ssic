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
    public class MemberCardRepository : GenericRepository<MemberCard>, IDisposable
    {
        private iClassicEntities context;
        public MemberCardRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public MemberCard GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<MemberCard> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<MemberCard> Search(MemberCardSearch model)
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
                    default:
                        list = list.OrderByDescending(m => m.Id);
                        break;
                }
            }
            return list;
        }       

        public override void Update(MemberCard model)
        {
            var obj = GetById(model.Id);
            obj.BirthDayDiscount = model.BirthDayDiscount;
            obj.Name = model.Name;
            obj.NguoiThanDiscount = model.NguoiThanDiscount;
            obj.Note = model.Note;

            var listNew = model.ProductMemberCard.Where(m => !obj.ProductMemberCard.Any(n => n.ProductId == m.ProductId));
            var lisUpdate = model.ProductMemberCard.Where(m => obj.ProductMemberCard.Any(n => n.ProductId == m.ProductId));
            var listRemove = obj.ProductMemberCard.Where(m => !model.ProductMemberCard.Any(n => n.ProductId == m.ProductId));

            listNew.ToList().ForEach(t => obj.ProductMemberCard.Add(t));
            lisUpdate.ToList().ForEach(t => {
                var objForUpdate = obj.ProductMemberCard.FirstOrDefault(m => m.ProductId == t.ProductId);
                objForUpdate.Discount = t.Discount;
            });
            listRemove.ToList().ForEach(t => obj.ProductMemberCard.Remove(t));

            base.Update(obj);
        }
    }
}