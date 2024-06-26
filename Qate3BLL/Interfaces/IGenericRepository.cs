using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        void Update(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int? id);

        void Detach(T entity);
    }
}
