using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess;
using Web.Models.Interfaces;
using Web.Models.Models;
using WebApplication1.Models;
using WebApplication1.Views.ViewModels;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
      

            private readonly IBaseRepository<Category> _baseRepository;
            private readonly CategoryContext _context;
        public CategoryController(IBaseRepository<Category> baseRepository, CategoryContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }
        [HttpGet]
            public async Task<IActionResult> GetAllCategories()
            {
                var allCats = await _baseRepository.GetAllAsync();
                var ordered=  allCats.OrderBy(c=>c.catOrder).ThenByDescending(c=>c.catName).ToList();
                
            return View(ordered);
            }
            [HttpGet]
            public IActionResult createCategory()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> createCategory(Category cat)
            {
            await _baseRepository.CreateAsync(cat);
            return RedirectToAction("GetAllCategories");
            }



            [HttpGet]
            public async Task<IActionResult> EditCategory(int id)
            {
                var cat = await _baseRepository.GetByIdAsync(id);
                if (cat == null)
                {
                    return NotFound();
                }
                return View(cat);
            }
            [HttpPost]
            public async Task<IActionResult> EditCategory(Category cat)
            {
                var category = await _baseRepository.GetByIdAsync(cat.Id);
            if (category == null)
            {
                return NotFound();
            }
            category.catName = cat.catName;
            category.catOrder = cat.catOrder;
            await _baseRepository.UpdateAsync(category);

            return RedirectToAction("GetAllCategories");
            }



            public async Task<IActionResult> CategoryDetails(int Id)
            {
                var cat = await _baseRepository.GetByIdAsync(Id);
                if (cat == null)
                {
                    return NotFound();
                }

            var category = new CategoryDetailsViewModel()
            {
                Id = cat.Id,
                catName = cat.catName,
                catOrder = cat.catOrder,
                createdDate = cat.createdDate,
                markedAsDeleted = cat.markedAsDeleted
            };
           
                return View(category);
            }

            public async Task<IActionResult> DeleteCategory(int id)
            {

                var foundcategory = await _baseRepository.GetByIdAsync(id);
                if (foundcategory != null)
                {
                  foundcategory.markedAsDeleted = true;
                  await  _context.SaveChangesAsync();
                }
                return RedirectToAction("GetAllCategories");
            }


          
        }
    
}
