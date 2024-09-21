using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DetailsModel Details { get; set; }

        public List<int>? ActorIDs { get; set; }

        // Navigation Property for MovieModel (optional if you need access to full Movie details in code)
        [ForeignKey("ActorIDs")]
        public List<ActorModel>? Actors { get; set; }

        public List<ImageBlobModel>? ImagesBlobs { get; set; }

        public ImageBlobModel FrontPageImage { get; set; } 

        [JsonIgnore]
        public ICollection<ThemeModel>? Themes { get; set; }

        public string? TrailerLink { get; set; }


    }
}
