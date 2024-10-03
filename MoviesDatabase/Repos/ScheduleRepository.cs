using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;
using MoviesDatabase.DTO;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class ScheduleRepository : Repository<ScheduleModel>, IScheduleRepository
    {
        private readonly MovieRepository _MovieRepository;
        private readonly IRepository<CinemaHallModel> _CinemaHallRepository;
        
        public ScheduleRepository(ContextDB context, MovieRepository movieRepo, IRepository<CinemaHallModel> cinmaRepo) : base(context) 
        {
            this._MovieRepository = movieRepo;
            this._CinemaHallRepository = cinmaRepo;
        }

        public async Task<(bool, string, ICollection<ScheduleModel>)> GetAll()
        {
            try
            {
                var schedules = await _context.Set<ScheduleModel>()
                    .Include(m => m.Date)
                    .ToListAsync();

                if (schedules.Count > 0) return (true, "", schedules);

                else return (false, "No Schedules in DB", null);


            }
            catch (Exception ex) { return (false, ex.Message, null); }

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
            catch (Exception ex) { return (false, ex.Message, null); }

        }

        char GetNextLetter(char letter)
        {
            if (letter == 'Z')
            {
                return 'A';
            }
            return (char)(letter + 1);
        }


        public async Task<(bool, string)> CreateScheduleAndInsertIntoHall(SchedulesDTO schedule)
        {
            try
            {
                var hall = await _context.CinemaHall
                    .Include(h => h.Schedules)
                    .FirstOrDefaultAsync(h => h.id == schedule.HallId);

                if (hall == null)
                    return (false, "No Hall With That ID");

                char currentLetter = 'A';

                ScheduleModel scheduleModel = new ScheduleModel()
                {
                    MovieId = schedule.MovieId,
                    Date = schedule.Date,
                    HallId = schedule.HallId,
                    Seats = new List<SeatModel>()
                };

                _context.Schedules.Add(scheduleModel);
                await _context.SaveChangesAsync();

                var savedSchedule = await _context.Schedules
                    .Include(s => s.Seats)
                    .FirstOrDefaultAsync(s => s.id == scheduleModel.id);

                for (int row = 0; row < hall.RowsOfSeat; row++)
                {
                    for (int seat = 0; seat < hall.SeatsOnRow; seat++)
                    {
                        SeatModel seatModel = new SeatModel()
                        {
                            RowName = $"{currentLetter}",
                            RowNumber = seat + 1,
                            ScheduleID = savedSchedule.id,
                        };

                        savedSchedule.Seats.Add(seatModel);
                    }

                    currentLetter++;
                }

                await _context.SaveChangesAsync();

                hall.Schedules.Add(savedSchedule);
                await _context.SaveChangesAsync();

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string, ScheduleModel, MovieModel, List<TicketModel>)> GetMovieAndScheduleByID(int id)
        {
            try
            {
                var schedule = await _context.Set<ScheduleModel>()
                    .Include(m => m.Date)
                    .Include(m => m.Seats)
                    .Include(m => m.Tickets)
                    .FirstOrDefaultAsync(x => x.id == id);

                if (schedule != null)
                {
                    //(bool result, string message, var tickets) = await _TicketRepository.GetAllWithScheduleID(schedule.id);
                    (bool result, string message, var movie) = await _MovieRepository.GetWithId(schedule.MovieId);
                    if (result) return (true, "", schedule, movie, null);

                    else return (false, message, null, null, null);
                }

                else return (false, "No Schedule By That ID in Db", null, null, null);
            }
            catch (Exception ex) { return (false, ex.Message, null, null, null); }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var schedule = await _context.Set<ScheduleModel>()
                    .Include(m => m.Date)
                    .Include(m => m.Seats)
                    .FirstOrDefaultAsync(x => x.id == id);

                if (schedule == null) return (false, "No Schedule in Database with that id");

                var hall = await _context.CinemaHall
                    .Include(h => h.Schedules)
                    .FirstOrDefaultAsync(h => h.id == schedule.HallId);


                var date = await _context.Dates
                    .FirstOrDefaultAsync(x => x.id == schedule.Date.id);

                ICollection<SeatModel> seats = await _context.Seats
                    .Where(s => s.ScheduleID == schedule.id)
                    .ToListAsync();

                if (seats.Count() > 0)
                {
                    foreach(SeatModel seat in seats)
                    {
                        _context.Seats.Remove(seat);
                    }
                }

                if (date != null) { _context.Dates.Remove(date); }

                hall.Schedules.Remove(schedule);

                _context.Schedules.Remove(schedule);

                await _context.SaveChangesAsync();

                return (true, "");

            }

            catch(Exception ex) { return (false, ex.Message); }
        }
    }
}
