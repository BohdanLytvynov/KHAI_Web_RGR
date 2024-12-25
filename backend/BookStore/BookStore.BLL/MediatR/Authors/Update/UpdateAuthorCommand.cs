using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.Author;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.Update
{
    public record UpdateAuthorCommand(AuthorDto dto) : IValidatableRequest<Result<bool>>;
}
