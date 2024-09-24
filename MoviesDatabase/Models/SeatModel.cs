using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class SeatModel : IEntity
    {
        public int id { get; set; }

        public bool IsTaken { get; set; }

        public string RowName {  get; set; }

        public int RowNumber { get; set; }

    }
}
