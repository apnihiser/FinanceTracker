using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Web.Controllers
{
    [Authorize]
    public class ProviderController : Controller
    {
        private readonly IProviderData _providerData;

        public ProviderController(IProviderData providerData)
        {
            _providerData = providerData;
        }

        public async Task<IActionResult> Index()
        {
            var providers = await _providerData.GetAllProviders();

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

            int id = await _providerData.CreateProvider(model.PayorId, model.Title!, model.Service!, model.Url!);

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
            await _providerData.DeleteProvider(model.Id);

            return RedirectToAction("Index");
        }
    }
}
