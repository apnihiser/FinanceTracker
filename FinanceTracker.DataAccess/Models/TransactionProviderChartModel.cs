using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Models
{
    public class TransactionProviderChartModel
    {
        public string ProviderName { get; set; }
        public decimal Amount { get; set; }
    }
}
