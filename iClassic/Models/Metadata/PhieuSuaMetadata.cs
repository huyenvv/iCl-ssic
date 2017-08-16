﻿using System.ComponentModel.DataAnnotations;

namespace iClassic.Models.Metadata
{
    public class PhieuSuaMetadata
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Ngày nhận")]
        public System.DateTime NgayNhan { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Ngày trả")]
        public System.DateTime NgayTra { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Khách hàng")]
        public int KhachHangId { get; set; }
    }
}