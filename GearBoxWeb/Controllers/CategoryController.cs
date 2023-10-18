using GearBox.DataAccess.Repository.IRepository;
using GearBox.DataAccess.Data;
using GearBox.Models;
using Microsoft.AspNetCore.Mvc;

namespace GearBoxWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        //CREATE
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
                _categoryRepo.Add(obj); //add new row on DB
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); //redirect to page
            }  
            return View();            
        }
        //EDIT
        public IActionResult Edit(int? id)
        {
            if (id==null || id== 0) 
            {
                return NotFound();
            }
            Category categoryFromDb = _categoryRepo.Get(u=>u.Id == id);            
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {            
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj); //add new row on DB
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); //redirect to page
            }
            return View();
        }

        public IActionResult Delete (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }           
            _categoryRepo.Remove(obj); //add new row on DB
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); //redirect to page           
        }

    }
}
