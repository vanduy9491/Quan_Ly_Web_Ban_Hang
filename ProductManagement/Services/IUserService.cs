using ProductManagement.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);
        void Signout();
        Task<RegisterResult> Register(Register register);
    }
}
