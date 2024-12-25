using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.Update
{
    public class UpdateAuthorCommandDtoValidation : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandDtoValidation()
        {
            RuleFor(x => x.dto.Id)
                .NotEmpty().Must(x => x > 0);

            RuleFor(x => x.dto.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.dto.Surename)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.dto.BirthDate)
                .NotEmpty()
                .Must(x => DateOnly.TryParse(x.ToShortDateString(), out var _r));
        }
    }
}
