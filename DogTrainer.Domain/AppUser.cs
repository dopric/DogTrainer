using Microsoft.AspNetCore.Identity;

namespace DogTrainer.Domain
{
    public class AppUser : IdentityUser
    {
        //public int Id { get; set; }
        //public string Email { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;

        public ICollection<AppUserSkill> Skills { get; set; } = new List<AppUserSkill>();

    }
}
