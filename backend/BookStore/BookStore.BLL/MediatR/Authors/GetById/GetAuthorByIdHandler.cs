using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.GetById
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
    {
        private readonly IRepositoryWrapper _wrapper;

        private readonly IMapper _mapper;

        public GetAuthorByIdHandler(IRepositoryWrapper wrapper,
            IMapper mapper)
        {
            _mapper = mapper;

            _wrapper = wrapper;
        }

        public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {            
            try
            {
                var repo = _wrapper.AuthorRepository;

                var a = await repo.GetFirstOrDefaultAsync(x =>x.Id == request.id);

                if (a == null)
                    throw new Exception($"Unable to find element with Id: {request.id}");
                return FluentResults.Result.Ok(_mapper.Map<AuthorDto>(a));
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));                
            }
        }
    }
}
