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

        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        public MovieModel Movies { get; set; }
        public int SeatID { get; set; }

        [ForeignKey("SeatID")]
        public SeatModel Seat { get; set; }
        public int HallID { get; set; }

        [ForeignKey("HallID")]
        public SeatModel Hall { get; set; }

    }
}
