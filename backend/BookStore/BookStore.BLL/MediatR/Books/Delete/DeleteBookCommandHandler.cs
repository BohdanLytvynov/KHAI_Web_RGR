using BookStore.BLL.Dto.Book;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Delete
{
    internal class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<DeleteBookDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteBookCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<DeleteBookDto>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _repositoryWrapper.BookRepository;

                var book = await repo.GetFirstOrDefaultAsync(x => x.Id == request.id);

                if (book == null) throw new Exception("Unable to find the book for deletion!");

                repo.Delete(book);

                if (await _repositoryWrapper.SaveChangesAsync() > 0)
                { 
                    return FluentResults.Result.Ok(new DeleteBookDto() { Id = request.id });
                }
                throw new Exception("Unabble to delete the book!");
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));
            }
        }
    }
}
