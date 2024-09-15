using MovieApp.Business.DTOs.MovieImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.MovieDTOs
{
    public record MovieGetDTO(int Id, string Title, string Desc, DateTime CreatedAt, DateTime ModifiedAt, bool IsDeleted, ICollection<MovieImageGetDTO> MovieImages );
   
}
