using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Author : IEquatable<Author>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surename { get; set; }

        public DateOnly BirthDate { get; set; }

        public List<Book_Author>? Book_Authors { get; set; } = new();

        public bool Equals(Author? other)
        {
            return Name.Equals(other.Name) && 
                Surename.Equals(other.Surename) && 
                BirthDate.Equals(other.BirthDate);
        }
    }
}
