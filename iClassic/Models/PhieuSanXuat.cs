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
    
    public partial class PhieuSanXuat
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public string MaVai { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double DatCoc { get; set; }
        public System.DateTime NgayThu { get; set; }
        public System.DateTime NgayLay { get; set; }
        public int Status { get; set; }
        public int KhachHangId { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
