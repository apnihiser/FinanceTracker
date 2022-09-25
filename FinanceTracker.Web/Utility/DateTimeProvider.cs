namespace FinanceTracker.Web.Utility
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime CurrentDate { get; set; }

        public DateTimeProvider()
        {
            CurrentDate = DateTime.Now;
        }
    }
}
