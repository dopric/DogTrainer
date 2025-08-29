using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DogTrainer.Application.Repositories
{
    public class AppUserSkillRepository : IAppUserSkillRepository
    {
        private readonly DataContext dbContext;

        public AppUserSkillRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<AppUserSkill> AddAsync(AppUserSkill entity)
        {
            await dbContext.UserSkills.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(int userId, int skillId)
        {
            var entity = await dbContext.UserSkills
                .Where(us => us.AppUserId == userId && us.SkillId == skillId)
                .FirstOrDefaultAsync();
            if(entity is null)
            {
                throw new EntityNotFoundException<AppUserSkill>(userId);
            }
            dbContext.UserSkills.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppUserSkill>> GetAllSkillsForUserAsync(int userId)
        {
            var skills = await dbContext.UserSkills
                .Include(us => us.Skill)
                .Where(us => us.AppUserId == userId)
                .ToListAsync();
            return skills;
        }

        public async Task<ICollection<AppUserSkill>> GetAllUsersForSkillAsync(int skillId)
        {
            var users = await dbContext.UserSkills
                .Include(us => us.AppUser)
                .Where(us => us.SkillId == skillId)
                .ToListAsync();
            return users;
        }
    }
}
