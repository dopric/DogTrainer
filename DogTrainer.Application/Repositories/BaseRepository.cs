using AutoMapper;
using AutoMapper.QueryableExtensions;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Exceptions;
using DogTrainer.Application.Interfaces;
using DogTrainer.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DogTrainer.Application.Repositories
{
    public class BaseRepository<TEntity, TDto> : IDogTrainerRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        protected readonly DataContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(DataContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<TDto> AddAsync<TDto>(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return dto;
        }

        public async Task DeleteByIdAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            if(entity is null)
            {
                throw new EntityNotFoundException<TEntity>($"Entity {typeof(TEntity).Name} could not be found");
            }
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TDto>> GetAllAsync<TDto>()
        {
            var listOfEntities = await _dbContext.Set<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<ICollection<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            var listOfEntities = await _dbContext.Set<TEntity>().Where(expression).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<TDto?> GetByIdAsync<TDto>(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbContext.Set<TEntity>().Where(expression).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<TDto> UpdateAsync<TDto>(TEntity entity)
        {

             _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            // konverzija ovde nije potrebna zato sto se radi o prostom objektu 
            return _mapper.Map<TDto>(entity);
        }
    }
}
