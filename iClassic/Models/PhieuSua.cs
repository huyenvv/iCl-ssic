//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iClassic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuSua
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public Nullable<double> SoTien { get; set; }
        public int Status { get; set; }
        public int StatusGiaoNhan { get; set; }
        public int Type { get; set; }
        public int InvoiceId { get; set; }
        public Nullable<int> ProblemBy { get; set; }
        public int ThoId { get; set; }
    
        public virtual Tho Tho { get; set; }
        public virtual Tho Tho1 { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
