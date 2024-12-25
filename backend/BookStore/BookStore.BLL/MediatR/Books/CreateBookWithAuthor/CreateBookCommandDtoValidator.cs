using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.CreateBookWithAuthor
{
    internal class CreateBookCommandDtoValidator : AbstractValidator<CreateBookWithAuthorCommand>
    {
        public CreateBookCommandDtoValidator() 
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty();
            RuleFor(x => x.Dto.PubYear)
                .NotEmpty()
                .Must(x => x > 0);
        }
    }
}
