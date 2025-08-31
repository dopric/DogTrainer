using DogTrainer.Domain;
using System.Linq.Expressions;

namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserSkillRepository
    {
        Task<IEnumerable<AppUserSkill>> GetAllSkillsForUserAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task<ICollection<AppUserSkill>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task<AppUserSkill> AddAsync(AppUserSkill entity);
        Task DeleteByIdAsync(Expression<Func<AppUserSkill, bool>> expression);
    }
}
