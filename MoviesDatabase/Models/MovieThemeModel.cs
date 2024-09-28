using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class MovieThemeModel : IEntity
    {
        public int id { get; set; }
        public int MovieID { get; set; }
        public int ThemeID { get; set; }
    }
}
