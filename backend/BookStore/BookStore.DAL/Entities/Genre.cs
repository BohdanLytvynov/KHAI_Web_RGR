using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Book_Genre>? Book_Geners { get; set; } = new();       
    }
}
