using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.BLL.Dto.Book;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.RepositoryWrapper;
using FluentResults;
using MediatR;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetAllBooksWithAuthors
{
    public class GetAllBooksWithAuthorsQueryHandler : IRequestHandler<GetAllBooksWithAuthorsQuery, Result<IEnumerable<BookDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IMapper _mapper;

        public GetAllBooksWithAuthorsQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repositoryWrapper = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BookDto>>> Handle(GetAllBooksWithAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {                                              
                var Books = (await _repositoryWrapper.BookRepository.GetAllAsync())
                .Include(b => b.Book_Authors).ThenInclude(ba => ba.Author)
                .Include(b => b.Book_Genres).ThenInclude(bg => bg.Genre);
                
                return FluentResults.Result.Ok(_mapper.Map<IEnumerable<BookDto>>(Books));
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(new Error(e.Message));                
            }
        }
    }
}


