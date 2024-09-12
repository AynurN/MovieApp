using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using MovieApp.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Repositories
{
    public class MovieImageRepository : GenericRepository<MovieImage>, IMovieImageRepository
    {
        public MovieImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
