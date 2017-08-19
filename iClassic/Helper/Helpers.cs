using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public class Helpers
    {
        public static string RemoveSign4Vietnamese(string str)
        {
            var vietnameseSigns = new string[]
                {
                    "aAeEoOuUiIdDyY",
                    "áàạảãâấầậẩẫăắằặẳẵ",
                    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ",
                    "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ",
                    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ",
                    "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ",
                    "ÍÌỊỈĨ",
                    "đ",
                    "Đ",
                    "ýỳỵỷỹ",
                    "ÝỲỴỶỸ"
                };

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {

                for (int j = 0; j < vietnameseSigns[i].Length; j++)

                    str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);

            }

            return str;
        }
        public static string RemoveSymbol(string str)
        {
            //Giữ 1 ký tự trắng
            str = string.Join(" ", str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            //Loại trừ các ký tự đặc biệt - ngoài các ký tự sau
            string notSymbol =
                        " -[]abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" +
                        "áàạảãâấầậẩẫăắằặẳẵ" +
                        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ" +
                        "éèẹẻẽêếềệểễ" +
                        "ÉÈẸẺẼÊẾỀỆỂỄ" +
                        "óòọỏõôốồộổỗơớờợởỡ" +
                        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ" +
                        "úùụủũưứừựửữ" +
                        "ÚÙỤỦŨƯỨỪỰỬỮ" +
                        "íìịỉĩ" +
                        "ÍÌỊỈĨ" +
                        "đ" +
                        "Đ" +
                        "ýỳỵỷỹ" +
                        "ÝỲỴỶỸ";

            for (int i = 0; i < str.Length; i++)
            {
                if (notSymbol.IndexOf(str[i]) == -1)
                {
                    str = str.Remove(i, 1);
                    i--;
                }
            }
            return str;
        }
        public static string CreateURLParam(int id, string value)
        {
            return string.Format("{0}-{1}", RemoveSymbol(RemoveSign4Vietnamese(value)).Replace(" ", "-").Replace("--", "-"), id);
        }
        public static string CreateURLParam(string value)
        {
            return RemoveSymbol(RemoveSign4Vietnamese(value)).Replace(" ", "-").Replace("--", "-");
        }
        public static int GetIDFromURLParam(string param)
        {
            //Trả về 0 nếu không lọc được ID. Khi gọi, kiểm tra != 0 thì thực hiện tiếp
            int id = 0;
            if (!string.IsNullOrEmpty(param))
            {
                int startIndex = param.LastIndexOf('-');
                int endIndex = param.Length;

                if (startIndex < endIndex)
                {
                    int.TryParse(param.Substring(startIndex + 1, endIndex - startIndex - 1), out id);
                }
            }
            return id;
        }
        public static HashSet<KeyValuePair<int, string>> GetTicketStatusList()
        {
            var result = new HashSet<KeyValuePair<int, string>>();
            var list = Enum.GetValues(typeof(TicketStatus));
            foreach (int enumItem in list)
            {
                var keyValueItem = new KeyValuePair<int, string>(enumItem, ((TicketStatus)enumItem).GetDescription());
                result.Add(keyValueItem);
            }
            return result;
        }
        public static string DrawStatusTicket(int status)
        {
            var statusEnum = (TicketStatus)status;
            var className = "";
            switch (statusEnum)
            {
                case TicketStatus.DaXuLy:
                    className = "warning";
                    break;
                case TicketStatus.DaTraChoKhach:
                    className = "success";
                    break;
                default:
                    className = "danger";
                    break;
            }

            return "<span class='label label-" + className + "'>" + statusEnum.GetDescription() + "</span>";
        }
    }
}