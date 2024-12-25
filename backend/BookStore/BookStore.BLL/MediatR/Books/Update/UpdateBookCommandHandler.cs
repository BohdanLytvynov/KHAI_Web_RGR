using AutoMapper;
using BookStore.BLL.Dto.Book;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;

namespace BookStore.BLL.MediatR.Books.Update
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<SimpleBookDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<SimpleBookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _repositoryWrapper.BookRepository;
                repo.Edit(_mapper.Map<Book>(request.dto));

                if (await _repositoryWrapper.SaveChangesAsync() > 0)
                {
                    return FluentResults.Result.Ok(request.dto);
                }
                throw new Exception("Fail to remove the Book!");
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));
            }
        }
    }
}
