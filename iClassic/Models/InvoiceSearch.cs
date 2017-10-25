using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class InvoiceSearch : SortPagingBase
    {
        public int BranchId { get; set; }
        public int? Status { get; set; }
        public bool? StatusVai { get; set; }
        public bool? IsSapPhaiTra { get; set; }
        public bool? IsDenHanThu { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}