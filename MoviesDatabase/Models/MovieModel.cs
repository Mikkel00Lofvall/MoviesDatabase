using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class MovieModel : IEntity
    {
        public int id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }

        public List<ActorModel> Actors { get; set; }

        public CinemaHallModel CinemaHall { get; set; }
    }
}
