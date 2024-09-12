using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.GenreDTOs;

public record GenreCreateDTO(string Name, bool IsDeleted);

public class GenreCreateDTOValidation : AbstractValidator<GenreCreateDTO>
{
    public GenreCreateDTOValidation()
    {
        RuleFor(g => g.Name).NotEmpty().NotNull().MaximumLength(100).MinimumLength(2);
        RuleFor(g => g.IsDeleted).NotNull();
    }

}

