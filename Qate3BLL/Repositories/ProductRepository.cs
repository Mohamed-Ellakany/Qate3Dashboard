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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext):base(dbContext) 
        {
            
        }

        public IQueryable<Product> SearchByName(string name)
        {
           return  _dbContext.Products.Where(P => P.Prod_Title.Contains(name));
        }

    
    }
}
