using BookStore.BLL.Dto.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.Book
{
    public class SimpleBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PubYear { get; set; }

        public List<GenreDto> Geners { get; set; } = new();
    }
}
