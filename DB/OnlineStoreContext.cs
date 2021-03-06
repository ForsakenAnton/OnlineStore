using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;

namespace OnlineStore.DB
{
    public class OnlineStoreContext: IdentityDbContext<User>
    {
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        /// /////////////////////////////////////////////////////////////
        public DbSet<Template> Templates { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        /// /////////////////////////////////////////////////////////////

        //public DbSet<BankCredit> BankCredits { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Category>().ToTable("Category");
           // builder.Entity<CategoryProduct>().ToTable("CategoryProduct"); // Ругается при создании!!!
            builder.Entity<Manufacturer>().ToTable("Manufacturer");
            builder.Entity<Comment>().ToTable("Comment");
            builder.Entity<Like>().ToTable("Like");
            builder.Entity<Answer>().ToTable("Answer");
            builder.Entity<FavoriteProduct>().ToTable("FavoriteProduct");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Delivery>().ToTable("Delivery");

            /// ////////////////////////////////////////////////////////
            builder.Entity<Template>().ToTable("Template");
            builder.Entity<Characteristic>().ToTable("Characteristic");
            /// /////////////////////////////////////////////////////////

            //builder.Entity<BankCredit>().ToTable("BankCredit");



            builder.Entity<Product>()
                .HasIndex(p => p.CommodityCode)
                .IsUnique();

            //builder
            //    .Entity<Template>()
            //    .HasMany(p => p.Categories)
            //    .WithMany(p => p.Templates);
                //.UsingEntity(j => j.ToTable("TemplateCategories"));

            //builder.Entity<Comment>()
            //    .Property(u => u.Rating)
            //    .HasDefaultValue<int>(1);

            //builder.Entity<Order>()
            //    .Property(o => o.OrderNumber)
            //    .ValueGeneratedOnAdd();

            //builder.Entity<Product>()
            //    .Property(p => p.Price).

            //builder.Entity<Product>()
            //    .HasOne<Manufacturer>(e => e.Manufacturer)
            //    .WithMany(d => d.Products)
            //    .HasForeignKey(e => e.ManufacturerId)
            //    .IsRequired(true)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
