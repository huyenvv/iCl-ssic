using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Helper;

namespace iClassic.Models
{
    public class StatisticSearch : SortPagingBase
    {
        public ReportTypes? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int BranchId { get; set; }
    }
}