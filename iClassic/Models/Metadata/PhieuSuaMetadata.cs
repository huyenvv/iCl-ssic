using System.ComponentModel.DataAnnotations;

namespace iClassic.Models.Metadata
{
    public class PhieuSuaMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }       

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}