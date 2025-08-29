namespace DogTrainer.Domain
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<AppUserSkill> Skills { get; set; } = new List<AppUserSkill>();

    }
}
