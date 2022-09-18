using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static FinanceTracker.Web.Utility.Helper;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountData _accountData;

        public AccountController(IAccountData accountData)
        {
            _accountData = accountData;
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

            return View(displayAccount);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

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
        public async Task<IActionResult> Update(AccountDisplayModel input)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AccountModel accountRecord = new()
                {
                    Id = input.Id,
                    Title = input.Title,
                    Description = input.Description,
                    Type = input.Type,
                    Balance = input.Balance,
                    ApplicationUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                };

                await _accountData.Update(accountRecord);

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
                HolderId = model.ApplicationUserId
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
