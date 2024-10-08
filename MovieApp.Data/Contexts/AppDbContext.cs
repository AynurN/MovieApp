﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models;
using MovieApp.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Contexts
{

    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieImage> MovieImages { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
