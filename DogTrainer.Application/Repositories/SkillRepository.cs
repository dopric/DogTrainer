using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DogTrainer.Application.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DataContext dbContext;

        public SkillRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Skill> AddAsync(Skill entity)
        {
            await dbContext.Skills.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var skill = await dbContext.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
            if(skill is null)
            {
                throw new EntityNotFoundException<Skill>(id);
            }
            dbContext.Skills.Remove(skill);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await dbContext.Skills.ToListAsync();
        }

        public async Task<Skill?> GetByIdAsync(int id)
        {
            return await dbContext.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Skill> UpdateAsync(Skill entity)
        {
            var skill = await dbContext.Skills.Where(s => s.Id == entity.Id).FirstOrDefaultAsync();
            if(skill is null)
            {
                throw new EntityNotFoundException<Skill>(entity.Id);
            }
            skill.Name = entity.Name;
            await dbContext.SaveChangesAsync();
            return skill;
        }
    }
}
