using GearBox.DataAccess.Data;
using GearBox.DataAccess.Repository.IRepository;
using GearBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GearBox.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Product obj)
        {
           var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            { 
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;                
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Description = obj.Description; 
                objFromDb.CategoryId = obj.CategoryId;  
                objFromDb.Author = obj.Author;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.ProductDiscount = obj.ProductDiscount;
                objFromDb.CostOfProduct = obj.CostOfProduct;
                objFromDb.Weight = obj.Weight;
                if (objFromDb.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            } 
            //_db.Products.Update(obj);
        }
    }
}
