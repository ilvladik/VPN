﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VPN.Domain.Entities;

namespace VPN.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Key> Keys { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
