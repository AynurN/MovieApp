using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.TokenDTOS
{
    public record TokenResponseDTO(string AccessToken, DateTime ExpireDate);
    
}
