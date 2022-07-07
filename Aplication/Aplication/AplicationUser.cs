using Aplication.Interface;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Aplication.Aplication
{
    public class AplicationUser : IAplicationUser
    {
        IUser _IUser;

        public AplicationUser(IUser IUser)
        {
            _IUser = IUser;
        }

        public async Task<bool> AddUser(string name, string email, string password)
        {
            return await _IUser.AddUser(name, email, password);
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            return await _IUser.ValidateUser(email, password);
        }
    }
}
