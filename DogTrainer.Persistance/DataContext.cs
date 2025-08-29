using DogTrainer.Domain;
using Microsoft.EntityFrameworkCore;

namespace DogTrainer.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }




        public DbSet<AppUser> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<AppUserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUserSkill>(x => x.HasKey(aus => new { aus.AppUserId, aus.SkillId }));

            builder.Entity<AppUserSkill>()
                .HasOne(aus => aus.AppUser)

                .WithMany(c => c.Skills)
                .HasForeignKey(aus => aus.AppUserId);

            builder.Entity<AppUserSkill>()
                .HasOne(aus => aus.Skill)
                .WithMany(s => s.AppUsers)
                .HasForeignKey(aus => aus.SkillId);
        }
    }
}
