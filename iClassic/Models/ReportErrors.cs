using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Helper;

namespace iClassic.Models
{
    public class ReportErrors
    {
        public LoiPhieuSuaType TypeError { get; set; }
        public string OtherError { get; set; }
        public int Count { get; set; }
    }
}