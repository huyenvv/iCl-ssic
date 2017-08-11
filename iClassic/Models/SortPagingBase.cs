namespace iClassic.Models
{
    public class SortPagingBase
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string SortName { get; set; }
        public System.Web.UI.WebControls.SortDirection SortOrder { get; set; } // 1 ASC, 2: DESC
        public string SearchText { get; set; }
    }
}