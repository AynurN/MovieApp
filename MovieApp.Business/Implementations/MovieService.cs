using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;
using MovieApp.Business.Utilities;
using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using MovieApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository repo;
        private readonly IMapper mapper;
        private readonly IGenreService genreService;
        private readonly IWebHostEnvironment env;

        public MovieService( IMovieRepository repo, IMapper mapper, IGenreService genreService, IWebHostEnvironment env )
        {
            this.repo = repo;
            this.mapper = mapper;
            this.genreService = genreService;
            this.env = env;
        }
        public async Task CreateAsync(MovieCreateDTO dTO)
        {
            if (!await genreService.IsExistAsync(g => g.Id == dTO.GenreId && g.IsDeleted == false)) throw new EntityNotFoundException("Genre not found");
            Movie movie=mapper.Map<Movie>(dTO);
            string imageURl = dTO.Image.SaveFile(env.WebRootPath, "Images");
            MovieImage movieImage = new MovieImage()
            {
                ImageUrl = imageURl,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                IsDeleted = false
            };

            movie.MovieImages.Add(movieImage);
            movie.CreatedAt = DateTime.Now;
            movie.ModifiedAt = DateTime.Now;
            await repo.CreateAsync(movie);
            await repo.CommitAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Movie movie=await repo.GetByIdAsync(id);
            if (movie == null) throw new EntityNotFoundException("Movie not found!");
            repo.Delete(movie);
           await repo.CommitAsync();
        }

        public async Task<ICollection<MovieGetDTO>> GetByExpessionAsync(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
            
            var query = repo.GetByExpression(asNoTracking, expression, includes);
            var datas = await query.ToListAsync();

            return mapper.Map<ICollection<MovieGetDTO>>(datas);
        }

        public async Task<MovieGetDTO> GetByIdAsync(int id)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Movie movie = await repo.GetByIdAsync(id);
            if (movie == null) throw new EntityNotFoundException("Movie not found!");
            MovieGetDTO dto= mapper.Map<MovieGetDTO>(movie);
            return dto;

        }

        public  async Task<MovieGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
           Movie? movie=  repo.GetByExpression(AsNoTracking, expression, includes).FirstOrDefault();
            if (movie == null) throw new EntityNotFoundException("Movie not found");
            return mapper.Map<MovieGetDTO>(movie);
        }

        public async Task UpdateAsync(int id, MovieUpdateDTO dTO)
        {
            if (!await genreService.IsExistAsync(g => g.Id == dTO.GenreId && g.IsDeleted == false)) throw new EntityNotFoundException("Genre not found");

            if (id < 1) throw new InvalidIdException("Id is not valid");
            Movie movie = await repo.GetByIdAsync(id);
            if (movie == null) throw new EntityNotFoundException("Movie not found!");
            mapper.Map(dTO, movie);
            if (dTO.Image != null) {
                string imageURl = dTO.Image.SaveFile(env.WebRootPath, "Images");
                MovieImage movieImage = new MovieImage()
                {
                    ImageUrl = imageURl,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    IsDeleted = false
                };

                movie.MovieImages.Add(movieImage);
            }
            
            movie.ModifiedAt = DateTime.Now;
            await repo.CommitAsync();
        }
    }
}
