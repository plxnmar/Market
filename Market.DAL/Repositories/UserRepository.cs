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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(User entity)
        {
            await db.Users.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<User> Get(int id)
        {
            return await db.Users.
                Include(x => x.Role).
                Include(r => r.Cart).
                ThenInclude(x => x.CartItems).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return db.Users.
                Include(x => x.Role).
                Include(r => r.Cart).
                ThenInclude(x=> x.CartItems).
                ThenInclude(x => x.Product);
        }

        public async Task<bool> Remove(User entity)
        {
            db.Users.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<User> Update(User entity)
        {
            db.Users.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
