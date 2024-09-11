using MovieApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {

        public DbSet<TEntity> Table { get;}
        Task CreateAsync(TEntity entity);
        IQueryable<TEntity> GetByExpression(bool AsNoTracking=false,Expression<Func<TEntity, bool>>? expression=null, params string[] includes); 
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<int> CommitAsync();
    }
}
