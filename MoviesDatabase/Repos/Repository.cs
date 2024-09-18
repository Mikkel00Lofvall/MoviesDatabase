using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ContextDB _context;
        public Repository(ContextDB context) 
        {
            _context = context;
        }

        public async Task<(bool, string)> Create(T entity)
        {
            var result = await GetAll();
            bool found = result.Any(x => x == entity);
            if (!found)
            {
                try
                {
                    _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync();
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, $"Failure: {ex}");
                }
            }
            else
            {
                return (false, $"Failure: Already Exist in db!");
            }
        }

        public async Task<(bool, string)> Delete(T entity)
        {
            var result = await GetAll();
            bool found = result.Any(x => x == entity);
            if (found)
            {
                try
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, $"Failure: {ex}");
                }
            }
            else
            {
                return (false, $"Failure: Does not exist in db!");
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<(bool, string, T?)> GetWithId(int id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.id == id);
            if (result != null)
            {
                return (true, "", result);
            }

            else
            {
                return (false, "Failure: There was no match in db", result);
            }
        }
    }
}
