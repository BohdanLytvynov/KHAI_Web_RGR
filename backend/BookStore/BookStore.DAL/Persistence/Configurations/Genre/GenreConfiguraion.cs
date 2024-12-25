using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Genres
{
    internal class GenreConfiguraion : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres", "bookStore");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("genre_id");
                
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("genre");            
        }
    }
}
