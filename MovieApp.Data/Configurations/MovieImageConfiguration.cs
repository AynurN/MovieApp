using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Configurations
{
    public class MovieImageConfiguration : IEntityTypeConfiguration<MovieImage>
    {
        public void Configure(EntityTypeBuilder<MovieImage> builder)
        {
            builder.Property(p => p.ImageUrl).IsRequired();

        //relation
        builder.HasOne(p=>p.Movie)
                .WithMany(i=>i.MovieImages)
                .HasForeignKey(i=>i.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
