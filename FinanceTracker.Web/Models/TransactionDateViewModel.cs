namespace FinanceTracker.Web.Models
{
    public class TransactionDateViewModel
    {
        public List<TransactionViewModel>? Transactions { get; set; }
        public DateTime TargetMonth { get; set; }
    }
}
