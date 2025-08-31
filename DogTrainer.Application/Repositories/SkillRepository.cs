using AutoMapper;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class SkillRepository : ISkillRepository
    {

        private readonly BaseRepository<Skill> _baseRepository;
        public SkillRepository(DataContext dbContext, IMapper mapper)
        {
            _baseRepository = new BaseRepository<Skill>(dbContext, mapper);
        }

        public async Task<Skill> AddAsync(Skill entity)
        {
            return await _baseRepository.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Expression<Func<Skill, bool>> expression)
        {
            await _baseRepository.DeleteByIdAsync(expression);
        }

        public async Task<ICollection<Skill>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Skill?> GetByIdAsync(Expression<Func<Skill, bool>> expression)
        {
            return await _baseRepository.GetByIdAsync(expression);
        }

        public async Task<Skill> UpdateAsync(Expression<Func<Skill, bool>> expression)
        {
            return await _baseRepository.UpdateAsync(expression);
        }
    }
}
