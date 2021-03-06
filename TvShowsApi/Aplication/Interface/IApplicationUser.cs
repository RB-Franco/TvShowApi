using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationUser
    {
        Task<bool> AddUser(string name, string email, string password);
        Task<bool> ValidateUser(string email, string password);
        Task<string> ReturnIdUser(string email);
    }
}
