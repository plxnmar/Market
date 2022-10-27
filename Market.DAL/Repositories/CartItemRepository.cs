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
    public class CartItemRepository : IBaseRepository<CartItem>
    {
        private readonly ApplicationDbContext db;

        public CartItemRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(CartItem entity)
        {
            await db.CartItems.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<CartItem> Get(int id)
        {
            return await db.CartItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<CartItem> GetAll()
        {
            return db.CartItems;
        }

        public async Task<bool> Remove(CartItem entity)
        {
            db.CartItems.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<CartItem> Update(CartItem entity)
        {
            db.CartItems.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
