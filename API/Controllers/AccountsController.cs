using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using DogTrainer.Domain;
using MediatR;
using DogTrainer.Application.Features.AppUser.Queries;
using DogTrainer.Application.Features.AppUser.Command;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountsController(IMediator mediator, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            this._mediator = mediator;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // try to get the user with this email
            var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.UserName == userLogin.UserName);

            if(user is null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Versuche, den Benutzer mit dem angegebenen Passwort anzumelden
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if(!result.Succeeded)
            {
                return Unauthorized("Ungültige Anmeldedaten");
            }

            // JWT-Token mit dem TokenService generieren
            var token = _tokenService.CreateToken(user);
            var loggedInUser = new RegistredUser
            {
                Email = user.Email,
                Token = token
            };
            return Ok(loggedInUser);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var existingUser = await _mediator.Send(new GetUserQuery.Query { RegisterDto = userRegister });
            var existingUser = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.Email.Equals(userRegister.Email));

            if(existingUser != null)
            {
                return BadRequest("User already exists");
            }

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            var appUser = new AppUser
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                PasswordHash = userRegister.Password
            };
            //userRegister.Password = hashedPassword;
            var result = await _signInManager.UserManager.CreateAsync(appUser, userRegister.Password);

            if(result.Succeeded)
            {
                // create jwt token
                var token = _tokenService.CreateToken(appUser);
                var registredUser = new RegistredUser
                {
                    Email = appUser.Email,
                    Token = token
                };

                return Ok(registredUser);
            }
            return BadRequest("Registrierung nicht erfolgreich");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery.Query { Id = id });
            if(user == null)
            {
                return BadRequest("User can not be deleted, user not found");
            }

            await _mediator.Send(new DeleteUserCommand.Command(id));
            return NoContent();
        }
    }
}
