using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Skills.Commands
{
    public class AddSkillCommand
    {
        public AddSkillCommand()
        {
            Console.WriteLine("AddSkillCommand created");
        }
        public class Command : IRequest<SkillDto>
        {
            public Command()
            {
                Console.WriteLine("Command created");
            }
            public SkillDto Skill { get; set; } = new SkillDto();
        }

        public class Handler : IRequestHandler<Command, SkillDto>
        {
            private readonly ISkillRepository _skillRepository;

            public Handler(ISkillRepository skillRepository)
            {
                _skillRepository = skillRepository;
            }
            public async Task<SkillDto> Handle(Command request, CancellationToken cancellationToken)
            {
                // Implementation to add the skill goes here.
                // For now, we will return the skill from the request.
                return await _skillRepository.AddAsync<SkillDto>(request.Skill);
            }
        }
    }
}
