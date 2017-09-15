using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class CustomerSearch : SortPagingBase
    {
        public int BranchId { get; set; }

        public int Group { get; set; }
    }
}