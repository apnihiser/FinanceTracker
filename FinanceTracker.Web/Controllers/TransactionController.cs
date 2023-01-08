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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string userId;

        public TransactionController(
            ITransactionData transactionData,
            IProviderData providerData,
            IAccountData accountData,
            ISelectListProvider selectList,
            UserManager<ApplicationUserIdentity> userManager,
            SignInManager<ApplicationUserIdentity> signInManager,
            IDateTimeProvider dateTime,
            IHttpContextAccessor contextAccessor)
            
        {
            _transactionData = transactionData;
            _providerData = providerData;
            _accountData = accountData;
            _selectList = selectList;
            _userManager = userManager;
            _signInManager = signInManager;
            _dateTime = dateTime;
            _contextAccessor = contextAccessor;
            userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

            var data = await _transactionData.GetUserTransactionsByMonth(userId, dateTimeOutput);

            ViewBag.ProviderSelectList = await _selectList.ProviderSelectList();
            ViewBag.AccountSelectList = await _selectList.AccountSelectList();
            ViewBag.StatusSelectList = _selectList.StatusSelectList();
            ViewBag.TransactionTypeList = _selectList.TransactionTypeSelectList();

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
                    Reason = x.TransactionReason,
                    Type = x.Type,
                    AccountName = x.AccountName,
                    ProviderName = x.ProviderName,
                    AmountDue = x.Amount,
                    DueDate = x.DueDate,
                    Status = x.Status
                });
            });

            TransactionDateViewModel output = new TransactionDateViewModel()
            {
                Transactions = transactionList,
                TargetMonth = data.First().DueDate
            };

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

            // if transaction status is "Cleared" then we need to find the account and reverse the amount from the account!
            // Get Transaction by Id
            var transaction = await _transactionData.GetFullTransactionById(id);

            // Check if Transaction Id is "Cleared"
            if (transaction.Status == "Cleared")
            {
                // if cleared reverse Amount from status
                var account = await _accountData.GetAccountByAccountId(transaction.AccountId);

                // ex1: balance = 10.00 - (-1.00) = 11.00
                // ex2: balance = 10.00 - (1.00) = 9.00
                account.Balance -= transaction.Amount;

                await _accountData.Update(account);
            }

            await _transactionData.DeleteTransactionById(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TransactionUpdateViewModel input)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            // Check to see if Status was changed
            // Get unmodified Transaction from the database
            var transaction = await _transactionData.GetFullTransactionById(input.Id);

            // was transaction previously cleared?
            bool wasCleared = false;

            if(transaction.Status == "Cleared")
            {
                wasCleared = true;
            }

            // Check if Transaction Id is "Cleared"
            bool isCleared = false;

            if (input.Status == "Cleared")
            {
                isCleared = true;
            }

            // Was it changed to Cleared?
            if (wasCleared != isCleared && input.Status == "Cleared")
            {
                // Since transaction was changed from non-cleared to cleared, we need to add the change to the account
                var account = await _accountData.GetAccountByAccountId(input.AccountId);

                // ex1: balance = 10.00 + (-1.00) = 9.00
                // ex2: balance = 10.00 + (1.00) = 11.00
                account.Balance += input.AmountDue;

                await _accountData.Update(account);
            }
            // Was it changed to not cleared (from cleared)?
            // **QUESTION, what if changed to nonCleared (from cleared) AND a Dollar Amount changed?**
            // **ANSWER: We only need to reverse what was previously cleared, we don't care if the amount changes until it is cleared in the future.**
            else if(wasCleared != isCleared && input.Status != "Cleared")
            {
                // Since transaction was changed from non-cleared to cleared, we need to add the change to the account
                var account = await _accountData.GetAccountByAccountId(input.AccountId);

                // We are going to use the unmodified transaction! since we want only reverse what was previously cleared
                // We DO NOT care about the new amount provided, only until it get changed to CLEARED in the future.
                // ex1: balance = 10.00 - (-1.00) = 11.00
                // ex2: balance = 10.00 - (1.00) = 9.00
                account.Balance -= transaction.Amount;

                await _accountData.Update(account);
            }
            // Will need to update to maintain balance between cleared transaction of user were to update.
            else if(transaction.Status == "Cleared" && input.Status == "Cleared")
            {
                // account was cleared previous but user is updating cleared transaction value, so we need to reverse previous transaction
                var account = await _accountData.GetAccountByAccountId(input.AccountId);
                // ex1: balance = 10.00 - (-1.00) = 11.00
                // ex2: balance = 10.00 - (1.00) = 9.00
                account.Balance -= transaction.Amount;
                await _accountData.Update(account);

                // Now add new transaction
                // ex1: balance = 10.00 + (-1.00) = 9.00
                // ex2: balance = 10.00 + (1.00) = 11.00
                account.Balance += input.AmountDue;

                await _accountData.Update(account);
            }

            TransactionModel output = new TransactionModel()
            {
                Id = input.Id,
                AccountId = input.AccountId,
                TransactionReason = input.Reason,
                Type = input.Type,
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
                AccountId = input.AccountId,
                PayeeId = input.PayeeId,
                TransactionReason = input.Reason,
                Type = input.Type,
                Amount = input.AmountDue,
                DueDate = input.DueDate,
                Status = input.Status
            };

            // Get Transaction by Id
            int id = await _transactionData.CreateTransaction(output);
            var transaction = await _transactionData.GetFullTransactionById(id);

            // Check if Transaction Id is "Cleared"
            if (output.Status == "Cleared")
            {
                // if cleared change Amount from status
                var account = await _accountData.GetAccountByAccountId(transaction.AccountId);

                // ex1: balance = 10.00 + (-1.00) = 9.00
                // ex2: balance = 10.00 + (1.00) = 11.00
                account.Balance += output.Amount;

                await _accountData.Update(account);
            }    

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetProviderTransactionResults(DateTime dateTime = default(DateTime))
        {
            CommonResponse<List<TransactionProviderChartData>> commonResponse = new CommonResponse<List<TransactionProviderChartData>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionProviderChartDataByMonth(userId, dateTime);

                List<TransactionProviderChartData> result = new List<TransactionProviderChartData>();

                dataRows.ForEach( x => result.Add(new TransactionProviderChartData { Amount = x.Amount, Name = x.ProviderName }) );

                commonResponse.DataEnum = result;

                commonResponse.Status = Helper.success_code;
            }
            catch(Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        public async Task<IActionResult> GetStatusTransactionResults(DateTime dateTime = default(DateTime))
        {
            CommonResponse<List<TransactionStatusChartData>> commonResponse = new CommonResponse<List<TransactionStatusChartData>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionStatusChartDataByMonth(userId, dateTime);

                List<TransactionStatusChartData> result = new List<TransactionStatusChartData>();

                dataRows.ForEach(x => result.Add(new TransactionStatusChartData { Amount = x.Amount, Name = x.Status }));

                commonResponse.DataEnum = result;
                commonResponse.Message = Helper.ChartLoadSuccessful;
                commonResponse.Status = Helper.success_code;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        public async Task<IActionResult> GetStatusCountTransactionResults(DateTime dateTime = default(DateTime))
        {
            CommonResponse<List<TransactionStatusCountChartData>> commonResponse = new CommonResponse<List<TransactionStatusCountChartData>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionStatusCountChartDataByMonth(userId, dateTime);

                List<TransactionStatusCountChartData> result = new List<TransactionStatusCountChartData>();

                dataRows.ForEach(x => result.Add(new TransactionStatusCountChartData { Count = x.Count, Name = x.Status }));

                commonResponse.DataEnum = result;
                commonResponse.Message = Helper.ChartLoadSuccessful;
                commonResponse.Status = Helper.success_code;
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }
    }
}
