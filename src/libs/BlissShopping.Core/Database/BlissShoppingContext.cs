using BlissShopping.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissShopping.Core.Database
{
    public class BlissShoppingContext : DbContext
    {
        public DbSet<Wishlist> Wishlist { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public BlissShoppingContext(DbContextOptions<BlissShoppingContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        //public BlissShoppingContext(DbContextOptions<BlissShoppingContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null) throw new ArgumentNullException(nameof(optionsBuilder));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }
    }
}
