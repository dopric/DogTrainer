using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Skills.Commands
{
    public class DeleteSkillCommand
    {

        public class Command(int id) : IRequest
        {
            public int Id { get; } = id;
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly ISkillRepository _skillRepository;

            public Handler(ISkillRepository skillRepository)
            {
                _skillRepository = skillRepository;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _skillRepository.DeleteByIdAsync(s => s.Id == request.Id);
            }
        }
    }
}
