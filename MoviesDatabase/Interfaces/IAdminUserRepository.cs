using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Interfaces
{
    internal interface IAdminUserRepository
    {
        Task<(bool, string)> Create(AdminUserModel model);
        Task<(bool, string)> Delete(int id);
        Task<(bool, string, AdminUserModel)> GetWithId(int id);
        Task<(bool, string, ICollection<AdminUserGetDTO>)> GetAll();
    }
}
