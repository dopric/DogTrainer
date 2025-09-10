using AutoMapper;
using AutoMapper.QueryableExtensions;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class AppUserSkillRepository : BaseRepository<AppUserSkill, AppUserSkillDto>, IAppUserSkillRepository
    {

        public AppUserSkillRepository(DataContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task DeleteAppUserSkillAsync(string userId, int skillId)
        {
            var entity = await _dbContext.UserSkills.FirstOrDefaultAsync(x => x.AppUserId == userId && x.SkillId == skillId);
            if (entity != null)
            {
                 _dbContext.UserSkills.Remove(entity);
                 await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException<AppUserSkill>("User Skill not found");
            }
        }

        public async Task<IEnumerable<AppUserSkillDto>> GetAllSkillsForUserAsync(string userId)
        {
            var userSkills = await _dbContext.UserSkills
                .AsNoTracking()
                .Where(u=> u.AppUserId == userId)
                .ProjectTo<AppUserSkillDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return userSkills;
        }

        public async Task<ICollection<AppUserSkillDto>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            var userSkills = await _dbContext.UserSkills
                .AsNoTracking()
                .Where(expression)
                .ProjectTo<AppUserSkillDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return userSkills;
        }

        public async Task<AppUserSkillDto> AddAsync(AppUserSkillDto dto)
        {
            return await base.AddAsync<AppUserSkillDto>(dto);
        }
    }
}
