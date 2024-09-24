using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.DTO
{
    public class MovieDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DetailsModel Details { get; set; }

        public List<ImageBlobModel>? ImagesBlobs { get; set; }

        public ImageBlobModel FrontPageImage { get; set; }

        public string? TrailerLink { get; set; }
    }
}
