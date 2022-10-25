using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Account;
using Market.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel);
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel registerViewModel);
    }
}
