using DogTrainer.Application.Dtos;
using DogTrainer.Domain;
using System.Linq.Expressions;

namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserSkillRepository
    {
        Task<IEnumerable<AppUserSkillDto>> GetAllSkillsForUserAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task<ICollection<AppUserSkillDto>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task<AppUserSkillDto> AddAsync(AppUserSkill entity);
        Task DeleteByIdAsync(Expression<Func<AppUserSkill, bool>> expression);
    }
}
