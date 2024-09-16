using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.CommentDTOs
{
    public record CommentGetDTO(int Id, string Content, string AppUserUserName, int MovieId);
    
}
