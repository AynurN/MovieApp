using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.DTOs.UserDTOs;

public record UserRegisterDTO(string FullName, string UserName, string Email, string Password, string ConfirmPassword, string? PhoneNumber);

public class UserRegisterDTOValidator :AbstractValidator<UserRegisterDTO>
{
    public UserRegisterDTOValidator()
    {
        RuleFor(u=>u.FullName).NotNull().NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(u=>u.UserName).NotNull().NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
        //TODO: AddPasswordValidators
        RuleFor(u => u).Custom((u, context) =>
        {
            if(u.Password != u.ConfirmPassword)
            {
                context.AddFailure("ConfirmPassword", "Password and ConfirmPassword do not match!");
            }
        }

        );
    }

}

