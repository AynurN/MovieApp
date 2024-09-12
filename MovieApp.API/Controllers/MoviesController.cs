using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await movieService.GetByExpessionAsync(true,null,"Genre"));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            MovieGetDTO dto = null;
            try
            {
                dto = await movieService.GetByIdAsync(id);
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

        public async Task<IActionResult> Create([FromForm] MovieCreateDTO dto)
        {
            try
            {
                await movieService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] MovieUpdateDTO dto)
        {
            try
            {
                await movieService.UpdateAsync(id, dto);
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
                await movieService.DeleteAsync(id);
            }
            catch (InvalidIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex) { 
              return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
