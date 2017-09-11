using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public enum TicketStatus
    {
        [Description("Tiếp nhận và chờ xử lý")]
        ChuaXuLy,

        [Description("Đã xử lý")]
        DaXuLy,

        [Description("Đã trả cho khách")]
        DaTraChoKhach
    }

    public enum PhieuSuaType
    {
        [Description("Bảo hành")]
        BaoHanh,

        [Description("Khách nhờ sửa")]
        KhachNhoSua,
    }

    public enum LoiPhieuSuaType
    {
        [Description("Người CẮT")]
        NguoiCat = 1,

        [Description("Người MAY")]
        NguoiMay = 2,

        [Description("Người ĐO")]
        NguoiDo = 3,

        [Description("Lý do khác")]
        LyDoKhac = 4,
    }

    public enum ReportTypes
    {
        SevenDaysRecent = 1,

        ThisMonth = 2,

        LastMonth = 3,

        SixMonthRecent = 4,

        AllTime = 5,
    }

    public enum ThoTypes
    {
        [Description("Thợ đo")]
        Do = 1,

        [Description("Thợ cắt")]
        Cat = 2,

        [Description("Thợ may")]
        May = 3
    }
}