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
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext db;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(Order entity)
        {
            await db.Orders.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Order> Get(int id)
        {
            return await db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Order> GetAll()
        {
            return db.Orders.Include(p => p.OrderItems).ThenInclude(x => x.Product);
        }

        public async Task<bool> Remove(Order entity)
        {
            db.Orders.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<Order> Update(Order entity)
        {
            db.Orders.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
