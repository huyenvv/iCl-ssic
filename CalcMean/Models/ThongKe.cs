using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalcMean.Models
{
    public class MyStatistic
    {
        public CmUser User { get; set; }
        public decimal TongDaNop { get; set; }
        public decimal TongDaAn { get; set; }
        public decimal TienGao { get; set; }
        public decimal TienThuaThangTruoc { get; set; }
        public DotChotSo DotChot { get; set; }
    }

    public class ThongKe
    {
        public List<MyStatistic> List { get; set; }

        public DotChotSo DotChot { get; set; }

        public decimal TongThu { get; set; }
        public decimal TongChiChuaGao { get; set; }
        public decimal TongTienGao { get; set; }
        public decimal TongChi { get; set; }
        public decimal TienThuaThangTruoc { get; set; }
    }
}


