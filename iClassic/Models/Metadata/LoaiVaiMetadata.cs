using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class LoaiVaiMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tên loại vải")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Mã vải")]
        public string MaVai { get; set; }

        [Display(Name = "Số tiền")]
        public double SoTien { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
    }
}