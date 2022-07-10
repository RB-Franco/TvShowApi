using Infrastructure.Configuration;
using Infrastructure.Repository;
using System.Threading.Tasks;
using TvShowTest.Fakes;

namespace TvShowTest.DataLoader
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
            await LoadUser();
        }

        private async Task LoadUser()
        {
            var user = UserFake.FakeUser01;
            var userRepository = new RepositoryUser();
            await userRepository.AddUser(user.Name, user.Email, user.PasswordHash);
        }
    }
}
