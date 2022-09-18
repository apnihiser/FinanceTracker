using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionData _transactionData;
        private readonly IProviderData _providerData;
        private readonly IAccountData _accountData;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;

        public TransactionController(
            ITransactionData transactionData,
            IProviderData providerData,
            IAccountData accountData,
            UserManager<ApplicationUserIdentity> userManager,
            SignInManager<ApplicationUserIdentity> signInManager)
        {
            _transactionData = transactionData;
            _providerData = providerData;
            _accountData = accountData;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _transactionData.GetAllFullTransactionsByUserIdAsync(userId);

            if (data is null)
            {
                return View();
            }

            List<ProviderModel> providers = await _providerData.GetAllProviders();

            if (providers is null)
            {
                return View();
            }

            ProviderSelectListViewModel providerSelectList = new();

            providers.ForEach(x =>
            {
                providerSelectList.Providers.Add(new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            });

            List<AccountModel> accounts = await _accountData.GetAllAccounts();

            if (accounts is null)
            {
                return View();
            }

            AccountSelectListViewModel accountSelectList = new();

            accounts.ForEach(x =>
            {
                accountSelectList.Accounts.Add(new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            });

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

            ViewBag.ProviderSelectList = providerSelectList.Providers;
            ViewBag.AccountSelectList = accountSelectList.Accounts;

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            await _transactionData.DeleteTransactionById(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TransactionFullDisplayModel input)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            TransactionModel output = new TransactionModel()
            {
                Id = input.Id,
                AccountId = input.AccountId,
                PayeeId = input.PayeeId,
                Amount = input.AmountDue,
                DueDate = input.DueDate,
                Status = input.Status
            };

            await _transactionData.EditTransactionById(output);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionFullDisplayModel input)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            TransactionModel output = new TransactionModel()
            {
                Id = input.Id,
                AccountId = input.AccountId,
                PayeeId = input.PayeeId,
                Amount = input.AmountDue,
                DueDate = input.DueDate,
                Status = input.Status
            };

            await _transactionData.CreateTransaction(output);

            return RedirectToAction("Index");
        }
    }
}
