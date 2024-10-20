﻿using Microsoft.EntityFrameworkCore;
using MoviesDatabase.DTO;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class TicketRepository : Repository<TicketModel>, ITicketRepository
    {
        private readonly ScheduleRepository _ScheduleRepository;
        public TicketRepository(ContextDB context, ScheduleRepository scheduleRepo) : base(context) 
        {
            _ScheduleRepository = scheduleRepo;
        }

        public async Task<(bool, string, List<TicketDTO>)> GetAllWithScheduleID(int scheduleID)
        {
            try
            {
                List<TicketDTO> ticketDTOs = new List<TicketDTO>();
                var Tickets = await _context.Set<TicketModel>()
                    .Where(t => t.ScheduleID == scheduleID)
                    .ToListAsync();

                foreach (TicketModel ticket in Tickets)
                {
                    TicketDTO ticketDTO = new TicketDTO()
                    {
                        DateID = ticket.DateID,
                        ScheduleID = ticket.ScheduleID,
                        SeatID = ticket.SeatID,
                        id = ticket.id,
                    };

                    ticketDTOs.Add(ticketDTO);
                }


                return (true, "", ticketDTOs);
            }

            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        } 

        public virtual async Task<(bool, string)> Create(TicketModel ticket)
        {
            try
            {
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                var schedule = await _context.Set<ScheduleModel>()
                    .Include(s => s.Tickets)
                    .FirstOrDefaultAsync(s => s.id == ticket.ScheduleID);

                if (schedule == null) return (false, "Schedule not found");

                schedule.Tickets.Add(ticket);

                await _context.SaveChangesAsync();

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public virtual async Task<(bool, string)> MultiCreate(ICollection<TicketModel> tickets)
        {
            if (tickets.Count() > 0)
            {
                foreach (TicketModel ticket in tickets)
                {
                    Create(ticket);
                }

                return (true, "");
            }

            else return (false, "No Tickets");
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var ticket = await _context.Set<TicketModel>()
                    .FirstOrDefaultAsync(t => t.id == id);

                var schedule = await _context.Set<ScheduleModel>()
                    .FirstOrDefaultAsync(s => s.Tickets.Contains(ticket));

                if (schedule != null) schedule.Tickets.Remove(ticket);

                _context.Remove(ticket);

                await _context.SaveChangesAsync();

                return (true, "");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }
    }
}
