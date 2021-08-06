using System;
using System.Threading.Tasks;

namespace TableStorage.Audit.BLL.Interfaces
{
    public interface IService<in TRequest, TResponse>
    {
        Task<TResponse[]> GetAll();

        Task<TResponse> GetById(Guid id);

        Task<TResponse> Create(TRequest request);

        Task<TResponse> Update(Guid id, TRequest request);

        Task Delete(Guid id);
    }
}