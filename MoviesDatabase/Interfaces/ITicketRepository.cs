using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    internal interface ITicketRepository
    {
        Task<(bool, string)> Create(TicketModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, TicketModel)> GetWithId(int id);
        Task<(bool, string, ICollection<TicketModel>)> GetAll();
    }
}
