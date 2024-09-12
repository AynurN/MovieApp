using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Interfaces
{
    public interface IGenreService
    {
        Task CreateAsync(GenreCreateDTO dTO);
        Task UpdateAsync(int id, GenreUpdateDTO dTO);
        Task DeleteAsync(int id);
        Task<ICollection<GenreGetDTO>> GetByExpessionAsync(bool AsNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes);
        Task<GenreGetDTO> GetByIdAsync(int id);

        Task<GenreGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<Genre, bool>> expression);
    }
}
