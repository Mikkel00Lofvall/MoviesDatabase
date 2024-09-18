using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class CinemaHallModel : IEntity
    {
        [JsonIgnore]
        public int id { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
