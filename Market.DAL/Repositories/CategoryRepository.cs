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
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(Category entity)
        {
            await db.Categories.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Category> Get(int id)
        {
            return await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Category> GetAll()
        {
            return db.Categories;
        }

        public async Task<bool> Remove(Category entity)
        {
            db.Categories.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<Category> Update(Category entity)
        {
            db.Categories.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
