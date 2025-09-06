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
    public class GetAllUsersQuery
    {
        public class Query : IRequest<List<AppUserDto>> { }
        public class Handler : IRequestHandler<Query, ICollection<AppUserDto>>
        {
            private readonly IAppUserRepository _appUserRepository;

            public Handler(IAppUserRepository appUserRepository)
            {
                _appUserRepository = appUserRepository;
            }
            public async Task<ICollection<AppUserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _appUserRepository.GetAllAsync<AppUserDto>();
                return users;
            }
        }
    }
}
