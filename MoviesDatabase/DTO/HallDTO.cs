using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class HallDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int SeatsOnRow { get; set; }
    }
}
