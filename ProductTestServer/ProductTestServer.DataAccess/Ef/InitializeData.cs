using ProductTestServer.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTestServer.DataAccess.Ef
{
    public static class InitializeData
    {
        public static async Task Initialize(ApplicationContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "VISI/pocket",
                        Category = "firstClass",
                        Active = true,
                        Price = 999
                    },
                    new Product
                    {
                        Name = "VISI/frame",
                        Category = "secondClass",
                        Active = true,
                        Price = 888
                    }                 
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
