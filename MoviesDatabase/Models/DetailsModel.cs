using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class DetailsModel
    {
        [JsonIgnore]
        public int id {  get; set; }

        public string ReleaseDate { get; set; }
        public string? Rating { get; set; }

        public int DurationInMinutes { get; set; }

        public string DirectedBy {  get; set; }

        public string Studio { get; set; }
    }
}
