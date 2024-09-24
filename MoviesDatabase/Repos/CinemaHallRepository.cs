using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class CinemaHallRepository : Repository<CinemaHallModel>
    {
        public CinemaHallRepository(ContextDB context) : base(context) { }

        public async Task<IEnumerable<CinemaHallModel>> GetAll()
        {
            return await _context.Set<CinemaHallModel>()
                .Include(h => h.Seats)
                .ToListAsync();
        }

        public async Task<(bool, string, object)> GetHallBySchedule(int scheduleId)
        {
            if (scheduleId != null) 
            {
                try
                {
                    var result = await _context.Set<CinemaHallModel>()
                        .Include(h => h.SeatIDs)
                        .FirstOrDefaultAsync(x => x.Schedules.Any(s => s.id == scheduleId));

                    if (result != null)
                    {
                        var seats = await _context.Set<SeatModel>()
                            .Where(seat => result.SeatIDs.Contains(seat.id))
                            .ToListAsync();

                        if (seats != null) return (true, "", new { result, seats });
                        else return (false, "No Seats for this Hall", null);

                    }

                    else return (false, "No CinemaHall With That ID in DB", null);

                }

                catch (Exception ex) { return (false, ex.Message, null); }

            }

            else { return (false, "No Schedule ID Givin", null); }
        }


        public override async Task<(bool, string)> Create(CinemaHallModel cinema)
        {
            var result = await GetAll();
            bool found = result.Any(x => x == cinema);
            if (!found)
            {
                try
                {
                    _context.Set<CinemaHallModel>().Update(cinema);
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
    }
}
