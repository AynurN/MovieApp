using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Interfaces
{
    public interface IMovieService
    {
      Task CreateAsync(MovieCreateDTO dTO);
        Task UpdateAsync(int id,MovieUpdateDTO dTO);
        Task DeleteAsync(int id);
        Task<ICollection<MovieGetDTO>> GetByExpessionAsync(bool AsNoTracking=false,Expression<Func<Movie, bool>>? expression=null, params string[] includes);
        Task<MovieGetDTO> GetByIdAsync(int id);

        Task<MovieGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes);
    }
}
