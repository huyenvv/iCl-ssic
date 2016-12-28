using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalcMean.Models
{
    [Table("TienGao")]
    public class TienGao
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal SoTien { get; set; }
        [Required]
        public int ChotSoId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("ChotSoId")]
        public virtual DotChotSo DotChotSo { get; set; }
    }
}
