using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Services.Implementation
{
    public class BranchRepository : GenericRepository<Branch>, IDisposable
    {
        private iClassicEntities context;
        public BranchRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
        }

        public Branch GetById(int Id)
        {
            return FirstOrDefault(m => m.Id == Id);
        }
    }
}