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

    //[MetadataType(typeof(BranchMetadata))]
    public partial class PhieuChi
    {
    }

    //[MetadataType(typeof(BranchMetadata))]
    public partial class PhieuSanXuat
    {
    }

    //[MetadataType(typeof(BranchMetadata))]
    public partial class PhieuSua
    {
    }

    [MetadataType(typeof(LoaiVaiMetadata))]
    public partial class LoaiVai
    {
    }
}