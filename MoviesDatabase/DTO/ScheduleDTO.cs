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
        public DateModel date { get; set; }

        public int MovieID { get; set; }
    }
}
