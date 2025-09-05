using System.ComponentModel.DataAnnotations;

namespace DogTrainer.Application.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
