using Microsoft.EntityFrameworkCore;
using MoviesDatabase.DTO;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class AdminUserRepository : Repository<AdminUserModel>, IAdminUserRepository
    {
        public AdminUserRepository(ContextDB context) : base(context) { }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var user = await _context.Set<AdminUserModel>()
                    .FirstOrDefaultAsync(u => u.id == id);

                _context.Remove(user);

                await _context.SaveChangesAsync();

                return (true, "");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public async Task<(bool, string)> GetAdminUser(string username, string password)
        {
            try
            {
                var adminUser = await _context.Set<AdminUserModel>()
                            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (adminUser != null)
                {
                    return (true, "Admin user found.");
                }

                return (false, "Admin user not found.");

            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public async Task<(bool, string, ICollection<AdminUserGetDTO>)> GetAll()
        {
            try
            {
                ICollection<AdminUserGetDTO> DTOs = [];

                var users = await _context.Set<AdminUserModel>()
                    .ToListAsync();

                foreach (var user in users)
                {
                    AdminUserGetDTO adminUserGetDTO = new()
                    {
                        Id = user.id,
                        Username = user.Username
                    };

                    DTOs.Add(adminUserGetDTO);
                }

                return (true, "", DTOs);
            }
            catch (Exception ex) { return (true, ex.Message, null);  }
        }
    }
}
