using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Implementations;
using MovieApp.Business.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await genreService.GetByExpessionAsync(true));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GenreGetDTO dto = null;
            try
            {
                dto = await genreService.GetByIdAsync(id);
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


       // [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GenreCreateDTO dto)
        {
            try
            {
                await genreService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

      //  [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] GenreUpdateDTO dto)
        {
            try
            {
                await genreService.UpdateAsync(id, dto);
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
       // [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await genreService.DeleteAsync(id);
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
