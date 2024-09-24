using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class SchedulesDTO
    {
        public int MovieId { get; set; }
        public DateModel Date { get; set; }

        public int HallId { get; set; }
    }
}
