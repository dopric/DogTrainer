using DogTrainer.Application.Dtos;
using DogTrainer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserRepository : IDogTrainerRepository<AppUser, AppUserDto>
    {
        Task<AppUserDto> Register(UserRegisterDto userRegister);
        Task<AppUserDto> Login(UserLoginDto userLogin);
    }
}
