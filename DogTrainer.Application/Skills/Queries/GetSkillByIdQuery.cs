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
    public class GetSkillByIdQuery
    {
        public class Query : IRequest<SkillDto?>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, SkillDto?>
        {
            private readonly ISkillRepository _skillRepository;

            public Handler(ISkillRepository skillRepository)
            {
                _skillRepository = skillRepository;
            }
            public async Task<SkillDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _skillRepository.GetByIdAsync<SkillDto>(u => u.Id == request.Id);
            }
        }
    }
}
