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

        [Description("Đang xử lý")]
        DangXuLy,

        [Description("Đã xử lý và chờ trả cho khách")]
        DaXuLy,

        [Description("Đã trả cho khách")]
        DaTraChoKhach
    }

    public enum PhieuSuaType
    {
        [Description("Bảo hành do thợ làm hỏng")]
        BaoHanh,        

        [Description("Khách nhờ sửa")]
        KhachNhoSua,

        [Description("Bảo hành khách tăng giảm kg")]
        BaoHanhKhachTangGiamCan,
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