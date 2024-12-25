using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Authors
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors", "bookStore" );

            builder.HasKey(x => x.Id);            

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("author_id");                

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("TEXT")
                .HasColumnName("author_name");

            builder.Property(x => x.Surename)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("TEXT")
                .HasColumnName("author_surename");

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("DATE")
                .HasColumnName("author_birth_date");            
        }
    }
}
