using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Dto.UserDto;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Login
{
    public record LoginUserQuery(LoginUserDto Dto) : IValidatableRequest<Result<AuthResponseDto>>
    {

    }
}
