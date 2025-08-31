using AutoMapper;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class AppUserSkillRepository : IAppUserSkillRepository
    {
        private readonly BaseRepository<AppUserSkill> _baseRepository;

        public AppUserSkillRepository(DataContext dbContext, IMapper mapper)
        {
            _baseRepository = new BaseRepository<AppUserSkill>(dbContext, mapper);
        }
        public async Task<AppUserSkill> AddAsync(AppUserSkill entity)
        {
            return await _baseRepository.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            await _baseRepository.DeleteByIdAsync(expression);
        }

        public async Task<IEnumerable<AppUserSkill>> GetAllSkillsForUserAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            var skills = await _baseRepository.GetAllAsync(expression);
            return skills;
        }

        public async Task<ICollection<AppUserSkill>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            return await _baseRepository.GetAllAsync(expression);
        }
    }
}
