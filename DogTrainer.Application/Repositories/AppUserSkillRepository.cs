using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class AppUserSkillRepository : BaseRepository<AppUserSkill, AppUserSkillDto>, IAppUserSkillRepository
    {

        public AppUserSkillRepository(DataContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Task<IEnumerable<AppUserSkillDto>> GetAllSkillsForUserAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<AppUserSkillDto>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression)
        {
            throw new NotImplementedException();
        }

        Task<AppUserSkillDto> IAppUserSkillRepository.AddAsync(AppUserSkill entity)
        {
            throw new NotImplementedException();
        }
    }
}
