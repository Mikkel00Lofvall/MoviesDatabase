﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class CinemaHallRepository : Repository<CinemaHallModel>, ICinemaHallRepository
    {

        public ContextDB _context { get; }

        public CinemaHallRepository(ContextDB context) : base(context)
        {
            _context = context;
        }

        public async Task<(bool, string, CinemaHallModel)> GetHallBySchedule(int scheduleId)
        {
            if (scheduleId != null) 
            {
                try
                {
                    var result = await _context.Set<CinemaHallModel>()
                        .FirstOrDefaultAsync(x => x.Schedules.Any(s => s.id == scheduleId));

                    if (result != null)
                    {
                        return (true, "", result);
                    }

                    else return (false, "No CinemaHall With That ID in DB", null);

                }

                catch (Exception ex) { return (false, ex.Message, null); }

            }

            else { return (false, "No Schedule ID Givin", null); }
        }


        public override async Task<(bool, string)> Create(CinemaHallModel cinema)
        {
            (bool result, string message, ICollection<CinemaHallModel> halls) = await GetAll();

            if (!result) return (false, message);
            bool found = halls.Any(x => x == cinema);
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


        public async Task<(bool, string)> Delete(int CinemaHallID)
        {
            try
            {
                var hall = await _context.Set<CinemaHallModel>()
                    .Include(m => m.Schedules)
                    .FirstOrDefaultAsync(x => x.id == CinemaHallID);

                if (hall == null) return (false, "No Cinema Room in Database with that id");

                ICollection<ScheduleModel> schedules = await _context.Schedules
                    .Where(x => x.HallId == CinemaHallID)
                    .ToListAsync();

                foreach (ScheduleModel schedule in schedules)
                {
                    _context.Schedules.Remove(schedule);
                    ICollection<SeatModel> seats = await _context.Seats
                        .Where(s => s.ScheduleID == schedule.id)
                        .ToListAsync();

                    foreach (SeatModel seat in seats)
                    {
                        _context.Seats.Remove(seat);
                    }
                }

                _context.CinemaHall.Remove(hall);


                await _context.SaveChangesAsync();

                return (true, "");

            }

            catch (Exception ex) { return (false, ex.Message); }
        }
    }
}
