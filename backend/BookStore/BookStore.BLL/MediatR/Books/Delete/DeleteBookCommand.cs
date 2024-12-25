using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.Book;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Delete
{
    public record class DeleteBookCommand(int id) : IValidatableRequest<Result<DeleteBookDto>>
    {
    }
}
