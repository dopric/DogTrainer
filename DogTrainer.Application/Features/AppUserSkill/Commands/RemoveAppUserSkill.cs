using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTrainer.Application.Dtos;
using MediatR;

namespace DogTrainer.Application.Features.AppUserSkill.Commands
{
    public class RemoveAppUserSkill
    {
        public class Command(AppUserSkillDto appUserSkillDto) : IRequest
        {
            public string UserId { get; set; } = appUserSkillDto.AppUserId;
            public int SkillId { get; set; } = appUserSkillDto.SkillId;
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DogTrainer.Application.Interfaces.IAppUserSkillRepository _appUserSkillRepository;

            public Handler(DogTrainer.Application.Interfaces.IAppUserSkillRepository appUserSkillRepository)
            {
                _appUserSkillRepository = appUserSkillRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                // Remove the AppUserSkill assignment
                await _appUserSkillRepository.DeleteAppUserSkillAsync(request.UserId, request.SkillId);
            }
        }
    }
}