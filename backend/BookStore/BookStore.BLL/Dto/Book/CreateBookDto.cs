using BookStore.BLL.Dto.Author;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.Book
{
    public class CreateBookDto
    {        
        public string Name { get; set; }

        public int PubYear { get; set; }
        
        public IEnumerable<CreateAuthorDto> Authors { get; set; }              

        public IEnumerable<CreateGenreDto> Genres { get; set; }       
    }
}
