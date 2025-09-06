using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Skills.Queries
{
    public class GetAllSkillsQuery
    {
        public class Query : IRequest<ICollection<SkillDto>> { }

        public class Handler : IRequestHandler<Query, ICollection<SkillDto>>
        {
            private readonly ISkillRepository _skillRepository;

            public Handler(ISkillRepository skillRepository)
            {
                _skillRepository = skillRepository;
            }
            public async Task<ICollection<SkillDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _skillRepository.GetAllAsync<SkillDto>();
            }
        }
    }
}
