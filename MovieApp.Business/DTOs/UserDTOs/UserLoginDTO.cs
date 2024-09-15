using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.UserDTOs;

public record UserLoginDTO(string UserName, string Password, bool RememberMe);
public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
{
    public UserLoginDTOValidator()
    {
        RuleFor(u=>u.UserName).NotEmpty().NotNull().MinimumLength(3).MaximumLength(50);
        RuleFor(u => u.Password).NotNull().NotEmpty();
    }

}

