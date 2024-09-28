using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class AdminUserRepository : Repository<AdminUserModel>
    {
        public AdminUserRepository(ContextDB context) : base(context) { }

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
    }
}
