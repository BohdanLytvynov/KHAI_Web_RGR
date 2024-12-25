using AutoMapper;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Authors.Update
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<bool>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;   

        public UpdateAuthorCommandHandler(IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _repositoryWrapper.AuthorRepository;

                repo.Edit(_mapper.Map<Author>(request.dto));

                if (await _repositoryWrapper.SaveChangesAsync() > 0)
                    return FluentResults.Result.Ok(true);
                throw new Exception("Fail to Execute UPDATE script");
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));               
            }
        }
    }
}
