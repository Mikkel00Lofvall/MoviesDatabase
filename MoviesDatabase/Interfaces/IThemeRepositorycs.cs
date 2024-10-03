using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    internal interface IThemeRepository
    {
        Task<(bool, string)> Create(ThemeModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, ThemeModel)> GetWithId(int id);
        Task<(bool, string, ICollection<ThemeModel>)> GetAll();
    }
}
