using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.GetById
{
    public class GetAuthorByIdCommandDtoValidation : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdCommandDtoValidation()
        {
            RuleFor(x => x.id).NotEmpty().Must(x => x > 0);
        }
    }
}
