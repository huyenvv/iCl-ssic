using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class ReportCustomVip
    {
        public Customer Customer { get; set; }
        public int SoLanSua { get; set; }
        public int SoLanMay { get; set; }
        public int SoSanPhamDaMay { get; set; }
        public double Total { get; set; }
    }
}