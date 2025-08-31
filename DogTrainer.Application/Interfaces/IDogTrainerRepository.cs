using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Interfaces
{
    public interface IDogTrainerRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> expresssion);
        Task<ICollection<T>> GetAllAsync();
        Task DeleteByIdAsync(Expression<Func<T, bool>> expresssion);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(Expression<Func<T, bool>> expresssion);
    }
}
