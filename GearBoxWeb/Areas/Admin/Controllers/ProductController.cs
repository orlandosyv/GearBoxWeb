using GearBox.DataAccess.Repository.IRepository;
using GearBox.DataAccess.Data;
using GearBox.Models;
using Microsoft.AspNetCore.Mvc;
using GearBox.DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearBox.Models.ViewModels;

namespace GearBoxWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = 
                _unitOfWork.Product.GetAll().ToList();  
            
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
                _unitOfWork.Product.Add(productVM.Product); //add new row on DB
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
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj); 
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index"); //redirect to page           
        }

    }
}
