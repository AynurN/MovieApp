using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.CommentDTOs;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Implementations;
using MovieApp.Business.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await commentService.GetByExpessionAsync(true, null, "Movie", "AppUser"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( int id)
        {

            CommentGetDTO dto = null;
            try
            {
                dto = await commentService.GetOneByExpressionAsync(false, m => m.Id == id, "Movie", "AppUser");
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CommentCreateDTO dto)
        {
            try
            {
                await commentService.CreateAsync(dto);
            }
            catch (Exception ex) { 
             return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] CommentUpdateDTO dto)
        {
            try
            {
                await commentService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await commentService.DeleteAsync(id);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
