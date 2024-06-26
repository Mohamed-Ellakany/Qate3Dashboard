using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Qate3BLL.Interfaces;
using Qate3BLL.Repositories;
using Qate3DAL.Data;
using Qate3DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private protected readonly AppDbContext _dbContext;
        private Hashtable _repositories;
        //public IProductRepository ProductRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public IGenericRepository<Category> CategoryRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public IGenericRepository<SubCategory> SubCategoryRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public IGenericRepository<Department> DepartmentRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }


        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            
            var key = typeof(T).Name;
            
            if (!_repositories.ContainsKey(key))
            {
                if(key == nameof(Product))
                {
                     var repository = new ProductRepository(_dbContext);
                    _repositories.Add(key, repository);
                }
                else
                {
                var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);

                }

            }

            return _repositories[key] as IGenericRepository<T>;

        }
    }
}
