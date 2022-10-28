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
    public class CartRepository : IBaseRepository<Cart>
    {
        private readonly ApplicationDbContext db;

        public CartRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(Cart entity)
        {
            await db.Carts.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Cart> Get(int id)
        {
            return await db.Carts
                .Include(p => p.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Cart> GetAll()
        {
            return db.Carts
                .Include(p => p.CartItems)
                .ThenInclude(x => x.Product);
        }

        public async Task<bool> Remove(Cart entity)
        {
            db.Carts.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<Cart> Update(Cart entity)
        {
            db.Carts.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
