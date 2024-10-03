using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    internal interface ICinemaHallRepository
    {
        Task<(bool, string)> Create(CinemaHallModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, CinemaHallModel)> GetWithId(int id);
        Task<(bool, string, ICollection<CinemaHallModel>)> GetAll();
    }
}
