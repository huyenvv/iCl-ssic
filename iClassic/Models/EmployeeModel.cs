using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iClassic.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Chi nhánh")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Sai định dạng {0}")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [StringLength(100, ErrorMessage = "{0} phải chưa đủ {2} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "{0} phải chứa ít nhất 1 kí tự hoa, 1 kí tự số và 1 kí tự đặc biệt.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string Role { get; set; }
    }
}