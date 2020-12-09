using System;
using System.Collections.Generic;
using System.Text;
using ChatterSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<User>(a => a.UserV)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserID);
        }

        public DbSet<Message> Messages { get; set; }
    }
}
