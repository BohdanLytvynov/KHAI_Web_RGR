using BookStore.BLL.Dto.Book;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetAllBooksWithAuthors
{
    public record GetAllBooksWithAuthorsQuery : IRequest<Result<IEnumerable<BookDto>>>
    {
    }
}
