using Microsoft.AspNetCore.Identity;

namespace DogTrainer.Domain
{
    public class AppUser : IdentityUser
    {
        public ICollection<AppUserSkill> Skills { get; set; } = new List<AppUserSkill>();

    }
}
