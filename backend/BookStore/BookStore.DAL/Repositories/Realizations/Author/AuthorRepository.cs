using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces;
using BookStore.DAL.Repositories.Interfaces.Authors;
using BookStore.DAL.Repositories.Interfaces.Base;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Authors
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreDbContext bookStoredbContext) : base(bookStoredbContext) { }

        public AuthorRepository()
        {
            
        }
    }
}
