using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Features.AppUser.Command
{
    public class DeleteUserCommand
    {
        public class Command(string id) : IRequest
        {
            public string Id { get; } = id;
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAppUserRepository _appUserRepository;

            public Handler(IAppUserRepository appUserRepository)
            {
                _appUserRepository = appUserRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _appUserRepository.DeleteByIdAsync(u => u.Id.Equals(request.Id));
            }
        }
    }
}
