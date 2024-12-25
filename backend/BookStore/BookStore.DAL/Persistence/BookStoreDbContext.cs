using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Persistence
{
    public class BookStoreDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        #region ctor
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region For Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=bookstorage;User Id=sa;Password=superAdmin;");

            optionsBuilder.EnableDetailedErrors();

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Db Sets

        public override DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Generes { get; set; }

        public DbSet<Book_Genre> Book_Geners { get; set; }

        public DbSet<Book_Author> Book_Author { get; set; }

        #endregion

        #region Functions



        protected override void OnModelCreating(ModelBuilder builder)
        {            
            builder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);            

            base.OnModelCreating(builder);
        }

        #endregion
    }
}
