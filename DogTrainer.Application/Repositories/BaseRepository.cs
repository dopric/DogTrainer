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
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public BaseRepository(DataContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<TDto> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            // check?
            var dto = _mapper.Map<TEntity, TDto>(entity);
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

        public async Task<ICollection<TDto>> GetAllAsync()
        {
            var listOfEntities = await _dbContext.Set<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<ICollection<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            // problem je sa ProjectTo pokusava da mapira AppUser na AppUser
            // da li postoji nacin da spolja se kaze tip D?
            var listOfEntities = await _dbContext.Set<TEntity>().Where(expression).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            return listOfEntities;
        }

        public async Task<TDto?> GetByIdAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbContext.Set<TEntity>().Where(expression).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<TDto> UpdateAsync(Expression<Func<TEntity, bool>> expression)
        {
            // TODO: sta treba da ide u update Entity ili Dto
            var entry = await _dbContext.Set<TEntity>().Where(expression).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            _mapper.Map<TEntity>(entry);
            await _dbContext.SaveChangesAsync();
            return entry;
        }
    }
}
