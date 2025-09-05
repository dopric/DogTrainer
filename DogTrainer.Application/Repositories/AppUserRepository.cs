using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;

namespace DogTrainer.Application.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser, AppUserDto>, IAppUserRepository
    {
        public AppUserRepository(DataContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public Task<AppUserDto> Login(UserLoginDto userLogin)
        {
            throw new NotImplementedException();
        }

        public Task<AppUserDto> Register(UserRegisterDto userRegister)
        {
            throw new NotImplementedException();
        }
    }
}
