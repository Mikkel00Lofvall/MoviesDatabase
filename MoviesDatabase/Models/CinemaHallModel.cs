using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class CinemaHallModel : IEntity
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<int>? SchedulesIDs { get; set; }

        [ForeignKey("SchedulesIDs")]
        public ICollection<ScheduleModel>? Schedules { get; set; } = new List<ScheduleModel>();

        public ICollection<SeatModel> Seats { get; set; } = new List<SeatModel>();

        public int SeatsOnRow { get; set; }
    }
}
