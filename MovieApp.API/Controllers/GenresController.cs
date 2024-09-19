using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.ApiResponses;
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
            var data = await genreService.GetByExpessionAsync(true);
            return Ok(new ApiResponse<ICollection<GenreGetDTO>>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null,
                Data = data
            });
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
                return BadRequest(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest, //400
                    ErrorMessage = "Id is invalid!",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<GenreGetDTO>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }


       // [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GenreCreateDTO dto)
        {
            try
            {
                await genreService.CreateAsync(dto);
            }
            catch (GenreAlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<GenreCreateDTO>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreCreateDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<GenreCreateDTO>
            {
                Data = null,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

      //  [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] GenreUpdateDTO dto)
        {
            try
            {
                await genreService.UpdateAsync(id, dto);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreUpdateDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id yanlisdir",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreUpdateDTO>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (GenreAlreadyExistsException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null,
                    PropertyName = ex.PropName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreUpdateDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<GenreUpdateDTO>
            {
                Data = null,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });

        }
       // [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await genreService.DeleteAsync(id);
            }
            catch (InvalidIdException)
            {
                return BadRequest(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest, 
                    ErrorMessage = "Id is invalid!",
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDTO>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = null,
                ErrorMessage = null
            });
        }
    }
}
