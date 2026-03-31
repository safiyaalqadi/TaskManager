using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.application.Interfaces;
using System.Threading;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace TaskManager.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<UserItem>, IDBContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserItem> Users { get; set; }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<TaskItem>().HasKey(c => c.Id);
            modelBuilder.Entity<TaskItem>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<UserItem>().HasKey(u => u.Id);
            modelBuilder.Entity<UserItem>().Property(u => u.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskItem>()
               .HasOne(t => t.user)
               .WithMany(u => u.Tasks)
               .HasForeignKey(t => t.userId);
        }
    }
}
