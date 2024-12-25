﻿using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.Book;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.CreateBookWithAuthor
{
    public record CreateBookWithAuthorCommand(CreateBookDto Dto) : IValidatableRequest<Result<bool>>
    {
        
    }
}