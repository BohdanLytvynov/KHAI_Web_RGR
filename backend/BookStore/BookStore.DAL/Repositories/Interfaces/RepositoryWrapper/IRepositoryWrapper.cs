using BookStore.DAL.Repositories.Interfaces.Authors;
using BookStore.DAL.Repositories.Interfaces.Book_Authors;
using BookStore.DAL.Repositories.Interfaces.Book_Genres;
using BookStore.DAL.Repositories.Interfaces.Books;
using BookStore.DAL.Repositories.Interfaces.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        public IBookRepository BookRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public IGenreRepository GenreRepository { get; }

        public IBook_AuthorRepository Book_AuthorRepository { get; }

        public IBook_GenersRepository Book_GenersRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
