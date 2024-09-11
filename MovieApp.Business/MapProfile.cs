using AutoMapper;
using MovieApp.Business.DTOs.MovieDTOs;
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
        }
    }
}
