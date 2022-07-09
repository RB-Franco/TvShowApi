using Application.Interface;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Application.Application
{
    public class ApplicationUser : IApplicationUser
    {
        IUser _IUser;

        public ApplicationUser(IUser IUser)
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

        public async Task<string> ReturnIdUser(string email)
        {
            return await _IUser.ReturnIdUser(email);
        }

    }
}
