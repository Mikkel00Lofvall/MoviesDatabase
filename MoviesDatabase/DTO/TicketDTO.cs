using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class TicketDTO
    {
        public int MovieID { get; set; }
        public int HallID;
        public int SeatID { get; set; }
    }
}
