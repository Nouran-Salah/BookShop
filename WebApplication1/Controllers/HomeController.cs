using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.DataAccess;
using Web.Models.Models;
using WebApplication1.Models;
using WebApplication1.Views.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryContext _context;
        public HomeController(ILogger<HomeController> logger , CategoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
           var cat= _context.Categories.ToList();
            return View(cat);
        }
        [HttpGet]
        public IActionResult createCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createCategory(Category cat)
        {  
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return RedirectToAction("GetAllCategories");
        }



        [HttpGet]
        public IActionResult EditCategory(int Id)
        {
            var cat = _context.Categories.SingleOrDefault(c => c.Id == Id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(Category cat)
        {
            _context.Categories.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("GetAllCategories");
        }



        public IActionResult CategoryDetails(int Id)
        {
            var category = _context.Categories
            .Select(c => new CategoryDetailsViewModel
            {  Id=c.Id,
                catName = c.catName,
                catOrder = c.catOrder,
                createdDate = c.createdDate,
                markedAsDeleted = c.markedAsDeleted
            })
            .FirstOrDefault(c => c.Id == Id);
            return View(category);
        }

        public IActionResult DeleteCategory(int id)
        {
        
        var foundcategory = _context.Categories.Find(id);
            if (foundcategory != null)
            {
                foundcategory.markedAsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction("GetAllCategories");
         }


[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
