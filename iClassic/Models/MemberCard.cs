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
    
    public partial class MemberCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberCard()
        {
            this.Invoice = new HashSet<Invoice>();
            this.ProductMemberCard = new HashSet<ProductMemberCard>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int BirthDayDiscount { get; set; }
        public int NguoiThanDiscount { get; set; }
        public double SoTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductMemberCard> ProductMemberCard { get; set; }
    }
}
