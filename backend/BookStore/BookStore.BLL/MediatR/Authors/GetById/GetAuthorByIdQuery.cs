using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.Author;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.GetById
{
    public record GetAuthorByIdQuery(int id) : IValidatableRequest<Result<AuthorDto>>
    {
    }
}
