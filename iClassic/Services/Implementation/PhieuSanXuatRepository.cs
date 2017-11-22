using iClassic.Models;
using iClassic.Services.Base;
using System;

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

        public void ChangeStatusMuaVai(int id, bool status)
        {
            var obj = GetById(id);
            obj.HasVai = status;
            base.Update(obj);
        }
    }
}