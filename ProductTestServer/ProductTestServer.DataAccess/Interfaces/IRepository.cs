using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTestServer.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        T FindSingle(Func<T, bool> predicate);
        void Update(T item);
    }
}
