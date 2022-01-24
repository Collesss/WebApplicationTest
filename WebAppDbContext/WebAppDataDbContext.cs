using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppDbModels.Models;
using WebAppDbModels.Configurations;

namespace WebAppDbContext
{
    public class WebAppDataDbContext : DbContext
    {
        public DbSet<User> Users;

        public WebAppDataDbContext(DbContextOptions<WebAppDataDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
