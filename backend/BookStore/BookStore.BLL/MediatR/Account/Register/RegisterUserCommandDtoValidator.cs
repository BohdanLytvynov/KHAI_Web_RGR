using BookStore.BLL.MediatR.Account.RegisterCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Register
{
    public class RegisterUserCommandDtoValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandDtoValidator() 
        {
            RuleFor(x => x.dto.surename).NotEmpty().MaximumLength(50);
            RuleFor(x => x.dto.name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.dto.birthday).Must(x => DateOnly.TryParse(x, out var _date));
            RuleFor(x => x.dto.address).NotEmpty();
            RuleFor(x => x.dto.password).NotEmpty().Must(x => x.Length > 8);
            RuleFor(x => x.dto.nickname).NotEmpty();
        }
    }
}
