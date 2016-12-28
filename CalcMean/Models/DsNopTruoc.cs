using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalcMean.Models
{
    [Table("DsNopTruoc")]
    public class DsNopTruoc
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal SoTien { get; set; }
        [Required]
        public string NguoiNopId { get; set; }
        [Required]
        public int ChotSoId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("NguoiNopId")]
        public virtual CmUser CmUser { get; set; }
    }
}
