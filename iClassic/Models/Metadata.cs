using iClassic.Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace iClassic.Models
{
    [MetadataType(typeof(BranchMetadata))]
    public partial class Branch
    {
    }

    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }

    [MetadataType(typeof(PhieuChiMetadata))]
    public partial class PhieuChi
    {        
    }

    [MetadataType(typeof(InvoiceMetadata))]
    public partial class Invoice
    {
    }

    [MetadataType(typeof(PhieuSuaMetadata))]
    public partial class PhieuSua
    {
    }

    [MetadataType(typeof(LoaiVaiMetadata))]
    public partial class LoaiVai
    {
    }

    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUsers
    {
    }

    [MetadataType(typeof(ProductTypeMetadata))]
    public partial class ProductType
    {
    }

    [MetadataType(typeof(ThoMetadata))]
    public partial class Tho
    {
    }

    [MetadataType(typeof(MemberCardMetadata))]
    public partial class MemberCard
    {
    }
}