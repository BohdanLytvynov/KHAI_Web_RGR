using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces;
using BookStore.DAL.Repositories.Interfaces.Authors;
using BookStore.DAL.Repositories.Interfaces.Book_Authors;
using BookStore.DAL.Repositories.Interfaces.Book_Genres;
using BookStore.DAL.Repositories.Interfaces.Books;
using BookStore.DAL.Repositories.Interfaces.Genres;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.Authors;
using BookStore.DAL.Repositories.Realizations.Book_Authors;
using BookStore.DAL.Repositories.Realizations.Book_Genres;
using BookStore.DAL.Repositories.Realizations.Books;
using BookStore.DAL.Repositories.Realizations.Genres;

namespace BookStore.DAL.Repositories.Realizations.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IBookRepository _bookRepository;
        
        private IAuthorRepository _authorRepository;

        private IGenreRepository _genreRepository;

        private IBook_GenersRepository _book_GenersRepository;

        private IBook_AuthorRepository _book_AuthorRepository;

        public IBookRepository BookRepository => GetRepository(_bookRepository as BookRepository);
        
        public IAuthorRepository AuthorRepository { get=> GetRepository(_authorRepository as AuthorRepository); }

        public IGenreRepository GenreRepository { get => GetRepository(_genreRepository as GenreRepository); }

        public IBook_GenersRepository Book_GenersRepository { get => GetRepository(_book_GenersRepository as Book_GenreRepository); }

        public IBook_AuthorRepository Book_AuthorRepository { get=> GetRepository(_book_AuthorRepository as Book_AuthorRepository); }

        private readonly BookStoreDbContext _db;

        public RepositoryWrapper(BookStoreDbContext db)
        {
            _db = db;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public T GetRepository<T>(T? repo)
     where T : IBookStoreDbContextProvider, new()
        {
            if (repo is null)
            {
                repo = new T()
                {
                    BookStoreDb = _db
                };
            }

            return repo;
        }
    }
}
