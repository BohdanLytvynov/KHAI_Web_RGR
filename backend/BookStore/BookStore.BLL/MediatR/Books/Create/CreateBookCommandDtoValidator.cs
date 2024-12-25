using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Create
{
    internal class CreateBookCommandDtoValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandDtoValidator()
        {
            RuleFor(x => x.dto.Name).NotNull();
            RuleFor(x => x.dto.PubYear).NotNull().Must(x => int.TryParse(x.ToString(), out var v));
            RuleFor(x => x.dto.Geners).NotNull().Must(x => x.Count > 0);
        }
    }
}
