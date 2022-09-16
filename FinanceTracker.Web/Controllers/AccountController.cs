using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FinanceTracker.Web.Utility.Helper;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountData _accountData;
        //private readonly IPayorData _payorData;

        public AccountController(IAccountData accountData/*, IPayorData payorData*/)
        {
            _accountData = accountData;
            //_payorData = payorData;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _accountData.GetAllFullAccounts();

            if (data is null)
            {
                return View();
            }

            List<FullAccountViewModel> output = new();

            data.ForEach(x =>
            {
                output.Add(new FullAccountViewModel
                {
                    Id = x.Id,
                    HolderId = x.HolderId,
                    Balance = x.Balance,
                    Description = x.Description,
                    Title = x.Title,
                    Type = x.Type,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                });
            });

            return View(output);
        }

        public async Task<IActionResult> Display(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }

            FullAccountModel account = await _accountData.GetFullAccountByHolderId(id);
            //List<PayorModel> payors = await _payorData.GetAllPayors();

            if (account is null/* || payors is null*/)
            {
                return RedirectToAction("Index");
            }

            FullAccountViewModel displayAccount = new ()
            {
                Id = account.Id,
                Title = account.Title,
                Description = account.Description,
                Type = account.Type,
                Balance = account.Balance,
                HolderId = account.HolderId
            };

            //List<SelectListItem> selectList = ConvertToSelectList(payors);

            //ViewBag.UserSelectList = selectList;

            return View(displayAccount);
        }

        //public async Task<IActionResult> Create()
        //{
        //    List<PayorModel> payors = await _payorData.GetAllPayors();

        //    List<SelectListItem> selectList = ConvertToSelectList(payors);

        //    ViewBag.UserSelectList = selectList;

        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var num = await _accountData.Create(model.Title, model.Description, model.Type, model.Balance, model.HolderId);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AccountDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                await _accountData.Update(model.Id, model.Title, model.Description, model.Type, model.Balance, model.HolderId);

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _accountData.GetAccountsByUserId(id);

            if (model is null)
            {
                return View();
            }

            AccountDisplayModel output = new AccountDisplayModel()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Type = model.Type,
                Balance = model.Balance,
                HolderId = model.HolderId
            };

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AccountDisplayModel model)
        {
            await _accountData.Delete(model.Id);

            return RedirectToAction("Index");
        }
    }
}
