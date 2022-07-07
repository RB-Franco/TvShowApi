using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {
        Task<bool> AddUser(string name, string email, string password);
        Task<bool> ValidateUser(string email, string password);
    }
}
