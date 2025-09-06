using AutoMapper;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Features.AppUser.Command
{
    public class AddUserCommand
    {
        public class Command(UserRegisterDto user) : IRequest<AppUserDto>
        {
            public UserRegisterDto User { get; } = user;
        }

        public class Handler : IRequestHandler<Command, AppUserDto>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public Handler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }
            public async Task<AppUserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var userDto = _mapper.Map<AppUserDto>(request.User);
                var newUser = await _appUserRepository.AddAsync<AppUserDto>(userDto);
                return newUser;
            }
        }
    }
}
