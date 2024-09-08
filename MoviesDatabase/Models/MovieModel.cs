using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int DurationInMinutes { get; set; }
    }
}
