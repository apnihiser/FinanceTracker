using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FinanceTracker.Web.Utility;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class ProviderController : Controller
    {
        private readonly IProviderData _providerData;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITransactionData _transaction;
        private readonly IAccountBalanceService _accountBalance;
        private readonly string _userId;

        public ProviderController(IProviderData providerData, IHttpContextAccessor contextAccessor, ITransactionData transaction, IAccountBalanceService accountBalance)
        {
            _providerData = providerData;
            _contextAccessor = contextAccessor;
            _transaction = transaction;
            _accountBalance = accountBalance;
            _userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> Index()
        {
            var providers = await _providerData.GetAllProvidersByUserId(_userId);

            List <ProviderDisplayModel> output = new();

            foreach (var provider in providers)
            {
                output.Add(new ProviderDisplayModel
                {
                    Id = provider.Id,
                    PayorId = provider.PayorId,
                    Title = provider.Title,
                    Service = provider.Service,
                    Url = provider.Url
                });
            }

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProviderDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            await _providerData.UpdateProvider(model.Id, model.Service, model.Title, model.Url);

            return RedirectToAction("Index", new { model.Id });
        }

        public async Task<IActionResult> Display(int id)
        {
            ProviderDisplayModel? output = null;

            var provider = await _providerData.GetProviderById(id);

            if (provider is null)
            {
                return View();
            }

            output = new ProviderDisplayModel
            {
                Id = provider.Id,
                PayorId = provider.PayorId,
                Service = provider.Service,
                Title = provider.Title,
                Url = provider.Url
            };

            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProviderDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            int id = await _providerData.CreateProvider(userId, model.Title!, model.Service!, model.Url!);

            return RedirectToAction("Index", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var provider = await _providerData.GetProviderById(id);

            if (provider is null)
            {
                return View();
            }

            ProviderDisplayModel output = new ProviderDisplayModel
            {
                Id = provider.Id,
                PayorId = provider.PayorId,
                Service = provider.Service,
                Title = provider.Title,
                Url = provider.Url
            };

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProviderDisplayModel model)
        {
            // steps...
            // 1) look up all transactions with ProviderId.
            // 2) foreach transaction
            //     a. look up account by accountId
            //     b. rollback deposit or withdrawal from the transaction
            // 3) delete transaction
            // 4) delete provider

            //1)
            var transactions = await _transaction.GetFullTransactionsByProviderId(model.Id);

            //2)
            foreach (var transaction in transactions)
            {
                TransactionUpdateViewModel output = new()
                {
                    AccountId = transaction.AccountId,
                    PayeeId = transaction.PayeeId,
                    AmountDue = transaction.Amount,
                    DueDate = transaction.DueDate,
                    Id = transaction.Id,
                    Reason = transaction.TransactionReason,
                    Status = transaction.Status,
                    Type = transaction.Type
                };

                await _accountBalance.DeleteActionAccountBalance(output, true);

                //3)
                await _transaction.DeleteTransactionByProviderId(model.Id);
            }

            //4)
            await _providerData.DeleteProvider(model.Id);

            return RedirectToAction("Index");
        }
    }
}
