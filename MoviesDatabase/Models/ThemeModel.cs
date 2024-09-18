using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class ThemeModel : IEntity
    {
        [JsonIgnore]
        public int id { get; set; }
        public string? Name { get; set; }

        public ICollection<MovieModel> Movies { get; set; } = new List<MovieModel>();

    }
}
