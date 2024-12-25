using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.AccessTokenIds
{
    internal class AccessTokenIdConfiguration : IEntityTypeConfiguration<AccessTokenId>
    {
        public void Configure(EntityTypeBuilder<AccessTokenId> builder)
        {
            builder.HasKey(x => new { x.UserId, x.AccessTokenGUID });
            
            builder.Property(x => x.AccessTokenGUID)
                .IsRequired().HasColumnType("UUID");

            builder.HasOne(x => x.User)
                .WithMany(x => x.AccessTokenIds)
                .HasForeignKey(fk => fk.UserId);

            builder.Property(x => x.ExpDate)
                .IsRequired().HasColumnName("access_token_expiration");                
        }
    }
}
