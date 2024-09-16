using MovieApp.Business.DTOs.CommentDTOs;
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
    public interface ICommentService
    {
        Task CreateAsync(CommentCreateDTO dTO);
        Task UpdateAsync(int id, CommentUpdateDTO dTO);
        Task DeleteAsync(int id);
        Task<ICollection<CommentGetDTO>> GetByExpessionAsync(bool AsNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
        Task<CommentGetDTO> GetByIdAsync(int id);

        Task<CommentGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
    }
}
