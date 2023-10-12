using GearBoxWeb.Data;
using GearBoxWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GearBoxWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }            

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); //add new row on DB
                _db.SaveChanges();
                return RedirectToAction("Index"); //redirect to page
            }  
            return View();            
        }
    }
}
