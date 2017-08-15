using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class EmployeeSearch : SortPagingBase
    {
        public int BranchId { get; set; }
    }
}