using Microsoft.EntityFrameworkCore;
using ProductTestServer.DataAccess.Ef;
using ProductTestServer.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductTestServer.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationContext db;

        public Repository(ApplicationContext context)
        {
            this.db = context;
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().AsQueryable();
        }

        public async Task<T> GetAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public T FindSingle(Func<T, bool> predicate)
        {
            return db.Set<T>().SingleOrDefault(predicate);
        }
    }
}
