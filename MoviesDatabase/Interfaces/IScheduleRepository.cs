using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    internal interface IScheduleRepository
    {
        Task<(bool, string)> Create(ScheduleModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, ScheduleModel)> GetWithId(int id);
        Task<(bool, string, ICollection<ScheduleModel>)> GetAll();
    }
}
