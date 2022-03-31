using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissShopping.Core.Database.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductQuantity { get; set; }

    }

    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("BSWishlist");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.ProductId)
                .IsRequired(true);

        }
    }
}
