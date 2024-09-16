using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieApp.Business.DTOs.CommentDTOs;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;
using MovieApp.Core.IRepositories;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repo;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task CreateAsync(CommentCreateDTO dTO)
        {
            Comment comment = mapper.Map<Comment>(dTO);
            comment.CreatedAt = DateTime.Now;
            comment.ModifiedAt = DateTime.Now;
            await repo.CreateAsync(comment);
            await repo.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Comment comment = await repo.GetByIdAsync(id);
            if (comment == null) throw new EntityNotFoundException("Comment not found!");
            repo.Delete(comment);
            await repo.CommitAsync();
        }

        public async Task<ICollection<CommentGetDTO>> GetByExpessionAsync(bool AsNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
        {
            var query = repo.GetByExpression(AsNoTracking, expression, includes);
            var datas = await query.ToListAsync();

            return mapper.Map<ICollection<CommentGetDTO>>(datas);
        }

        public async Task<CommentGetDTO> GetByIdAsync(int id)
        {

            if (id < 1) throw new InvalidIdException("Id is not valid");
            Comment comment = await repo.GetByIdAsync(id);
            if (comment == null) throw new EntityNotFoundException("Comment not found!");
            return mapper.Map<CommentGetDTO>(comment);
        }

        public async Task<CommentGetDTO> GetOneByExpressionAsync(bool AsNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
        {
            Comment? comment = repo.GetByExpression(AsNoTracking, expression, includes).FirstOrDefault();
            if (comment == null) throw new EntityNotFoundException("Comment not found");
            return mapper.Map<CommentGetDTO>(comment);
        }

        public async Task UpdateAsync(int id, CommentUpdateDTO dTO)
        {
            if (id < 1) throw new InvalidIdException("Id is not valid");
            Comment comment = await repo.GetByIdAsync(id);
            if (comment == null) throw new EntityNotFoundException("Comment not found!");
            mapper.Map(dTO, comment);
            comment.ModifiedAt = DateTime.Now;
            await repo.CommitAsync();
        }
    }
}
