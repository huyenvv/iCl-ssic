using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class BranchMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập \"Tên chi nhánh\"")]
        [Display(Name = "Tên chi nhánh")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập \"Số điện thoại\"")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
    }
}