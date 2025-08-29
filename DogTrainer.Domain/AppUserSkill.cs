namespace DogTrainer.Domain
{
    public class AppUserSkill
    {
        //public int AppUserSkillId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
