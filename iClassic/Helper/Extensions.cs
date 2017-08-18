using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Models;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;

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

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return string.Empty;
        }
    }
}