using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.Book_Authors
{
    public interface IBook_AuthorRepository : IRepositoryBase<Book_Author>
    {
    }
}
