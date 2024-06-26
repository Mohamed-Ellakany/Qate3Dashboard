using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //public IProductRepository ProductRepository { get; set; }
        //public IGenericRepository<Category> CategoryRepository { get; set; }
        //public IGenericRepository<SubCategory> SubCategoryRepository { get; set; }
        //public IGenericRepository<Department> DepartmentRepository { get; set; }

        IGenericRepository<T> Repository<T>() where T: BaseEntity;

        int Complete();


    }
}
