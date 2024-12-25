using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;

namespace BookStore.BLL.MediatR.Authors.Create
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public CreateAuthorHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repositoryWrapper;
        }

        public async Task<Result<AuthorDto>> Handle(CreateAuthorCommand request, 
            CancellationToken cancellationToken)
        {            
            try
            {
                var repo = _repository.AuthorRepository;

                repo.Create(_mapper.Map<Author>(request.dto));
                
                if(await _repository.SaveChangesAsync() > 0)
                    return FluentResults.Result.Ok(request.dto);
                throw new Exception("Error when executing INSERT - script for Author.");

            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));            
            }
            
        }
    }
}
