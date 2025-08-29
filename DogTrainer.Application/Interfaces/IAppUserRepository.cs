using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Interfaces
{
    public interface IAppUserRepository : IDogTrainerRepository<Domain.AppUser>
    {
    }
}
