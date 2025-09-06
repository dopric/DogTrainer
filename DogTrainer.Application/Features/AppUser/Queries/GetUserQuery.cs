using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using MediatR;

namespace DogTrainer.Application.Features.AppUser.Queries
{
    public class GetUserQuery 
    {
        public class Query : IRequest<AppUserDto>
        {
            public UserRegisterDto RegisterDto { get; set; }
        }

        public class Handler(IAppUserRepository repository) : IRequestHandler<Query, AppUserDto>
        {
            public async Task<AppUserDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.RegisterDto.Email) && string.IsNullOrEmpty(request.RegisterDto.UserName))
                {
                    return null;
                }
                return await repository.GetByIdAsync<AppUserDto>(
                    u => !string.IsNullOrEmpty(u.Email) &&
                    !string.IsNullOrEmpty(request.RegisterDto.Email) && u.Email.Equals(request.RegisterDto.Email)
                    || !string.IsNullOrEmpty(u.UserName)
                    && !string.IsNullOrEmpty(request.RegisterDto.UserName) && u.UserName.Equals(request.RegisterDto.UserName));
            }
        }
        
    }
}