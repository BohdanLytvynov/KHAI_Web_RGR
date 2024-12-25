using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Users
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("user_firstname");

            builder.Property(x => x.Surename)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("user_lastname");

            builder.Property(x =>x.Address)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("user_address");

            builder.Property(x =>x.BirthDate)
                .IsRequired()
                .HasColumnType("DATE")
                .HasColumnName("user_birth_date");
        }
    }
}
