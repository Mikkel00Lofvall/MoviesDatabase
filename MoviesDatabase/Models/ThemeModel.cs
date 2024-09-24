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
    public class ThemeModel : IEntity
    {
        [JsonIgnore]
        public int id { get; set; }
        public string Name { get; set; }

        
        public List<int>? MovieIDs { get; set; }

        [ForeignKey("MovieIDs")]
        public List<MovieModel> Movies { get; set; } = new List<MovieModel>();

    }
}
