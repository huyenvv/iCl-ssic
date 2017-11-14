namespace iClassic.Models
{
    public class SortPagingBase
    {
        private System.Web.UI.WebControls.SortDirection? _sortOrder { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string SortName { get; set; }
        public System.Web.UI.WebControls.SortDirection SortOrder
        {
            get
            {
                if (_sortOrder.HasValue)
                {
                    return _sortOrder.Value;
                }
                return System.Web.UI.WebControls.SortDirection.Descending;
            }
            set {
                _sortOrder = value;
            }
        } // 1 ASC, 2: DESC
        public string SearchText { get; set; }
    }
}