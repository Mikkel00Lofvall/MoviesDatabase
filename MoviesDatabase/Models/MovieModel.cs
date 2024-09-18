using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class MovieModel : IEntity
    {
        [JsonIgnore]
        public int id { get; set; }

        public int key => id;

        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }

        [JsonIgnore]
        public List<ActorModel>? Actors { get; set; }

        public List<ImageBlobModel> ImagesBlobs { get; set; }

        public ImageBlobModel FrontPageImage { get; set; } 

        [JsonIgnore]
        public ICollection<ThemeModel>? Themes { get; set; }

        public string? TrailerLink { get; set; }


    }
}
