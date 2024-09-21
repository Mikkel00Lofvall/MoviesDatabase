using MoviesDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class ActorModel
    {
        [JsonIgnore]
        public int id { get; set; }
        public PersonModel Person { get; set; }
    }
}
