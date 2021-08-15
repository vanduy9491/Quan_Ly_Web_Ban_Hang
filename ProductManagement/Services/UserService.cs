using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DBContexts;
using ProductManagement.Entities;
using ProductManagement.Models.Account;
using ProductManagement.Models.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ProductDBContext context;

        public UserService(UserManager<AppIdentityUser> userManager,
                            SignInManager<AppIdentityUser> signInManager,
                            RoleManager<IdentityRole> roleManager,
                            ProductDBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        public  Task<List<AppIdentityUser>> GetUsers()
        {
            var users = context.Users.ToListAsync();
            return users;
        }

        public async Task<LoginResult> Login(Login LoginUser)
        {
            var user = await userManager.FindByNameAsync(LoginUser.Email);
            if (user == null)
            {
                return new LoginResult()
                {
                    UserId = string.Empty,
                    Email = string.Empty,
                    Message = "User is not existing."
                };
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.RememberMe, false);
            if (signInResult.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                return new LoginResult()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Message = "Login successed",
                    Roles = roles.ToArray()
                };
            }
            return new LoginResult()
            {
                UserId = string.Empty,
                Email = string.Empty,
                Message = "Password went wrong, please try again"
            };
        }
        public async Task<RegisterResult> Register(Register register)
        {
            var registerResult = new RegisterResult();
            var newUser = new AppIdentityUser()
            {
                UserName = register.Email,
                Email = register.Email,
                NormalizedEmail = register.Email,
                NormalizedUserName = register.Email,
                LockoutEnabled = false,
                Avatar = "/Images/avatars/no_avatar.png"
            };
            var user = await userManager.CreateAsync(newUser, register.Password);

            if (user.Succeeded)
            {
                var registerUser = await userManager.FindByEmailAsync(register.Email);
                var assignUserRoles = await userManager.AddToRoleAsync(newUser, "Customer");
                if (assignUserRoles.Succeeded)
                {
                    registerResult.Message = "Rgister succeed";
                    registerResult.UserId = registerUser.Id;
                }
            }

            foreach(IdentityError error in user.Errors)
            {
                registerResult.Message += $"<p>{error.Description}</p>";
            }
            return registerResult;
        }
        public void Signout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}
