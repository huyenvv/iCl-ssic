using System.ComponentModel.DataAnnotations;

namespace iClassic.Models.Metadata
{
    public class ThoMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tên thợ")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Chuyên môn")]
        public string Type { get; set; }
    }
}