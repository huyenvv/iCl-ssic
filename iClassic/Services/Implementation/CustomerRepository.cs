using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}