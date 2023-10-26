using GearBox.DataAccess.Repository.IRepository;
using GearBox.DataAccess.Data;
using GearBox.Models;
using Microsoft.AspNetCore.Mvc;
using GearBox.DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearBox.Models.ViewModels;
using GearBox.Utility;
using Microsoft.AspNetCore.Authorization;

namespace GearBoxWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = 
                _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();  
            
            return View(objProductList);            
        }
        //CREATE
        public IActionResult Upsert(int? id) //Create
        {
            //Projections EF core
            //IEnumerable<SelectListItem> CategoryList = 
            //    _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            //    {
            //        Text = x.Name,
            //        Value = x.Id.ToString(),
            //    });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductViewModel productVM = new()
                {
                    CategoryList = 
                    _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(),
                        }),

                    Product = new Product()
                };

            if (id == null || id == 0) 
                {return View(productVM);}
            else { 
                //Update
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id == id);
                return View(productVM);
            }            
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() +  Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) 
                    {
                        //Delete Old Image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            { System.IO.File.Delete(oldImagePath); }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) 
                    { file.CopyTo(fileStream); }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0) 
                { _unitOfWork.Product.Add(productVM.Product); }
                else 
                { _unitOfWork.Product.Update(productVM.Product); }
                
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index"); //redirect to page
            }
            else {
                productVM.CategoryList =
                _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                return View(productVM);
            };            
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> objProductList =
                    _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null) 
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
                { System.IO.File.Delete(oldImagePath); }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            //List<Product> objProductList =
            //        _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { success = true, message="Delete Successful"});
        }

        #endregion

    }
}
