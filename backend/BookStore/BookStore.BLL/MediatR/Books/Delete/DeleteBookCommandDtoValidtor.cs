using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Delete
{
    internal class DeleteBookCommandDtoValidtor : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandDtoValidtor()
        {
            RuleFor(x => x.id).NotEmpty().Must(x => x > 0);
        }
    }
}
