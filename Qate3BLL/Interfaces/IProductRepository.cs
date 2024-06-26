using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3BLL.Interfaces
{
    public interface IProductRepository :IGenericRepository<Product>
    {
       IQueryable<Product> SearchByName(string name);
    }
}
