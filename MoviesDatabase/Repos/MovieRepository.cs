using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesDatabase.Repos
{
    public class MovieRepository : Repository<MovieModel>, IMovieRepository
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


        public async Task<(bool, string, ICollection<MovieModel>)> GetAll()
        {
            try
            {
                var Movies = await _context.Set<MovieModel>()
                    .Include(m => m.ImagesBlobs)
                    .Include(m => m.Actors)
                    .Include(m => m.FrontPageImage)
                    .Include(m => m.Details)
                    .ToListAsync();

                return (true, "", Movies);
            }
            catch (Exception ex) { return (false, ex.Message,null); }
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
                    .Include(s => s.Seats)
                    .Include(s => s.Date)
                    .Where(x => x.MovieId == MovieID)
                    .ToListAsync();

                

                foreach (ScheduleModel schedule in schedules) 
                {

                    var tickets = await _context.Set<TicketModel>()
                        .Where(t => t.ScheduleID == schedule.id)
                        .ToListAsync();

                    foreach (TicketModel ticket in tickets)
                    {
                        _context.Tickets.Remove(ticket);
                    }
                    _context.Dates.Remove(schedule.Date);
                    foreach (SeatModel Seat in schedule.Seats)
                    {
                        _context.Seats.Remove(Seat);
                    }

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
