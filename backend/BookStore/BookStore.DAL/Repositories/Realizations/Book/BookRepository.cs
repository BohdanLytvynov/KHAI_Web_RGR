using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces;
using BookStore.DAL.Repositories.Interfaces.Base;
using BookStore.DAL.Repositories.Interfaces.Books;
using BookStore.DAL.Repositories.Realizations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Realizations.Books
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context) : base(context) { }

        public BookRepository() : base() { }

        public async Task AddBook(Book book, IEnumerable<Author> authors = null, IEnumerable<Genre> genres = null)
        {
            //Case When book already exists
            var bookExists = await BookStoreDb.Books.FirstOrDefaultAsync(b => b.Equals(book));

            if (bookExists is not null)
                throw new Exception("The book Already Exists! If you want to change info about the book, Please use Edit Operation");

            if (authors != null)
            {
                foreach (var author in authors)
                {
                    var authorExists = await BookStoreDb.Authors
                        .FirstOrDefaultAsync(a => a.Surename.Equals(author.Surename)
                        && a.Name.Equals(author.Name)
                        && a.BirthDate.Equals(author.BirthDate));

                    if (authorExists is not null)// Author already exists
                    {
                        BookStoreDb.Entry(authorExists).State = EntityState.Unchanged;

                        book.Book_Authors.Add(new Book_Author() { Author = authorExists, Book = book });
                    }
                    else
                    {
                        book.Book_Authors.Add(new Book_Author() { Author = author, Book = book });
                    }
                }
            }

            if (genres != null)
            {
                foreach (var genre in genres)
                {
                    var genreExists = await BookStoreDb.Generes
                        .FirstOrDefaultAsync(g => g.Name.Equals(genre.Name));

                    if (genreExists is not null)
                    {
                        BookStoreDb.Entry(genreExists).State = EntityState.Unchanged;

                        book.Book_Genres.Add(new Book_Genre() { Genre = genreExists, Book = book });
                    }
                    else
                    {
                        book.Book_Genres.Add(new Book_Genre() { Genre = genre, Book = book });
                    }
                }
            }
            
            BookStoreDb.Books.Add(book);                 
        }
    }
}
