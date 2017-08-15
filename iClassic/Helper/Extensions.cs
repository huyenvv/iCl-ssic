using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Models;

namespace iClassic.Helper
{
    public static class Extensions
    {
        public static EmployeeEditModel ToModel(this ApplicationUser model)
        {
            return new EmployeeEditModel
            {
                Id = model.Id,
                BranchId = model.BranchId,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                IsActive = model.IsActive
            };
        }
    }
}