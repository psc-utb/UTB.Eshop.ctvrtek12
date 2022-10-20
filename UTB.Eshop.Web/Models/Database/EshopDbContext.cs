using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Entities;

namespace UTB.Eshop.Web.Models.Database
{
    public class EshopDbContext : DbContext
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }

        public EshopDbContext([NotNull] DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            DatabaseInit databaseInit = new DatabaseInit();
            builder.Entity<CarouselItem>().HasData(databaseInit.CreateCarouselItems());
        }
    }
}
