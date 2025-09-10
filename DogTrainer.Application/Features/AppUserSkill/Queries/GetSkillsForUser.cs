using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;

namespace DogTrainer.Application.Features.AppUserSkill.Queries
{
    public class GetSkillsForUser
    {
        public class Query(string userId) : IRequest<ICollection<AppUserSkillDto>>
        {
            public string UserId { get; } = userId;
        }

        public class Handler : IRequestHandler<Query, ICollection<AppUserSkillDto>>
        {
            private IAppUserSkillRepository _appUserSkillRepository;
            public Handler(IAppUserSkillRepository appUserSkillRepository)
            {
                this._appUserSkillRepository = appUserSkillRepository;

            }
            public async Task<ICollection<AppUserSkillDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userSkills = await _appUserSkillRepository.GetAllSkillsForUserAsync(request.UserId);
                return userSkills.Select(s => new AppUserSkillDto
                {
                    AppUserId = s.AppUserId,
                    SkillId = s.SkillId,
                }).ToList();
            }
        }
    }
}