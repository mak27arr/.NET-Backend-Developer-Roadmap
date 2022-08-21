using AutoMapper;
using DTO.Identity;
using Identity.Helper;
using Identity.Infrastructure;
using Identity.Interfaces;
using Microsoft.IdentityModel.Tokens;
using myCloudDAL.DAL.Entities.Identity;
using myCloudDAL.DAL.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        readonly IUnitOfWork _database;
        private readonly AuthConfig _authConf;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, AuthConfig configuration, IMapper mapper)
        {
            _database = uow;
            _authConf = configuration;
            _mapper = mapper;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = _mapper.Map<AppUser>(userDto);
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, String.Join(" ", result.Errors), "");

                if (string.IsNullOrEmpty(userDto.Role))
                    userDto.Role = nameof(Roles.User);

                await _database.UserManager.AddToRoleAsync(user, userDto.Role);
                var clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name };
                _database.ClientManager.Create(clientProfile);
                await _database.SaveAsync();
                return new OperationDetails(true, "Registered", "");
            }
            else
            {
                return new OperationDetails(false, "Exist", "Email");
            }
        }

        public async Task<AuthResult> Authenticate(UserDTO userDto)
        {
            ;
            var user = !string.IsNullOrEmpty(userDto.UserName) ? await _database.UserManager.FindByNameAsync(userDto.UserName) : await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null && await _database.UserManager.CheckPasswordAsync(user, userDto.Password))
            {
                var userRoles = await _database.UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(24), claims: authClaims, signingCredentials: new SigningCredentials(_authConf.IssuerSigningKey, SecurityAlgorithms.HmacSha256));
            
                return new AuthResult(true, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user, userRoles, "User Login Successfully");
            }

            return new AuthResult(false, null, DateTime.MinValue, null, null, "Error");
        }

        public async Task SetInitialData(UserDTO adminDto)
        {
            foreach (var appRole in Enum.GetValues(typeof(Roles)))
            {
                var role = await _database.RoleManager.FindByNameAsync(appRole.ToString());

                if (role != null)
                    continue;

                role = new AppRole { Name = appRole.ToString() };
                await _database.RoleManager.CreateAsync(role);

            }

            if (adminDto != null)
                await Create(adminDto);
        }

        public void Dispose() => _database.Dispose();
    }
}
