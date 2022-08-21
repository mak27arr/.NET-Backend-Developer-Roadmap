using DTO.Identity;
using Identity.Infrastructure;

namespace Identity.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<AuthResult> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto);
    }
}
