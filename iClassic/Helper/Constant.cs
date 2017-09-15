using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{    
    public class Constant
    {
        public const string SESSION_TicketDetails = "SESSION_TICKET_DETAILS";
        public const string SESSION_CheckoutDetails = "SESSION_CHECKOUT_DETAILS";

        public const string SESSION_MessageSuccess = "SESSION_MESSAGE_SUCCESS";
        public const string SESSION_MessageError = "SESSION_MESSAGE_ERROR";

        public const string SESSION_CurrentBrach = "SESSION_CurrentBrach";

        public static Dictionary<int, string> ListKenhQuangCao = new Dictionary<int, string>()
        {
            {1, "Google"},
            {2, "Facebook"},
            {3, "Khách cũ"},
            {4, "Khách qua giới thiệu"},
            {5, "Khách vãng lai"},
            {6, "Người quen"},
        };

        public static Dictionary<int, string> CustomerGroup = new Dictionary<int, string>()
        {
            {0, "-Tất cả-"},
            {1, CustomerTypes.ThongThuong.GetDescription()},
            {2, CustomerTypes.TiemNang.GetDescription()},
            {3, CustomerTypes.VIP.GetDescription()}
        };
    }
}