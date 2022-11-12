using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Helpers;
using Market.Domain.Response;
using Market.Domain.ViewModels.Account;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Implementatins
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Role> roleRepository;

        public AccountService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel)
        {
            var baseResponse = new BaseResponse<ClaimsIdentity>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == loginViewModel.Name);
                if (user == null)
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };

                if (user.Password != HashPasswordHelper.GetHashPassword(loginViewModel.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный логин или пароль",
                    };
                }

                var id = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = id,
                    Description = "Пользователь найден",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel registerViewModel)
        {

            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == registerViewModel.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже существует"
                    };
                }

                var cart = new Cart() { };

                user = new User()
                {
                    Name = registerViewModel.Name,
                    Password = HashPasswordHelper.GetHashPassword(registerViewModel.Password),
                    Cart = cart
                };


                var userRole = await roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "user");

                if (userRole != null)
                    user.Role = userRole;



                await userRepository.Create(user);

                user.Cart.UserId = user.Id;
                await userRepository.Update(user);


                var id = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = id,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        }
    }
}
