using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public enum TicketStatus
    {
        [Description("Chưa xử lý")]
        ChuaXuLy,

        [Description("Đã xử lý")]
        DaXuLy,

        [Description("Đã trả cho khách")]
        DaTraChoKhach
    }

    public enum ReportTypes
    {
        SevenDaysRecent = 1,

        ThisMonth = 2,

        LastMonth = 3,

        SixMonthRecent = 4,

        AllTime = 5,
    }
}