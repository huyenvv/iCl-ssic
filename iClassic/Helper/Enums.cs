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

    public enum PhieuSanXuatStatus
    {
        [Description("Chưa xử lý")]
        None,

        [Description("Đã giao cho thợ cắt")]
        DaGiaoChoThoCat,

        [Description("Đã nhận từ thợ cắt")]
        DaNhanTuThoCat,

        [Description("Đã giao cho thợ may")]
        DaGiaoChoThoMay,

        [Description("Đã nhận từ thợ may")]
        DaNhanTuThoMay,

        [Description("Đã giao cho khách")]
        DaGiaoChoKhach
    }

    public enum PhieuSuaStatus
    {
        [Description("Chưa xử lý")]
        None,

        [Description("Đã giao cho thợ sửa")]
        DaGiaoChoTho,

        [Description("Đã nhận từ thợ sửa")]
        DaNhanThoSua,

        [Description("Đã giao cho khách")]
        DaGiaoChoKhach
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
        May = 3,

        [Description("Thợ cắt & may")]
        CatMay = 4
    }

    public enum CustomerTypes
    {
        [Description("Thông thường")]
        ThongThuong = 1,

        [Description("Tiềm năng")]
        TiemNang = 2,

        [Description("VIP")]
        VIP = 3
    }

    public enum VaiTypes
    {
        [Description("Khách mang vải đến")]
        KhachMangVaiDen,

        [Description("Không có sẵn tại cửa hàng")]
        KhongCoSan,

        [Description("Vải mẫu tại cửa hàng")]
        VaiMauCuaHang
    }

    public enum SalaryType
    {
        [Description("Nhân viên")]
        Employee = 1,

        [Description("Thợ")]
        Worker = 2,
    }
}