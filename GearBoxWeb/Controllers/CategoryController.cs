﻿using GearBoxWeb.Data;
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
                _db.Categories.Add(obj); //add new row on DB
                _db.SaveChanges();
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
            Category categoryFromDb = _db.Categories.Find(id);            
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
                _db.Categories.Update(obj); //add new row on DB
                _db.SaveChanges();
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
            Category categoryFromDb = _db.Categories.Find(id);            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }           
            _db.Categories.Remove(obj); //add new row on DB
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); //redirect to page           
        }

    }
}
