using System.ComponentModel.DataAnnotations;

namespace iClassic.Models.Metadata
{
    public class PhieuSanXuatMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Loại vải")]
        public int MaVaiId { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tiền công")]
        public double TienCong { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Đặt cọc")]
        public double DatCoc { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Ngày thử")]
        public System.DateTime NgayThu { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Ngày trả")]
        public System.DateTime NgayTra { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Khách hàng")]
        public int CustomerId { get; set; }
    }
}