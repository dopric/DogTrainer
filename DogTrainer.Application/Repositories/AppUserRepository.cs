using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DogTrainer.Application.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext dbContext;

        public AppUserRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AppUser> AddAsync(AppUser entity)
        {
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            // consider returning a DTO instead of the entity directly
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if(user is null)
            {
                throw new EntityNotFoundException<AppUser>(id);
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> UpdateAsync(AppUser entity)
        {
            var user = await dbContext.Users.Where(u => u.Id == entity.Id).FirstOrDefaultAsync();
            if(user is null)
            {
                throw new EntityNotFoundException<AppUser>(entity.Id);
            }
            // update fields, better way?
            user.Email = entity.Email;
            user.Password = entity.Password;
            await dbContext.SaveChangesAsync();
            // return DTO instead of entity directly
            return user;
        }
    }
}
