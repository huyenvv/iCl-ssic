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
        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }

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
        [Display(Name = "Ngày lấy")]
        public System.DateTime NgayLay { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Khách hàng")]
        public int KhachHangId { get; set; }
    }
}