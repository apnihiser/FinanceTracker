using FinanceTracker.DataAccess.Data;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionData _transactionData;

        public TransactionController(ITransactionData transactionData)
        {
            _transactionData = transactionData;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _transactionData.GetAllFullTransactions();

            if (data is null)
            {
                return View();
            }

            List<TransactionFullDisplayModel> output = new();

            data.ForEach( x =>
            {
                output.Add( new TransactionFullDisplayModel
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    PayeeId = x.PayeeId,
                    AccountName = x.AccountName,
                    ProviderName = x.ProviderName,
                    AmountDue = x.Amount,
                    DueDate = x.DueDate,
                    Service = x.Service,
                    Status = x.Status
                });
            });

            return View(output);
        }
    }
}
