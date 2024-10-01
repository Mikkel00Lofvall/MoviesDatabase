using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class ScheduleDTO
    {
        public int id { get; set; }
        public DateDTO date { get; set; }

        public int MovieID { get; set; }

        public ICollection<SeatModel> Seats { get; set; }

        public ICollection<TicketModel> Tickets { get; set; }
    }
}
