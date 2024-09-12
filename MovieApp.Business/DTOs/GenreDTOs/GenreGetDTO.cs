using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.GenreDTOs
{
    public record GenreGetDTO(int Id, string Name, DateTime CreatedAt, DateTime ModifiedAt, bool IsDeleted);
   
}
