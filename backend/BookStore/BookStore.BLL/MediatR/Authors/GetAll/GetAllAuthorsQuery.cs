using BookStore.BLL.Dto.Author;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.GetAll
{
    public record GetAllAuthorsQuery() : IRequest<Result<IEnumerable<AuthorDto>>>;    
}
