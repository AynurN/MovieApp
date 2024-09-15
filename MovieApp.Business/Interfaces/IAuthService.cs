using MovieApp.Business.DTOs.TokenDTOS;
using MovieApp.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDTO dto);
        Task<TokenResponseDTO> Login(UserLoginDTO dto);
    }
}
