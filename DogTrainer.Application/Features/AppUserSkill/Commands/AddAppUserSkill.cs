using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;

namespace DogTrainer.Application.Features.AppUserSkill.Commands
{
    public class AddAppUserSkill
    {

        public class Command(AppUserSkillDto appUserSkillDto) : IRequest<AppUserSkillDto>
        {
            public AppUserSkillDto AppUserSkillDto { get; set; } = appUserSkillDto;
            public class Handler : IRequestHandler<Command, AppUserSkillDto>
            {
                private readonly IAppUserSkillRepository _appUserSkillRepository;
                public Handler(IAppUserSkillRepository appUserSkillRepository)
                {
                    this._appUserSkillRepository = appUserSkillRepository;
                }

                public async Task<AppUserSkillDto> Handle(Command request, CancellationToken cancellationToken)
                {
                    // Add the entity to the repository
                    return await _appUserSkillRepository.AddAsync(request.AppUserSkillDto);

                }
            }
        }
    }
}