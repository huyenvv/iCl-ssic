using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace iClassic.Models.Metadata
{
    public class ProductTypeMetadata
    {        
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Các số đo có thể có")]
        public virtual ICollection<ProductTyeField> ProductTyeField { get; set; }
    }
}