using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalcMean.Models
{
    [Table("DotChotSo")]
    public class DotChotSo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal? TongThu { get; set; }
        public decimal? TongChi { get; set; }
        public decimal? TienThuaThangTruoc { get; set; }
        public bool IsChot { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        public ICollection<DsChiTieu> DsChiTieu { get; set; }

        public ICollection<TienGao> TienGao { get; set; }
    }
}
