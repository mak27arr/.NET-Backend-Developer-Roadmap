using API.Helper.Mapper;
using API.ViewModel.Auth;
using AutoMapper;
using DTO.Identity;
using Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security;


namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly Mapper _mapper;

        public AuthController(IUserService userService)
        {
            this._userService = userService;
            _mapper = new Mapper(MapConfig.AuthConfig());
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            var user = _mapper.Map<UserDTO>(registerVM);
            var rez = await _userService.Create(user);

            if (rez.Succedeed) 
            {
                return Ok(new
                {
                    Status = "Success",
                    Message = rez.Message
                });
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = rez.Message
                });
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            var user = _mapper.Map<UserDTO>(loginVM);
            var claim = await _userService.Authenticate(user);

            if (claim.Sacsseses)
            {
                return Ok(claim);
            }
            else
            {
                ModelState.AddModelError("", "Fail");
                return Unauthorized(claim.Status);
            }
        }
    }
}
