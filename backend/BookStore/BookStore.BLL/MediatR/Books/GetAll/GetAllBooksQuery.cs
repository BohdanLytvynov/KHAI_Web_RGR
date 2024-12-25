using BookStore.BLL.Dto.Book;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetAll
{
    public class GetAllBooksQuery : IRequest<Result<IEnumerable<SimpleBookDto>>>
    {
    }
}
