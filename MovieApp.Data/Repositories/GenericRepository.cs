using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using MovieApp.Data.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public DbSet<TEntity> Table => context.Set<TEntity>();

        public async Task<int> CommitAsync() => await context.SaveChangesAsync();

        public async Task CreateAsync(TEntity entity)=> await Table.AddAsync(entity);
       

        public async void Delete(TEntity entity)=>  Table.Remove(entity);
       

        public  IQueryable<TEntity> GetByExpression(bool AsNoTracking=false, Expression<Func<TEntity, bool>>? expression=null, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes.Length > 0)
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                    
                }
            }
            if (AsNoTracking) query = query.AsNoTracking();
            return expression is not null ? query.Where(expression) : query;
        }

        public async Task<TEntity> GetByIdAsync(int id)=>await Table.FirstOrDefaultAsync(t=>t.Id == id);
       
    }
}
