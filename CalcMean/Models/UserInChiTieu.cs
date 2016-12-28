using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalcMean.Models
{
    [Table("UserInChiTieu")]
    public class UserInChiTieu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal SoTien { get; set; }
        [Required]
        public string ForUserId { get; set; }
        [Required]
        public int ChiTieuId { get; set; }

        [ForeignKey("ChiTieuId")]
        public virtual DsChiTieu DsChiTieu { get; set; }
        [ForeignKey("ForUserId")]
        public virtual CmUser CmUser { get; set; }
    }
}
