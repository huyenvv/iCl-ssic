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
        public System.DateTime NgayNhan { get; set; }
        public System.DateTime NgayTra { get; set; }
        public int Status { get; set; }
        public int KhachHangId { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
