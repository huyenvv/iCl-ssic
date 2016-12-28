using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalcMean.Models
{
    [Table("DsChiTieu")]
    public class DsChiTieu
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public decimal SoTien { get; set; }
        public string UserTieuId { get; set; }
        [Required]
        public int ChotSoId { get; set; }
        [Required]
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("ChotSoId")]
        public virtual DotChotSo DotChotSo { get; set; }
        public virtual ICollection<UserInChiTieu> UserInChiTieu { get; set; }
    }
}
