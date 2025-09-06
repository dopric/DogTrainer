using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using DogTrainer.Domain;
using MediatR;
using DogTrainer.Application.Features.AppUser.Queries;
using DogTrainer.Application.Features.AppUser.Command;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            // Schritt 1: Eingabe validieren
            if (userLogin == null || string.IsNullOrWhiteSpace(userLogin.UserName) || string.IsNullOrWhiteSpace(userLogin.Password))
            {
                return BadRequest("Benutzername und Passwort sind erforderlich.");
            }

            // Schritt 2: Benutzer aus Datenbank oder Service abrufen (Platzhalter)
            // Beispiel: var user = _userService.GetUserByUserName(userLogin.UserName);
            // Hier als Dummy:
            var user = await _mediator.Send(new GetUserQuery.Query
            {
                RegisterDto =
                { Email = string.Empty,
                Password=userLogin.Password}
            });

            if (user is null)
            {
                return Unauthorized("Invalid credentials");
            }

            // TODO

            // Schritt 4: Token generieren (optional, z.B. JWT)
            // Beispiel: var token = _tokenService.GenerateToken(dummyUser);


            // Schritt 5: Erfolgsmeldung zurückgeben
            return Ok("Login erfolgreich");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _mediator.Send(new GetUserQuery.Query { RegisterDto = userRegister });

            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            userRegister.Password = hashedPassword;
            // TODO
            var userDto = await _userRepository.Register(userRegister);


            return Ok("Registrierung erfolgreich");
        }

        [HttpDelete("{id:string}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery.Query { Id = id });
            if (user == null)
            {
                return BadRequest("User can not be deleted, user not found");
            }

            await _mediator.Send(new DeleteUserCommand.Command(id));
            return NoContent();
        }
    }
}
