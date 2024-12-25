using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.Genres
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
    }
}
