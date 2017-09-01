using System.ComponentModel.DataAnnotations;

namespace iClassic.Models
{
    public class EmployeeEditModel
    {
        public string Id { get; set; }
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
        public bool IsActive { get; set; }
        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string Role { get; set; }
    }
}