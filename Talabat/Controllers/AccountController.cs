using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Services;
using Talabat.DTO;
using Talabat.Errors;
using Talabat.Extensions;

namespace Talabat.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapping;

        public AccountController(UserManager<AppUser> userManager ,
            SignInManager<AppUser> signInManager ,
            ITokenService tokenService,
            IMapper mapping)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapping = mapping;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var user= await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var login= await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);

            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await _tokenService.CreatToken(user,_userManager)
            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (CheckEmailExists(registerDTO.Email).Result)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] {"This Email is already Used"}});
            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email.Split('@')[0]
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreatToken(user, _userManager)
            });
        }
        [Authorize]
        [HttpGet] // GET : api/Account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName= user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreatToken(user, _userManager)
            });

        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updateAddress)
        {
             var Address=  _mapping.Map<AddressDto, Address>(updateAddress);
            var appUser = await _userManager.FindUserEithAddress(User);
            appUser.Address = Address;
            var result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "An Error Occured during Update Address"));
            return Ok(_mapping.Map< Address, AddressDto>(appUser.Address));
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> GetAddress()
        {
            var userApp =await _userManager.FindUserEithAddress(User);
            return Ok(_mapping.Map<Address, AddressDto>(userApp.Address));
        }
        [HttpGet("emailexists")]
        public async  Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email)!=null;
        }
    }
}
