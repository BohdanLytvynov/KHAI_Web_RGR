using AutoMapper;
using BookStore.BLL.Dto.Book;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetAll
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<SimpleBookDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetAllBooksHandler(IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SimpleBookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookRepo = _repositoryWrapper.BookRepository;
                var books = await bookRepo.GetAllAsync();
                var books_geners = books.Include(x => x.Book_Genres).ThenInclude(x => x.Genre);

                return FluentResults.Result.Ok(_mapper.Map<IEnumerable<SimpleBookDto>>(books_geners));
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));               
            }
        }
    }
}
