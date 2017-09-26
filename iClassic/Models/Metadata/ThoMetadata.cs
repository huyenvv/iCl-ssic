using System;
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

        [Display(Name = "Lương một sản phẩm")]
        public Nullable<double> Salary { get; set; }

        [Display(Name = "Lương phần trăm một sản phẩm")]
        public Nullable<int> SalaryPercent { get; set; }
    }
}