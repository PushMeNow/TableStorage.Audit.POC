using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.DAL;
using TableStorage.Audit.DAL.Entities;

namespace TableStorage.Audit.BLL
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

            return MapToResponse(entity);
        }

        public async Task<TResponse> Create(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);

            await Set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return MapToResponse(entity);
        }

        public async Task<TResponse> Update(Guid id, TRequest request)
        {
            var entity = await Set.FindAsync(id);

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return MapToResponse(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Set.FindAsync(id);

            Set.Remove(entity);

            await _context.SaveChangesAsync();
        }

        private TResponse MapToResponse<T>(T obj)
        {
            return _mapper.Map<TResponse>(obj);
        }
    }
}