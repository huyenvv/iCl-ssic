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
    
    public partial class Tho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tho()
        {
            this.PhieuSua = new HashSet<PhieuSua>();
            this.PhieuSua1 = new HashSet<PhieuSua>();
            this.PhieuSanXuat = new HashSet<PhieuSanXuat>();
            this.PhieuSanXuat1 = new HashSet<PhieuSanXuat>();
            this.PhieuSanXuat2 = new HashSet<PhieuSanXuat>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> XuongId { get; set; }
        public int Type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSua> PhieuSua { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSua> PhieuSua1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSanXuat> PhieuSanXuat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSanXuat> PhieuSanXuat1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSanXuat> PhieuSanXuat2 { get; set; }
    }
}
