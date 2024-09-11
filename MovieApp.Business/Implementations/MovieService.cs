using AutoMapper;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;
using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using MovieApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public MovieService( IMovieRepository repo, IMapper mapper )
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task CreateAsync(MovieCreateDTO dTO)
        {
            Movie movie=mapper.Map<Movie>(dTO);
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
            ICollection<MovieGetDTO> dtos = datas.Select(data => new MovieGetDTO(data.Id, data.Title, data.Desc, data.CreatedAt, data.ModifiedAt, data.IsDeleted)).ToList();

            return dtos;
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
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Movie movie = await repo.GetByIdAsync(id);
            if (movie == null) throw new EntityNotFoundException("Movie not found!");
            mapper.Map(dTO, movie);
            movie.ModifiedAt = DateTime.Now;
            await repo.CommitAsync();
        }
    }
}
