using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int PayeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string ProviderName { get; set; }
        public string Service { get; set; }
        public string AccountName { get; set; }
        public string TransactionReason { get; set; }
        public string Type { get; set; }
    }
}
