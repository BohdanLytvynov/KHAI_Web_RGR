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

namespace BookStore.BLL.MediatR.Authors.Delete
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<DeleteAuthorDto>>
    {
        private readonly IMapper _mapper;

        private readonly IRepositoryWrapper _repository;

        public DeleteAuthorCommandHandler(IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<Result<DeleteAuthorDto>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _repository.AuthorRepository;

                var a = await repo.GetFirstOrDefaultAsync(x => x.Id == request.id);

                if (a == null)
                    throw new Exception("Can't find Author for remove!");

                repo.Delete(a);

                if (await _repository.SaveChangesAsync() > 0)
                    return FluentResults.Result.Ok(new DeleteAuthorDto() { Id = request.id });
                throw new Exception("Error when trying to Execute delete script for Author!");
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));                
            }
        }
    }
}
