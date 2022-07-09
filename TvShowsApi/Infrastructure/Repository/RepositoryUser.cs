using Domain.Interfaces;
using Entity.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryUser : IUser
    {
        private readonly DbContextOptions<Context> _optionsBuilder;
        public RepositoryUser()
        {
            _optionsBuilder = new DbContextOptions<Context>();
        }

        public async Task<bool> AddUser(string name, string email, string password)
        {
            try
            {

                using (var context = new Context(_optionsBuilder))
                {
                    await context.User.AddAsync(new User
                    {
                        Name = name,
                        Email = email,
                        PasswordHash = password,
                        CreatedDate = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<string> ReturnIdUser(string email)
        {
            try
            {

                using (var context = new Context(_optionsBuilder))
                {
                    var user = await context.User
                                            .Where(x => x.Email.Equals(email))
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
                    return user.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            try
            {

                using (var context = new Context(_optionsBuilder))
                {
                    return await context.User.Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(password)).AsNoTracking().AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
