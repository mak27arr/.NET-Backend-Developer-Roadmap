using DTO.Identity;
using Identity.Infrastructure;
using Identity.Interfaces;
using Microsoft.AspNet.Identity;
using myCloudDAL.DAL.Entities.Identity;
using myCloudDAL.DAL.Interface;
using System.Security.Claims;

namespace Identity.Services
{
    internal class UserService : IUserService
    {
        readonly IUnitOfWork _database;

        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            AppUser user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new AppUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                
                await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name };
                _database.ClientManager.Create(clientProfile);
                await _database.SaveAsync();
                return new OperationDetails(true, "Registered", "");
            }
            else
            {
                return new OperationDetails(false, "Exist", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            AppUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            
            if (user != null)
                return await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            
            return null;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new AppRole { Name = roleName };
                    await _database.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        public void Dispose() => _database.Dispose();
    }
}
