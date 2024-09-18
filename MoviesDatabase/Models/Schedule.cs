using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class Schedule
    {
        public int id { get; set; }
        public MovieModel Movie { get; set; }
        public DateTime PlayTime { get; set; }
    }
}
