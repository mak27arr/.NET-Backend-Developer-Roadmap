using DTO.Identity;
using Identity.Infrastructure;
using System.Security.Claims;

namespace Identity.Interfaces
{
    internal interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
