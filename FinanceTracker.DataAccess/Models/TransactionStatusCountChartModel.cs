using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Models
{
    public class TransactionStatusCountChartModel
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
}
