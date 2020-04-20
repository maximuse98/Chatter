using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTest.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .Property<int>("ThemeId");

            modelBuilder.Entity<Message>()
                .HasOne(s => s.Theme)
                .WithMany(e => e.Messages)
                //.HasForeignKey("FK_Theme")
                .IsRequired();
        }
    }
}
