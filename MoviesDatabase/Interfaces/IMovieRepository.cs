using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    interface IMovieRepository : IRepository<MovieModel>
    {
        Task<(bool, string)> Create(MovieModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, MovieModel)> GetWithId(int id);
        Task<(bool, string, ICollection<MovieModel>)> GetAll();
    }
}
