using AutoMapper;
using AutoMapper.QueryableExtensions;
using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Repositories
{
    public class BaseRepository<T> : IDogTrainerRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public BaseRepository(DataContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
            if(entity is null)
            {
                throw new EntityNotFoundException<T>($"Entity {typeof(T).Name} could not be found");
            }
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            var listOfEntities = await _dbContext.Set<T>().ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            var listOfEntities = await _dbContext.Set<T>().Where(expression).ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _dbContext.Set<T>().ProjectTo<T>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<T> UpdateAsync(Expression<Func<T, bool>> expression)
        {
            var entry = await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
            _mapper.Map<T>(entry);
            await _dbContext.SaveChangesAsync();
            return entry;
        }
    }
}
