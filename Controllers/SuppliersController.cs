using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.Entity;
using PurchaseManagement.Repository;

namespace PurchaseManagement.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IApplicationRepository<Supplier> _applicationRepository;

        public SuppliersController(IApplicationRepository<Supplier> applicationRepository)
        {
            this._applicationRepository = applicationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await this._applicationRepository.GetAllAsync();

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier) 
        { 
            if (ModelState.IsValid)
            {
                supplier.CreatedDate = DateTime.Now;
                this._applicationRepository.Create(supplier);
                return RedirectToAction("Index");
            } else
            {
                TempData["errorMessage"] = "Input data is not valid";
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await this._applicationRepository.GetAsync(s => s.Id == id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await this._applicationRepository.UpdateAsync(supplier);
                return RedirectToAction("Index");
            } 
            else
            {
                TempData["errorMessage"] = "Input data is not valid";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await this._applicationRepository.GetAsync(s => s.Id == id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Supplier supplier)
        {
            var existingSupplier = await this._applicationRepository.GetAsync(s => s.Id == supplier.Id);
            if (existingSupplier != null)
            {
                await this._applicationRepository.DeleteAsync(existingSupplier);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = "There is an error while handling your request.";
                return View();
            }
        }
    }
}
