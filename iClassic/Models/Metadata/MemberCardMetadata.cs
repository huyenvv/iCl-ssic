using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iClassic.Models.Metadata
{
    public class MemberCardMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Tên hạng thẻ")]
        public string Name { get; set; }        
        
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Giảm giá vào ngày sinh nhật")]
        public int BirthDayDiscount { get; set; }

        [Display(Name = "Giảm giá cho người thân đầu tiên")]
        public int NguoiThanDiscount { get; set; }

        [Display(Name = "Giảm giá trong các sản phẩm may")]
        public virtual ICollection<ProductMemberCard> ProductMemberCard { get; set; }
    }
}