using BookStore.BLL.Dto.Author;
using BookStore.BLL.MediatR.Authors.Create;
using BookStore.BLL.MediatR.Authors.Delete;
using BookStore.BLL.MediatR.Authors.GetAll;
using BookStore.BLL.MediatR.Authors.GetById;
using BookStore.BLL.MediatR.Authors.Update;
using BookStore.WebApi.Attributes.Authorization;
using BookStore.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers.Athors
{
    [AuthorizeUsingRole("User", "Admin")]
    public class AuthorsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _mediator.Send(new GetAllAuthorsQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetAuthorByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDto dto)
        {
            return HandleResult(await _mediator.Send(new CreateAuthorCommand(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] AuthorDto dto)
        {
            return HandleResult(await _mediator.Send(new UpdateAuthorCommand(dto)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await _mediator.Send(new DeleteAuthorCommand(id)));
        }
    }
}
