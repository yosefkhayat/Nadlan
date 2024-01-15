using API.DTOs;
using API.Services;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.Include(p=> p.Photos)
                .FirstOrDefaultAsync(x=> x.Email == loginDto.Email);
            
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
                return CreateUserObject(user, roles);
            return Unauthorized();
        }

        [HttpPost("reg")]
        public async Task<ActionResult<UserDto>> Reg()
        {

            var adminRole = new IdentityRole("Admin");

            var adminUser = new AppUser
            {
                DisplayName = "Admin1",
                UserName = "admin1@test.com",
                Email = "admin1@test.com",
                Bio = "admin test1"
            };
            var result = await _userManager.CreateAsync(adminUser, "Pa$$w0rd");
            var resRole = await _userManager.AddToRoleAsync(adminUser, adminRole.Name);

            if (result.Succeeded && resRole.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(adminUser);
                return CreateUserObject(adminUser, roles);
            }
            return BadRequest("Problem registering user");

        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                ModelState.AddModelError("email", "Email is taken");
                return ValidationProblem();
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                ModelState.AddModelError("userName", "Username is taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Bio = registerDto.Bio
            };


            var result = await _userManager.CreateAsync(user, registerDto.Password);
            var resRole = await _userManager.AddToRoleAsync(user, "User");

            if (result.Succeeded && resRole.Succeeded) { 
            var roles = await _userManager.GetRolesAsync(user);
            return CreateUserObject(user, roles);
            }
            return BadRequest("Problem registering user");

        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
            var roles = await _userManager.GetRolesAsync(user);

            return CreateUserObject(user,roles);
        }


        private UserDto CreateUserObject(AppUser user,IList<string> roles)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = user?.Photos?.FirstOrDefault(x=> x.IsMain)?.Url,
                Token = _tokenService.CreateToken(user,roles),
                Username = user.UserName,
                Roles = roles.ToArray(),
                Email = user.Email
            };
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Unit>> Delete(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.UserName == username);
            if (user == null) return NotFound();
            if (await _userManager.IsInRoleAsync(user,"Admin"))
            {
                ModelState.AddModelError("admin", "Can't Delete Admin!");
                return ValidationProblem();
            }

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return Ok();
            return BadRequest(result.Errors);
        }


        [HttpGet("listusers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserDto> userDtos = new List<UserDto>();
            foreach(AppUser user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = CreateUserObject(user,roles);
                userDtos.Add(userDto);
            }
            
           
            return userDtos;
        }

    }

}
