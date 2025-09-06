using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Interfaces
{
    public interface IDogTrainerRepository<T, D> where T : class where D : class
    {
        Task<D?> GetByIdAsync<D>(Expression<Func<T, bool>> expresssion);
        Task<ICollection<D>> GetAllAsync<D>();
        Task DeleteByIdAsync(Expression<Func<T, bool>> expresssion);
        Task<D> AddAsync<D>(D dto);
        Task<D> UpdateAsync<D>(T entity);
    }
}
