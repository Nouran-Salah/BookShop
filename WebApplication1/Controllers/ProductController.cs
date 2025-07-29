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
    public class ProductController : Controller
    {
        private readonly CategoryContext _context;
        private readonly IBaseRepository<Product> _baseRepository;
        public ProductController(CategoryContext context , IBaseRepository<Product> baseRepository)
        {
            _context= context;
            _baseRepository = baseRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products =await _baseRepository.GetAllAsync();
            var productlist = new List<GetProductModel>();

            foreach (var p in products)
            {   
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == p.CategoryId);  
                productlist.Add(new GetProductModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Author = p.Author,
                    price = p.price,
                    CategoryName = category.catName
                });

            }

            return View(productlist);
        }

        [HttpGet] 
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel product)
        {
            if (product == null)
            {
                TempData["unsuccessfulCreation"] = "Couldn't create a product";
                return View();
            }
            var category =await _context.Categories.FirstOrDefaultAsync(c => c.catName == product.CategoryName);
            if (category == null)
            {
                TempData["unsuccessfulCreation"] = "Category not found";
                return View();
            }
            var pro = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Author = product.Author,
                price = product.price,
                CategoryId = category.Id
            };

            await _baseRepository.CreateAsync(pro);
            return RedirectToAction("GetAllProducts");
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var pro = await _baseRepository.GetByIdAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == pro.CategoryId);
            var model = new EditProductModel
            {
                Id = pro.Id,
                Title = pro.Title,
                Description = pro.Description,
                Author = pro.Author,
                price = pro.price,
                CategoryName = category?.catName // Use null-conditional operator to avoid null reference exception
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductModel pro)
        {
            var product = await _baseRepository.GetByIdAsync(pro.Id);

            if (product == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.catName == pro.CategoryName);

            product.Title = pro.Title;
            product.Description = pro.Description;
            product.Author = pro.Author;
            product.price = pro.price;
            product.CategoryId = category.Id;
            await _baseRepository.UpdateAsync(product);

            return RedirectToAction("GetAllProducts");
        }



        public async Task<IActionResult> ProductDetails(int Id)
        {
            var pro = await _baseRepository.GetByIdAsync(Id);
            if (pro == null)
            {
                return NotFound();
            }

            var product = new GetProductModel()
            {
                Title = pro.Title,
                Description = pro.Description,
                Author = pro.Author,
                price = pro.price,
                CategoryName = (await _context.Categories.FirstOrDefaultAsync(c => c.Id == pro.CategoryId))?.catName
            };

            return View(product);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _baseRepository.DeleteAsync(id);
            return RedirectToAction("GetAllProducts");
        }

    }
}
