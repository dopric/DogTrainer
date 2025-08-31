namespace DogTrainer.Domain
{
    public class AppUserSkill
    {
        public string AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
        public int SkillId { get; set; }
        public required Skill Skill { get; set; }
    }
}
