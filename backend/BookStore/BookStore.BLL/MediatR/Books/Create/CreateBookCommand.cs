﻿using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.Book;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Create
{
    public record CreateBookCommand(SimpleBookDto dto) : IValidatableRequest<Result<SimpleBookDto>>
    {
    }
}