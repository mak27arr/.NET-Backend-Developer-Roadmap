using Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public abstract class BaseController : Controller
    {
        private readonly IUserService _userService;
        protected readonly AutoMapper.IConfigurationProvider _mapperConfig;

        protected BaseController(IUserService userService, AutoMapper.IConfigurationProvider mappConfig)
        {
            _userService = userService;
            _mapperConfig = mappConfig;
        }

        protected ActionResult<T> ExecuteWithRezalt<T>(Func<T> func)
        {
            try
            {
                var rezalt = func.Invoke();
                return Ok(rezalt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        protected async Task<Guid> GetUserId() => await _userService.GetUserId(User?.Identity?.Name);
    }
}
