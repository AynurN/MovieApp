using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;
using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository repo;
        private readonly IMapper mapper;

        public GenreService( IGenreRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task CreateAsync(GenreCreateDTO dTO)
        {
            Genre genre= mapper.Map<Genre>(dTO);
            genre.CreatedAt = DateTime.Now;
            genre.ModifiedAt = DateTime.Now;
           await  repo.CreateAsync(genre);
           await  repo.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {

            if (id < 1) throw new InvalidIdException("Id is not valid");
            Genre genre = await repo.GetByIdAsync(id);
            if (genre == null) throw new EntityNotFoundException("Genre not found!");
            repo.Delete(genre);
            await repo.CommitAsync();
        }

        public async Task<ICollection<GenreGetDTO>> GetByExpessionAsync(bool AsNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {

            var query = repo.GetByExpression(AsNoTracking, expression, includes);
            var datas = await query.ToListAsync();

            return mapper.Map<ICollection<GenreGetDTO>>(datas);
        }

        public async Task<GenreGetDTO> GetByIdAsync(int id)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Genre genre = await repo.GetByIdAsync(id);
            if (genre == null) throw new EntityNotFoundException("Genre not found!");
            return mapper.Map<GenreGetDTO>(genre);
        }

        public async Task<GenreGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {
            Genre? genre = repo.GetByExpression(AsNoTracking, expression, includes).FirstOrDefault();
            if (genre == null) throw new EntityNotFoundException("Genre not found");
            return mapper.Map<GenreGetDTO>(genre);
        }

        public async Task<bool> IsExistAsync(Expression<Func<Genre, bool>> expression)
        {
            return await repo.Table.AnyAsync(expression);
        }

        public async Task UpdateAsync(int id, GenreUpdateDTO dTO)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Genre genre = await repo.GetByIdAsync(id);
            if (genre == null) throw new EntityNotFoundException("Genre not found!");
            mapper.Map(dTO, genre);
            genre.ModifiedAt = DateTime.Now;
            await repo.CommitAsync();
        }
    }
}
