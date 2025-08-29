using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException(int id) : base($"Entity '{typeof(T).Name}' with id '{id}' was not found.")
        {
        }
    }
}
