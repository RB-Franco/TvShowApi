using Infrastructure.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TvShowTest
{
    public class UserRepositoryTestDataLoader
    {
        private readonly Context _context;

        public UserRepositoryTestDataLoader(Context context)
        {
            _context = context;
        }

        public async Task LoadData()
        {
            await LoadAllUsers();
        }

        private async Task LoadAllUsers()
        {
            var users = UserFake.AllUsers;
            var userRepository = new UserRepository(_context);
            await userRepository.AddRangeAsync(users);
        }
    }
}
