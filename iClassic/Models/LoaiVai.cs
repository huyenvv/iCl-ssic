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
    
    public partial class LoaiVai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiVai()
        {
            this.PhieuSanXuat = new HashSet<PhieuSanXuat>();
        }
    
        public int Id { get; set; }
        public string MaVai { get; set; }
        public string Name { get; set; }
        public double SoTienNhapVao { get; set; }
        public double SoTienBanRa { get; set; }
        public string Note { get; set; }
        public System.DateTime Created { get; set; }
        public string CreateBy { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSanXuat> PhieuSanXuat { get; set; }
    }
}
