using Microsoft.EntityFrameworkCore;
using MoviesDatabase.DTO;
using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoviesDatabase.Repos
{
    public class AdminUserRepository : Repository<AdminUserModel>, IAdminUserRepository
    {
        private readonly HasherService _hasherService;
        public AdminUserRepository(ContextDB context, HasherService hash) : base(context) 
        { 
            _hasherService = hash;
        }

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
                            .FirstOrDefaultAsync(u => u.Username == username);

                (bool result, string message) = await _hasherService.Verify(username, adminUser.Password, password);

                if (result == true)
                {
                    return (true, "Admin user found.");
                }

                return (false, message);

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

        public async Task<(bool, string)> Create(string username, string password)
        {
            try
            {
                (bool result, string message, ICollection<AdminUserGetDTO> admins) = await GetAll();

                if (!result) return (false, message);
                bool found = admins.Any(x => x.Username == username);
                if (!found)
                {
                    try
                    {
                        (result, message, AdminUserModel newUser) = await _hasherService.RegisterUser(username, password);
                        if (result != true)
                        {
                            return (false, message);
                        }
                        _context.Set<AdminUserModel>().Update(newUser);
                        await _context.SaveChangesAsync();

                        return (true, "");
                    }
                    catch (Exception ex)
                    {
                        return (false, $"Failure: {ex}");
                    }
                }
                else
                {
                    return (false, $"Failure: Already Exist in db!");
                }

            }
            catch (Exception ex) { return (true, ex.Message); }
        }
    }
}
