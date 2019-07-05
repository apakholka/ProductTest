using ProductTestServer.Common.Models;
using System;
using System.Threading.Tasks;

namespace ProductTestServer.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }

        Task SaveAsync();
    }
}
