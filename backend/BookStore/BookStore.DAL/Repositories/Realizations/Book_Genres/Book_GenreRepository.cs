using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.Book_Genres;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Book_Genres
{
    internal class Book_GenreRepository : RepositoryBase<Book_Genre>, IBook_GenersRepository
    {
        public Book_GenreRepository(BookStoreDbContext context) : base(context) { }
                            
        public Book_GenreRepository()
        {
            
        }
    }
}
