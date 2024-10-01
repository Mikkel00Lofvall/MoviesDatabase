using Microsoft.VisualBasic;
using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class ScheduleModel : IEntity
    {
        public int id { get; set; }

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public MovieModel Movie { get; set; }
        public DateModel Date { get; set; }

        public int HallId { get; set; }

        public ICollection<SeatModel> Seats { get; set; } = new List<SeatModel>();

        public ICollection<TicketModel> Tickets { get; set; } = new List<TicketModel>();

    }
}
