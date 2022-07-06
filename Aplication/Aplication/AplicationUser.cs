using Aplication.Interface;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
