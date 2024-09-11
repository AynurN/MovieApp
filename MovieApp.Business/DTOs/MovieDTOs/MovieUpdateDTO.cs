using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.MovieDTOs;

    public record MovieUpdateDTO(string Title, string Desc, bool IsDeleted);


public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDTO>
{
    public MovieUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Not empty")
            .NotNull().WithMessage("Not null")
            .MinimumLength(1).WithMessage("Min length must be 1")
            .MaximumLength(200).WithMessage("Length must be less than 200");

        RuleFor(x => x.Desc)
                .NotNull().When(x => !x.IsDeleted).WithMessage("If movie is active desc cannot be null")
                .MaximumLength(800).WithMessage("Length must be less than 800");

        RuleFor(x => x.IsDeleted).NotNull();


    }
}