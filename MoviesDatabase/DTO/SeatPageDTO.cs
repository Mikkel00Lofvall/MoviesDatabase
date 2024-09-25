using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public  class SeatPageDTO
    {
        public MovieDTO Movie { get; set; }

        public ScheduleDTO Schedule { get; set; }

        public HallDTO Hall { get; set; }
        
    }
}
