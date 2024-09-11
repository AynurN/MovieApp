using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Core.IRepositories;
using MovieApp.Data.Contexts;
using MovieApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services, string connectionStr)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(connectionStr)
            );

        }
    }
}
