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
    public class RoleRepository : IBaseRepository<Role>
    {
        private readonly ApplicationDbContext db;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Create(Role entity)
        {
            await db.Roles.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Role> Get(int id)
        {
            return await db.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Role> GetAll()
        {
            return db.Roles;
        }

        public async Task<bool> Remove(Role entity)
        {
            db.Roles.Remove(entity);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<Role> Update(Role entity)
        {
            db.Roles.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
