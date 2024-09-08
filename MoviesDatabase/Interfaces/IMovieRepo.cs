using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models;

namespace MoviesDatabase.Interfaces
{
    public interface IMovieRepo
    {
        public Task<IEnumerable<MovieModel>> GetAllMovies();
        public Task<MovieModel> GetMovieById(int id);
        public Task<bool> CreateMovie(MovieModel movie);
        public Task<bool> DeleteMovie(int id);

    }
}
