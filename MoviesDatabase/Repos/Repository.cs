using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
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

        public virtual async Task<(bool, string)> Create(T entity)
        {
            (bool result, string message, ICollection<T> entities) = await GetAll();

            if (!result) return (false, message);
            bool found = entities.Any(x => x == entity);
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

        public virtual async Task<(bool, string)> Delete(T entity)
        {
            (bool result, string message, ICollection<T> entities) = await GetAll();

            if (!result) return (false, message);
            bool found = entities.Any(x => x == entity);
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

        public virtual async Task<(bool, string, ICollection<T>)> GetAll()
        {
            try
            {
                var items = await _context.Set<T>().ToListAsync();

                return (true, "", items);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
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
