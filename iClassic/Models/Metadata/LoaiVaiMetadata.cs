using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class LoaiVaiMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Vải loại nào?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Mã vải")]
        public string MaVai { get; set; }

        [Display(Name = "Giá mua vào")]
        public Nullable<double> SoTienNhapVao { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Tính tiền lên sản phẩm")]
        public virtual ICollection<ProductTypeLoaiVai> ProductTypeLoaiVai { get; set; }
    }
}