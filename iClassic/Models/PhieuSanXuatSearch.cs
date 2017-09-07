using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class PhieuSanXuatSearch : SortPagingBase
    {
        public int BranchId { get; set; }
        public int? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}