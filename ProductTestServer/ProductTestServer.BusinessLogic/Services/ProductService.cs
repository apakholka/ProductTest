using ProductTestServer.BusinessLogic.Services.Interfaces;
using ProductTestServer.Common.Models;
using ProductTestServer.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProductTestServer.BusinessLogic.Services
{
    public class ProductService: IProductService
    {
        private IUnitOfWork db;

        public ProductService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public async Task<Product> FindAsync(int id)
        {
            return await db.Products.GetAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var findProduct = db.Products.FindSingle(x => x.Id == product.Id);
            findProduct.Name = product.Name;
            findProduct.Category = product.Category;
            findProduct.Active = product.Active;
            findProduct.Price = product.Price;
            db.Products.Update(findProduct);
            await db.SaveAsync();
            return findProduct;
        }

        public IEnumerable<Product> GetAll(string field, string orderBy)
        {
            if (field != null)
            {
                return db.Products.GetAll().OrderBy($"{field} {orderBy}").ToList();
                  
            }
            return db.Products.GetAll().ToList();
        }
    }
}
