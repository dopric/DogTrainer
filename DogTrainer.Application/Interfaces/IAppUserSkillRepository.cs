namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserSkillRepository
    {
        Task<IEnumerable<Domain.AppUserSkill>> GetAllSkillsForUserAsync(int userId);
        Task<ICollection<Domain.AppUserSkill>> GetAllUsersForSkillAsync(int skillId);
        Task<Domain.AppUserSkill> AddAsync(Domain.AppUserSkill entity);
        Task DeleteByIdAsync(int userId, int skillId);
    }
}
