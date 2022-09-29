using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Web.Models
{
    public class CommonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T DataEnum { get; set; }
    }
}
