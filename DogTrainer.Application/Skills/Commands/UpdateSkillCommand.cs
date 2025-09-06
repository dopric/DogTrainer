using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Skills.Commands
{
    public class UpdateSkillCommand
    {
        public class Command : IRequest<SkillDto>
        {
            public UpdateSkillDto Skill { get; set; }
        }
        public class Handler : IRequestHandler<Command, SkillDto>
        {
            private readonly ISkillRepository _skillRepository;
            private readonly IMapper _mapper;
            public Handler(ISkillRepository skillRepository, IMapper mapper)
            {
                _skillRepository = skillRepository;
                _mapper = mapper;
            }
            
            public async Task<SkillDto> Handle(Command request, CancellationToken cancellationToken)
            {
                // first convert the UpdateSkillDto to SkillDto
                var skillEntity = _mapper.Map<Skill>(request.Skill);
                // then update the skill
                return await _skillRepository.UpdateAsync<SkillDto>(skillEntity);
            }

        }
    }
}
