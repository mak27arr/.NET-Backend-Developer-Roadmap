using DTO.Identity;
using Identity.Infrastructure;
using System.Security.Claims;

namespace Identity.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<AuthResult> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto);
    }
}
