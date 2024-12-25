using BookStore.BLL.Services.AccessTokenCleaner.Interfaces;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.BLL.Services.AccessTokenCleaner.Realizations
{
    public class AccessTokenCleaner : ICleaner
    {
        public async Task Clean(User user, DateTime current)
        {
            await Task.Run(() =>
            {                
                user.AccessTokenIds.RemoveAll(x => x.ExpDate < current);
            });
        }
    }
}
