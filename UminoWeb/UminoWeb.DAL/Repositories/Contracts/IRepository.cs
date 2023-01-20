using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.DAL.Base;

namespace UminoWeb.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllIsNotDeletedAsync();
        Task<T> GetAsync(int? id);
        Task UpdateAsync(T entity);
        Task CompletelyDeleteAsync(int? id);
        Task DeleteAsync(T entity);
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        Task AddAsync(params T[] entities);
    }
}
