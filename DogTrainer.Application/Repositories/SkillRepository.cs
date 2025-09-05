using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class SkillRepository : BaseRepository<Skill, SkillDto>, ISkillRepository
    {
        public SkillRepository(DataContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
