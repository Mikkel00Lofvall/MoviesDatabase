using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class Seat : IEntity
    {
        public int id { get; set; }

        public bool isTaken { get; set; }

        public CinemaHallModel CinemaHallModel { get; set; }
    }
}
