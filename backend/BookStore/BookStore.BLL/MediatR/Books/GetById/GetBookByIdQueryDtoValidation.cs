using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetById
{
    internal class GetBookByIdQueryDtoValidation : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryDtoValidation()
        {
            RuleFor(x => x.id)
                .NotEmpty()
                .Must(x => x > 0);
        }
    }
}
