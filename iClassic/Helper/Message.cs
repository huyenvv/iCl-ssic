using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public static class Message
    {
        // Success
        public const string Update_Successfully = "Cập nhật thành công";
        public const string Delete_Successfully = "Xóa thành công";

        // Fail
        public const string Update_Fail = "Có lỗi xảy ra. Vui lòng liên hệ quản trị để giải quyết.";
        public const string RoleNotExists = "Không tồn tại \"quyền hạn này\"";
    }
}