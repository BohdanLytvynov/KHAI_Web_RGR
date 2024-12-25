using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Book_Author
    {
        public int? BookId { get; set; }

        public Book? Book { get; set; }

        public int? AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}
