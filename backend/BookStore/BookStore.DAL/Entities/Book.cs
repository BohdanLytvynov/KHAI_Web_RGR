using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Book : IEquatable<Book>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PubYear { get; set; }

        #region Author Navigation Properties

        public List<Book_Author> Book_Authors { get; set; } = new();

        #endregion

        #region Genre Navigation Properties

        public List<Book_Genre>? Book_Genres { get; set; } = new();

        public bool Equals(Book? other)
        {
            return Name.Equals(other.Name) &&
                PubYear.Equals(other.PubYear);
        }

        #endregion
    }
}
