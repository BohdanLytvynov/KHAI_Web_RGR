using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Books
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", "bookStore");

            builder.HasKey(x => x.Id);                

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("book_id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("title");   
            
            builder.Property(x => x.PubYear)
                .IsRequired()
                .HasColumnType("SMALLINT")
                .HasColumnName("publication_year");                 
        }
    }
}
