using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PurchaseManagement.Entity;
using PurchaseManagement.Repository;

namespace PurchaseManagement.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IApplicationRepository<Category> _applicationRepository;

        public CategoriesController(IApplicationRepository<Category> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await this._applicationRepository.GetAllAsync();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                await this._applicationRepository.CreateAsync(category);
                var categories = await this._applicationRepository.GetAllAsync();
                return PartialView("_List", categories);
            } else {
                var categories = await this._applicationRepository.GetAllAsync();
                return PartialView("_List", categories);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            await this._applicationRepository.UpdateAsync(category);
            var categories = await this._applicationRepository.GetAllAsync();
            return PartialView("_List", categories);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await this._applicationRepository.GetAsync(cat => cat.Id == id);
            if (category != null)
            {
                var result = await this._applicationRepository.DeleteAsync(category);
                return Json(new { success = result });
            }
            return Json(new { success = false });
        }
    } 
}
