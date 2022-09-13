using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Models
{
    public class FullAccountModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public decimal Balance { get; set; }
        public int HolderId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
