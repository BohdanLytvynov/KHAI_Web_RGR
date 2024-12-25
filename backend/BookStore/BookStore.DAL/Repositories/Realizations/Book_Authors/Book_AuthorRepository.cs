using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.Book_Authors;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Book_Authors
{
    public class Book_AuthorRepository : RepositoryBase<Book_Author>, IBook_AuthorRepository
    {
        public Book_AuthorRepository(BookStoreDbContext context): base(context) { }

        public Book_AuthorRepository()
        {
                
        }
    }
}
