using BookStore.DAL.Persistence;

namespace BookStore.DAL.Repositories.Interfaces
{
    public interface IBookStoreDbContextProvider
    {
        public BookStoreDbContext BookStoreDb { init; get; }
    }
}