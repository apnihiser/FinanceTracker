namespace FinanceTracker.Web.Utility
{
    public interface IDateTimeProvider
    {
        DateTime CurrentDate { get; set; }
    }
}