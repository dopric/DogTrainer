using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using DogTrainer.Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserRepository _userRepository;

        public AccountsController(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            // Schritt 1: Eingabe validieren
            if(userLogin == null || string.IsNullOrWhiteSpace(userLogin.UserName) || string.IsNullOrWhiteSpace(userLogin.Password))
            {
                return BadRequest("Benutzername und Passwort sind erforderlich.");
            }

            // Schritt 2: Benutzer aus Datenbank oder Service abrufen (Platzhalter)
            // Beispiel: var user = _userService.GetUserByUserName(userLogin.UserName);
            // Hier als Dummy:
            var dummyUser = new { UserName = "testuser", Password = "testpass" };

            // Schritt 3: Passwort prüfen
            if(userLogin.UserName != dummyUser.UserName || userLogin.Password != dummyUser.Password)
            {
                return Unauthorized("Ungültige Anmeldedaten.");
            }

            // Schritt 4: Token generieren (optional, z.B. JWT)
            // Beispiel: var token = _tokenService.GenerateToken(dummyUser);


            // Schritt 5: Erfolgsmeldung zurückgeben
            return Ok("Login erfolgreich");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetByIdAsync(u => u.UserName == userRegister.UserName || u.Email == userRegister.Email);
            if(existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            userRegister.Password = hashedPassword;

            //var userDto = await _userRepository.Register(userRegister);

            return Ok("Registrierung erfolgreich");
        }
    }
}
