using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.DAL;
using TableStorage.Audit.DAL.Entities;
using TableStorage.Audit.Exceptions;

namespace TableStorage.Audit.BLL.Services
{
    public abstract class ServiceBase<TEntity, TRequest, TResponse> : IService<TRequest, TResponse>
        where TEntity : BaseFields
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        protected readonly DbSet<TEntity> Set;

        protected ServiceBase(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Set = _context.Set<TEntity>();
        }

        public Task<TResponse[]> GetAll()
        {
            return Set.AsNoTracking()
                      .ProjectTo<TResponse>(_mapper.ConfigurationProvider)
                      .ToArrayAsync();
        }

        public async Task<TResponse> GetById(Guid id)
        {
            var entity = await Set.FindAsync(id);

            Guard.ThrowObjectNotFoundIfEmpty(entity, Set.EntityType.Name);
            
            return MapToResponse(entity);
        }

        public async Task<TResponse> Create(TRequest request)
        {
            Guard.ThrowParameterInvalidIfEmpty(request, nameof(request));
            
            var entity = _mapper.Map<TEntity>(request);

            await Set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return MapToResponse(entity);
        }

        public async Task<TResponse> Update(Guid id, TRequest request)
        {
            Guard.ThrowParameterInvalidIfEmpty(id, nameof(id));
            Guard.ThrowParameterInvalidIfEmpty(request, nameof(request));
            
            var entity = await Set.FindAsync(id);

            Guard.ThrowObjectNotFoundIfEmpty(entity, Set.EntityType.Name);
            
            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return MapToResponse(entity);
        }

        public async Task Delete(Guid id)
        {
            Guard.ThrowParameterInvalidIfEmpty(id, nameof(id));
            
            var entity = await Set.FindAsync(id);

            Guard.ThrowObjectNotFoundIfEmpty(entity, Set.EntityType.Name);

            Set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private TResponse MapToResponse<T>(T obj)
        {
            return _mapper.Map<TResponse>(obj);
        }
    }
}