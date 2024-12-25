using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.Books
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        public Task AddBook(Book book, IEnumerable<Author> authors, IEnumerable<Genre> genres);        
    }
}
