using DogTrainer.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using DogTrainer.Domain;
using MediatR;
using DogTrainer.Application.Features.AppUser.Queries;
using DogTrainer.Application.Features.AppUser.Command;
using Microsoft.AspNetCore.Identity;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Login per Username
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            if (user is null)
            {
                return Unauthorized("Ungültige Anmeldedaten");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, lockoutOnFailure: false);

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
            // Prüfe E-Mail und Benutzername
            var existingByEmail = await _userManager.FindByEmailAsync(userRegister.Email);
            if (existingByEmail != null)
            {
                return BadRequest("E-Mail ist bereits registriert.");
            }
            var existingByName = await _userManager.FindByNameAsync(userRegister.UserName);
            if (existingByName != null)
            {
                return BadRequest("Benutzername ist bereits vergeben.");
            }

            var appUser = new AppUser
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email
                // KEIN PasswordHash setzen – CreateAsync übernimmt das Hashing
            };

            var result = await _userManager.CreateAsync(appUser, userRegister.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Registrierung fehlgeschlagen.");
            }

            var tokenReg = _tokenService.CreateToken(appUser);
            var registredUser = new RegistredUser
            {
                Email = appUser.Email,
                Token = tokenReg
            };
            return Ok(registredUser);
           
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery.Query { Id = id });
            if (user == null)
            {
                return NotFound("Benutzer nicht gefunden.");
            }

            await _mediator.Send(new DeleteUserCommand.Command(id));
            return NoContent();
        }
    }
}
