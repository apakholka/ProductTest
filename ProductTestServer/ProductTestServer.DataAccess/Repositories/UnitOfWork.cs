using ProductTestServer.Common.Models;
using ProductTestServer.DataAccess.Ef;
using ProductTestServer.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProductTestServer.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext db;
        private IRepository<Product> productRepository;

        public UnitOfWork(ApplicationContext context)
        {
            this.db = context;
        }

        public IRepository<Product> Products
        {
            get { return this.productRepository ?? (this.productRepository = new Repository<Product>(db)); }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
