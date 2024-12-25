using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Behaviors.Validation
{
    public interface IValidatableRequest<out TResponse> : IRequest<TResponse>, IValidatableRequest
    {
    }

    public interface IValidatableRequest
    {
    }
}
