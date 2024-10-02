using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class TicketCreateDTO
    {
        public int ScheduleID { get; set; }
        public int SeatID { get; set; }
        public int DateID { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}
