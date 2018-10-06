using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Models;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;
using System.Text;

namespace iClassic.Helper
{
    public static class Extensions
    {
        public static EmployeeEditModel ToModel(this ApplicationUser model, string role)
        {
            return new EmployeeEditModel
            {
                Id = model.Id,
                BranchId = model.BranchId,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                IsActive = model.IsActive,
                Role = role
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

        public static string ToCsv<T>(this IEnumerable<T> items)
                     where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            string titleLine = string.Join(",", properties.Select(m => m.Name).ToArray());
            csvBuilder.AppendLine(titleLine);
            foreach (T item in items)
            {
                string line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }
    }
}