using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class MovieRepository : Repository<MovieModel>
    {
        public MovieRepository(ContextDB context) : base(context) { }

        public override async Task<(bool, string, MovieModel?)> GetWithId(int id)
        {
            var result = await _context.Set<MovieModel>()
                .Include(m => m.ImagesBlobs)
                .Include(m => m.Actors)
                .Include(m => m.FrontPageImage)
                .Include(m => m.Details)
                .FirstOrDefaultAsync(x => x.id == id);

            if (result != null)
            {
                return (true, "", result);
            }
            else
            {
                return (false, "Failure: There was no match in db", result);
            }
        }


        public override async Task<IEnumerable<MovieModel>> GetAll()
        {
            return await _context.Set<MovieModel>()
                .Include(m => m.ImagesBlobs)
                .Include(m => m.Actors)
                .Include(m => m.FrontPageImage)
                .Include(m => m.Details)
                .ToListAsync();
        }

    }
}
