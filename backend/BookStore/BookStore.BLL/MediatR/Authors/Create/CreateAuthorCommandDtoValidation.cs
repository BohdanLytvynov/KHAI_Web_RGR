using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.Create
{
    internal class CreateAuthorCommandDtoValidation : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandDtoValidation() 
        {
            RuleFor(x=>x.dto.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x=>x.dto.Surename)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.dto.BirthDate)
                .NotEmpty()
                .Must(x => DateOnly.TryParse(x.ToShortDateString(), out var _r));
        }
    }
}
