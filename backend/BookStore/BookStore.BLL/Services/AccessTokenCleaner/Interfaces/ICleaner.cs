using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Services.AccessTokenCleaner.Interfaces
{
    public interface ICleaner
    {
        Task Clean(User u, DateTime current);
    }
}
