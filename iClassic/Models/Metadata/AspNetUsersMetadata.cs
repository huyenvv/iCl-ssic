using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iClassic.Models.Metadata
{
    public class AspNetUsersMetadata
    {
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public double PhoneNumber { get; set; }
    }
}