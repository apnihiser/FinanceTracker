using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinanceTracker.Web.Controllers
{
    public class PayorController : Controller
    {
        private readonly IPayorData _payorData;

        public PayorController(IPayorData payorData)
        {
            _payorData = payorData;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _payorData.GetAllPayors();

            List<PayorDisplayModel> output = new();

            foreach (var record in data)
            {
                output.Add(new PayorDisplayModel
                {
                    Id = record.Id,
                    FirstName = record.FirstName,
                    LastName = record.LastName
                });
            }

            return View(output);
        }

        public async Task<IActionResult> Display(int id)
        {
            PayorDisplayModel? output = null;
            var payor = await _payorData.GetPayorById(id);

            if(payor is null)
            {
                return View();
            }

            output = new PayorDisplayModel()
            {
                Id = payor.Id,
                FirstName = payor.FirstName,
                LastName = payor.LastName
            };

            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PayorDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            await _payorData.CreatePayor(model.FirstName, model.LastName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PayorDisplayModel model)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            await _payorData.UpdatePayor(model.Id, model.FirstName, model.LastName);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _payorData.GetPayorById(id);

            if (model is null)
            {
                return View();
            }

            PayorDisplayModel output = new PayorDisplayModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PayorDisplayModel model)
        {
            await _payorData.DeletePayor(model.Id);

            return RedirectToAction("Index");
        }
    }
}
