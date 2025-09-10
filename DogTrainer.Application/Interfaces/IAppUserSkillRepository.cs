using DogTrainer.Application.Dtos;
using DogTrainer.Domain;
using System.Linq.Expressions;

namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserSkillRepository
    {
        Task<IEnumerable<AppUserSkillDto>> GetAllSkillsForUserAsync(string userId);
        Task<ICollection<AppUserSkillDto>> GetAllUsersForSkillAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task<AppUserSkillDto> AddAsync(AppUserSkillDto dto);
        Task DeleteByIdAsync(Expression<Func<AppUserSkill, bool>> expression);
        Task DeleteAppUserSkillAsync(string userId, int skillId);
    }
}
