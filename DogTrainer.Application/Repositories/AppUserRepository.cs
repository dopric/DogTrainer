using AutoMapper;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly BaseRepository<AppUser> _baseRepository;

        public AppUserRepository(DataContext dbContext, IMapper mapper)
        {
            _baseRepository = new BaseRepository<AppUser>(dbContext, mapper);
        }

        public async Task<AppUser> AddAsync(AppUser entity)
        {
            await _baseRepository.AddAsync(entity);
            return entity;
        }

        public async Task DeleteByIdAsync(Expression<Func<AppUser, bool>> expression)
        {
            await _baseRepository.DeleteByIdAsync(expression);
        }

        public async Task<ICollection<AppUser>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<AppUser?> GetByIdAsync(Expression<Func<AppUser, bool>> expression)
        {
            var entity = await _baseRepository.GetByIdAsync(expression);
            return entity;
        }

        public async Task<AppUser> UpdateAsync(Expression<Func<AppUser, bool>> expression)
        {
            var user = await _baseRepository.UpdateAsync(expression);
            return user;
        }
    }
}
