using AutoMapper;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.DTOs.MovieImageDTOs;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business
{
    public class MapProfile :Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieGetDTO>().ReverseMap();
            CreateMap< Movie, MovieCreateDTO>().ReverseMap();
            CreateMap< Movie, MovieUpdateDTO>().ReverseMap();
            CreateMap< Genre, GenreUpdateDTO>().ReverseMap();
            CreateMap< Genre, GenreCreateDTO>().ReverseMap();
            CreateMap< Genre, GenreGetDTO>().ReverseMap();
            CreateMap< MovieImage, MovieImageGetDTO>().ReverseMap();
            CreateMap< AppUser, UserLoginDTO>().ReverseMap();
            CreateMap< AppUser, UserRegisterDTO>().ReverseMap();
            
        }
    }
}
