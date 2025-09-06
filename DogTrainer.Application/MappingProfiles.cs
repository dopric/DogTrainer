using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Domain;

namespace DogTrainer.Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUserDto, AppUser>();
            CreateMap<AppUser, AppUserDto>();

            CreateMap<SkillDto, Skill>();
            CreateMap<Skill, SkillDto>();

            CreateMap<UserLoginDto, AppUser>();
            CreateMap<AppUser, UserLoginDto>();

            CreateMap<UserRegisterDto, AppUser>();
            CreateMap<AppUser, UserRegisterDto>();

            CreateMap<UpdateSkillDto, SkillDto>();
            CreateMap<SkillDto, UpdateSkillDto>();

            CreateMap<UpdateSkillDto, Skill>();
            CreateMap<Skill, UpdateSkillDto>();

            
        }
    }
}
