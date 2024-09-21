using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class ScheduleRepository : Repository<ScheduleModel>
    {
        private readonly MovieRepository MovieRepository;
        public ScheduleRepository(ContextDB context, MovieRepository repo) : base(context) 
        {
            this.MovieRepository = repo;
        }

        public async Task<(bool, string, IEnumerable<ScheduleModel>)> GetSchedulesByMovieID(int id)
        {
            try
            {
                var result = await _context.Set<ScheduleModel>()
                    .Include(m => m.Date)
                    .Where(x => x.MovieId == id)
                    .ToListAsync();

                return (true, "", result);
            }
            catch (Exception ex) 
            { 
                return (false, ex.Message, null);
            }

        }

        public async Task<(bool, string)> UpdateMovieWithSchedule(int movieID, DateModel date)
        {
            try
            {
                (bool result, string message, var movie) = await MovieRepository.GetWithId(movieID);
                if (result)
                {
                    if (movie != null)
                    {

                        _context.Dates.Add(date);

                        await _context.SaveChangesAsync();

                        ScheduleModel schedule = new ScheduleModel()
                        {
                            MovieId = movieID,
                            Date = date
                        };

                        _context.Schedules.Add(schedule);

                        await _context.SaveChangesAsync();

                        return (true, "");
                    }

                    return (true, "no movie with that id in database");
                }

                return (false, message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        } 
    }
}
