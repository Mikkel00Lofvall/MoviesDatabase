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


        public async Task<(bool, string)> Delete(int MovieID)
        {
            try
            {
                var movie = await _context.Set<MovieModel>()
                    .Include(m => m.Actors)
                    .Include(m => m.ImagesBlobs)
                    .FirstOrDefaultAsync(x => x.id == MovieID);

                if (movie == null) return (false, "No Movie with that id in database");

                ICollection<ScheduleModel> schedules = await _context.Schedules
                    .Where(x => x.MovieId == MovieID)
                    .ToListAsync();

                foreach (ScheduleModel schedule in schedules) 
                {
                    _context.Schedules.Remove(schedule);
                }

                foreach (ImageBlobModel blob in _context.ImageBlobs)
                {
                    if (movie.ImagesBlobs.Contains(blob))
                    {
                        movie.ImagesBlobs.Remove(blob);
                        _context.ImageBlobs.Remove(blob);
                    }
                }

                var detail = await _context.Set<DetailsModel>()
                    .FirstOrDefaultAsync(x => movie.Details.id == x.id);

                if (detail != null) { _context.Details.Remove(detail); }

                ICollection<MovieThemeModel> movieThemeModels = await _context.Set<MovieThemeModel>()
                    .Where(x => x.MovieID == movie.id)
                    .ToListAsync();

                foreach (MovieThemeModel theme in movieThemeModels)
                {
                    movie.MovieThemeConnector.Remove(theme);
                    _context.MovieThemeConnector.Remove(theme);
                }

                _context.Movies.Remove(movie);

                await _context.SaveChangesAsync();
                return (true, "");

            }
            catch (Exception ex) { return (false, ex.Message); }

        }

    }
}
