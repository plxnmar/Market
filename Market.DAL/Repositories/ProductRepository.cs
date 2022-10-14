using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public bool Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            return db.Products.ToListAsync();
        }

        public bool Remove(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
