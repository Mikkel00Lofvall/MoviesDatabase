using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class CinemaHallModel : IEntity
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
