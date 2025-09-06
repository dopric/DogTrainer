using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Features.AppUser.Queries
{
    public class GetUserByIdQuery
    {
        public class Query : IRequest<AppUserDto>
        {
            public string Id { get; set; } = string.Empty;
        }
        public class Handler : IRequestHandler<Query, AppUserDto>
        {
            private readonly IAppUserRepository _appUserRepository;
            public Handler(IAppUserRepository appUserRepository)
            {
                _appUserRepository = appUserRepository;
            }
            public async Task<AppUserDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    return null;
                }

                var user = await _appUserRepository.GetByIdAsync<AppUserDto>(u => u.Id.Equals(request.Id));
                return user;
            }
        }
    }
}
