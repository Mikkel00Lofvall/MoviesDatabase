using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class TicketModel : IEntity
    {
        public int id {  get; set; }
        public int Price {get; set; }

        public int SeatID { get; set; }

        public int ScheduleID { get; set; }

        public int DateID { get; set; }

        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}
