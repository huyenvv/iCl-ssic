using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class CustomerMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Mã khách hàng")]
        public string MaKH { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập \"Tên khách hàng\"")]
        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập \"Số điện thoại\"")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Chi nhánh")]
        public string BranchId { get; set; }

        [Display(Name = "Số đo")]
        public string SoDo { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }        
    }
}