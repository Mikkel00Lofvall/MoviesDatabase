using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<(bool, string, T?)> GetWithId(int id);
        Task<(bool, string)> Create(T entity);
        Task<(bool, string)> Delete(T entity);
    }
}
