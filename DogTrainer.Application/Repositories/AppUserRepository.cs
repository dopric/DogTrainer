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

        public async Task<AppUserDto> Login(UserLoginDto userLogin)
        {
            var user = await GetByIdAsync<AppUserDto>(u => u.Email == userLogin.UserName && u.PasswordHash == userLogin.Password);
            if(user == null)
            {
                throw new Exception("Invalid username or password");
            }
            return user;
        }

        public async Task<AppUserDto> Register(UserRegisterDto userRegister)
        {
            var user = await GetByIdAsync<AppUserDto>(u => u.Email == userRegister.Email || u.UserName == userRegister.UserName);
            if(user != null)
            {
                throw new Exception("User with given email or username already exists");
            }

            return user;
        }
    }
}
