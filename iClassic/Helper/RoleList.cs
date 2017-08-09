using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public static class RoleList
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";

        public static List<string> GetAll()
        {
            return new List<string>{
                Admin, Employee
            };
        }
    }
}