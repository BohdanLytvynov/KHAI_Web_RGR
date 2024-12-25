using BookStore.BLL.Dto.Book;
using BookStore.BLL.MediatR.Books.Create;
using BookStore.BLL.MediatR.Books.CreateBookWithAuthor;
using BookStore.BLL.MediatR.Books.Delete;
using BookStore.BLL.MediatR.Books.GetAll;
using BookStore.BLL.MediatR.Books.GetAllBooksWithAuthors;
using BookStore.BLL.MediatR.Books.GetById;
using BookStore.BLL.MediatR.Books.Update;
using BookStore.WebApi.Attributes.Authorization;
using BookStore.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers.Books
{
    [AuthorizeUsingRole("User", "Admin")]
    public class BooksController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return HandleResult(await _mediator.Send(new GetBookByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimpleBookDto dto)
        {
            return HandleResult(await _mediator.Send(new CreateBookCommand(dto)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await _mediator.Send(new DeleteBookCommand(id)));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] SimpleBookDto dto)
        {
            return HandleResult(await _mediator.Send(new UpdateBookCommand(dto)));
        }
    }
}
