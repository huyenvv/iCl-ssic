using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class InvoiceMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Mã hóa đơn")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tổng tiền")]
        public double Total { get; set; }

        [Display(Name = "Đặt cọc")]
        public double DatCoc { get; set; }

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

        [Display(Name = "Chiết khấu")]
        public double ChietKhau { get; set; }
    }
}