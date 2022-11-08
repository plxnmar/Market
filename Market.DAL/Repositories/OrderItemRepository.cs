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
    public class OrderItemRepository : IBaseRepository<OrderItem>
    {
        private readonly ApplicationDbContext db;

        public OrderItemRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(OrderItem entity)
        {
            await db.OrderItems.AddAsync(entity);
            await db.SaveChangesAsync();
  
            return true;
        }

        public async Task<OrderItem> Get(int id)
        {
            return await db.OrderItems
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<OrderItem> GetAll()
        {
            return db.OrderItems.Include(x => x.Product);
        }

        public async Task<bool> Remove(OrderItem entity)
        {
            db.OrderItems.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<OrderItem> Update(OrderItem entity)
        {
            db.OrderItems.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
