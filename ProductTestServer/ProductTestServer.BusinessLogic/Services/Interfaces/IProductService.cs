using ProductTestServer.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTestServer.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> FindAsync(int id);
        Task<Product> UpdateAsync(Product product);
        IEnumerable<Product> GetAll(string fild, string orderBy);
    }
}
