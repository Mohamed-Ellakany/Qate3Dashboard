using Microsoft.EntityFrameworkCore;
using Qate3BLL.Interfaces;
using Qate3DAL.Data;
using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _dbContext;
      

        public GenericRepository(AppDbContext dbContext )
        {
            _dbContext = dbContext;
           
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
           
             
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            if(typeof(T) == typeof(Category))
            {
              return (IEnumerable<T>)  await _dbContext.Categories.Include(c => c.Department).ToListAsync();
            }
            else
            {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            }

          

        }

        public async Task<T> GetByIdAsync(int? id)
        {
          return await _dbContext.Set<T>().FindAsync(id);
        }

        public  void Update(T entity)
        => _dbContext.Set<T>().Update(entity);


        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
