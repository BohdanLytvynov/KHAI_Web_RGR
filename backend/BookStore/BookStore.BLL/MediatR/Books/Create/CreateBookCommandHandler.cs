using AutoMapper;
using BookStore.BLL.Dto.Book;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Create
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<SimpleBookDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<SimpleBookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _repositoryWrapper.BookRepository;

                var book = _mapper.Map<Book>(request.dto);

                var genres = _mapper.Map<IEnumerable<Genre>>(request.dto.Geners);

                await repo.AddBook(book, null, genres);

                if (await _repositoryWrapper.SaveChangesAsync() > 0)
                {
                    var dto = _mapper.Map<SimpleBookDto>(book);

                    dto.Geners = request.dto.Geners;

                    return FluentResults.Result.Ok(dto);
                }
                else
                    throw new Exception("Fail to add new User!");
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));
            }
        }
    }
}
