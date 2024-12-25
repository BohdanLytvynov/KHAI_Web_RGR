using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.Genres;
using BookStore.DAL.Repositories.Realizations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Genres
{
    public class GenreRepository : RepositoryBase<Genre> , IGenreRepository
    {
        public GenreRepository(BookStoreDbContext context) : base(context) { }

        public GenreRepository()
        {

        }
    }
}
