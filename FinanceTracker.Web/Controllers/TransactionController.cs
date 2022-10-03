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
    //[Authorize]
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
            userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
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

        public async Task<IActionResult> GetProviderTransactionResults(DateTime dateTime = default(DateTime))
        {
            CommonResponse<List<TransactionProviderChartViewModel>> commonResponse = new CommonResponse<List<TransactionProviderChartViewModel>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionProviderChartDataByMonth(userId, dateTime);

                List<TransactionProviderChartViewModel> result = new List<TransactionProviderChartViewModel>();

                dataRows.ForEach( x => result.Add(new TransactionProviderChartViewModel { Amount = x.Amount, Name = x.ProviderName }) );

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
            CommonResponse<List<TransactionStatusChartViewModel>> commonResponse = new CommonResponse<List<TransactionStatusChartViewModel>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionStatusChartDataByMonth(userId, dateTime);

                List<TransactionStatusChartViewModel> result = new List<TransactionStatusChartViewModel>();

                dataRows.ForEach(x => result.Add(new TransactionStatusChartViewModel { Amount = x.Amount, Name = x.Status }));

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

        public async Task<IActionResult> GetStatusCodeTransactionResults(DateTime dateTime = default(DateTime))
        {
            CommonResponse<List<TransactionStatusCountChartViewModel>> commonResponse = new CommonResponse<List<TransactionStatusCountChartViewModel>>();

            try
            {
                var dataRows = await _transactionData.GetTransactionStatusCountChartDataByMonth(userId, dateTime);

                List<TransactionStatusCountChartViewModel> result = new List<TransactionStatusCountChartViewModel>();

                dataRows.ForEach(x => result.Add(new TransactionStatusCountChartViewModel { Count = x.Count, Name = x.Status }));

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
