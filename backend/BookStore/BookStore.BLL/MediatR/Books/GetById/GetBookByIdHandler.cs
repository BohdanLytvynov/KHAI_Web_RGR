using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.BLL.Dto.Book;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetById
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetBookByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {                
                var Books_Author = await _repositoryWrapper.BookRepository
                   .GetQueryableSet<Book_Author>(null, include => include.Include(x => x.Author)
                   .Include(x => x.Book));

                var Books_Gener = await _repositoryWrapper.BookRepository
                   .GetQueryableSet<Book_Genre>(null, include => include.Include(x => x.Book)
                   .Include(x => x.Genre));

                var Authors = await _repositoryWrapper.AuthorRepository.GetAllAsync();
                var Geners = await _repositoryWrapper.GenreRepository.GetAllAsync();

                var temp = await Books_Author.Join(Books_Gener, x => x.BookId, y => y.BookId,
                    (x, y) => new BookDto()
                    {
                        Name = x.Book.Name,
                        Id = x.Book.Id,
                        PubYear = x.Book.PubYear,
                        Authors = _mapper.Map<List<AuthorDto>>(
                            Authors.Where(a => a.Id == x.AuthorId)),
                        Genres = _mapper.Map<List<GenreDto>>(
                            Geners.Where(g => g.Id == y.GenreId)
                            )
                    }
                    ).FirstOrDefaultAsync(f => f.Id == request.id);

                return FluentResults.Result.Ok<BookDto>(temp);
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));
            }
        }
    }
}
