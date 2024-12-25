using BookStore.BLL.Dto.LogedOut;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Logout
{
    public record LogoutCommand : IRequest<Result<LogedOutDto>>
    {
    }
}
