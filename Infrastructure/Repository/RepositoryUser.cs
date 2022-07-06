using Domain.Services;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryUser : IUser
    {
        private readonly DbContextOptions<Configuration.Context> _optionsBuilder;
        public RepositoryUser()
        {
            _optionsBuilder = new DbContextOptions<Configuration.Context>();
        }

        public async Task<bool> AddUser(string name, string email, string password)
        {
            try
            {

                using (var date = new Configuration.Context(_optionsBuilder))
                {
                    await date.User.AddAsync(new User
                    {
                        Name = name,
                        Email = email,
                        Password = password,
                        CreatedDate = DateTime.Now
                    });
                    await date.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
