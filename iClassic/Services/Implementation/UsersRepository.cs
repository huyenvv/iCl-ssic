using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }    
}