﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;
using MoviesDatabase.DTO;
using MoviesDatabase.Interfaces;
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
        private readonly IRepository<CinemaHallModel> CinemaHallRepository;
        public ScheduleRepository(ContextDB context, MovieRepository movieRepo, IRepository<CinemaHallModel> cinmaRepo) : base(context) 
        {
            this.MovieRepository = movieRepo;
            this.CinemaHallRepository = cinmaRepo;
        }

        public async Task<(bool, string, IEnumerable<ScheduleModel>)> GetAll()
        {
            try
            {
                var schedules = await _context.Set<ScheduleModel>()
                    .Include(m => m.Date)
                    .ToListAsync();

                if (schedules.Count > 0)
                {
                    return (true, "", schedules);
                }

                else
                {
                    return (false, "No Schedules in DB", null);
                }


            }
            catch (Exception ex) 
            {
                return (false, ex.Message, null);
            }

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

        public async Task<(bool, string)> CreateScheduleAndInsertIntoHall(SchedulesDTO schedule)
        {
            try
            {
                ScheduleModel scheduleModel = new ScheduleModel()
                {
                    MovieId = schedule.MovieId,
                    Date = schedule.Date,
                    HallId = schedule.HallId,
                };

                _context.Schedules.Add(scheduleModel);
                await _context.SaveChangesAsync();

                var hall = await _context.CinemaHall
                              .Include(h => h.Schedules)
                              .FirstOrDefaultAsync(h => h.id == schedule.HallId);

                if (hall != null)
                {
                    hall.Schedules.Add(scheduleModel);
                    await _context.SaveChangesAsync();
                    return (true, "");
                }

                else
                {
                    return (false, "No Hall With That ID");
                }
            }
            catch (Exception ex) 
            {
                return (false, ex.Message);
            }

        }

        /*public async Task<(bool, string)> UpdateMovieWithSchedule(int movieID, DateModel date)
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
        } */

    }
}
