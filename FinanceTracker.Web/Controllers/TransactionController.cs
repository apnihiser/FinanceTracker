using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Enums;
using FinanceTracker.Web.Models;
using FinanceTracker.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Transactions;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionData _transactionData;
        private readonly IProviderData _providerData;
        private readonly IAccountData _accountData;
        private readonly ISelectListProvider _selectList;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;
        private readonly IDateTimeProvider _dateTime;

        public TransactionController(
            ITransactionData transactionData,
            IProviderData providerData,
            IAccountData accountData,
            ISelectListProvider selectList,
            UserManager<ApplicationUserIdentity> userManager,
            SignInManager<ApplicationUserIdentity> signInManager,
            IDateTimeProvider dateTime)
            
        {
            _transactionData = transactionData;
            _providerData = providerData;
            _accountData = accountData;
            _selectList = selectList;
            _userManager = userManager;
            _signInManager = signInManager;
            _dateTime = dateTime;
        }

        public async Task<IActionResult> Index(DateTime dateTime = default(DateTime), DateNavigation direction = DateNavigation.None)
        {
            DateTime dateTimeOutput = default(DateTime);

            if (dateTime == default(DateTime) && direction == DateNavigation.None)
            {
                dateTimeOutput = DateTime.Now;
            }
            else if (direction == DateNavigation.Back)
            {
                dateTimeOutput = dateTime.AddMonths(-1);
            }
            else if (direction == DateNavigation.Forwards)
            {
                dateTimeOutput = dateTime.AddMonths(1);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            var data = await _transactionData.GetUserTransactionsByMonth(userId, dateTimeOutput);

            if (data.Count == 0)
            {
                return View();
            }

            List<TransactionViewModel> transactionList = new();

            data.ForEach( x =>
            {
                transactionList.Add( new TransactionViewModel
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

            TransactionDateViewModel output = new TransactionDateViewModel()
            {
                Transactions = transactionList,
                TargetMonth = data.First().DueDate
            };


            ViewBag.ProviderSelectList = await _selectList.ProviderSelectList();
            ViewBag.AccountSelectList = await _selectList.AccountSelectList();
            ViewBag.StatusSelectList = _selectList.StatusSelectList();

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

        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var record = await _transactionData.GetFullTransactionById(id);

            TransactionUpdateViewModel output = new TransactionUpdateViewModel()
            {
               Id = record.Id,
               AccountId = record.AccountId,
               PayeeId = record.PayeeId,
               AmountDue = record.Amount,
               DueDate = record.DueDate,
               Status = record.Status
            };

            ViewBag.ProviderSelectList = await _selectList.ProviderSelectList();
            ViewBag.AccountSelectList = await _selectList.AccountSelectList();
            ViewBag.StatusSelectList = _selectList.StatusSelectList();

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TransactionUpdateViewModel input)
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
        public async Task<IActionResult> Create(TransactionViewModel input)
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
