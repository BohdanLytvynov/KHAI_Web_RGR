using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.Base;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.Authors
{
    public interface IAuthorRepository : IRepositoryBase<Author>, IBookStoreDbContextProvider
    {
    }
}
