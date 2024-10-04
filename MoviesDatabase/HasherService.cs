using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoviesDatabase.Models;

namespace MoviesDatabase
{
    public class HasherService
    {
        private readonly PasswordHasher<string> _passwordHasher;

        public HasherService()
        {
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<(bool, string, AdminUserModel)> RegisterUser(string username, string password)
        {
            try
            {
                string hasedPassword = _passwordHasher.HashPassword(username, password);

                AdminUserModel adminUser = new AdminUserModel()
                {
                    Password = hasedPassword,
                    Username = username,
                };

                return (true, "", adminUser);
            }

            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }

        }

        public async Task<(bool, string)> Verify(string email, string hashed, string password)
        {
            try
            {
                var result = _passwordHasher.VerifyHashedPassword(email, hashed, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return (true, "");
                }

                return (false, "No Admin User Verified");
            }
            catch(Exception ex) { return (false, ex.Message); }

        }
    }
}
