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
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(Product entity)
        {
            await db.Products.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await db.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Product> GetAll()
        {
            return db.Products.Include(p => p.Category);
        }

        public async Task<bool> Remove(Product entity)
        {
            db.Products.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<Product> Update(Product entity)
        {
            db.Products.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
