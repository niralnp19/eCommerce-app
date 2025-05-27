using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ad> Ad { get; set; }
        public DbSet<AdImage> AdImage { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ad>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<AdImage>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<UserProfile>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Follower>().HasKey(x => new {x.UserId , x.SellerUserId });
            modelBuilder.Entity<Review>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Review>().Property(x => x.Text).HasMaxLength(500);
        }
    }
}
