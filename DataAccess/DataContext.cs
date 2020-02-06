using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace DataAccess
{
    public class DataContext : DbContext,IDataContext
    {
        protected DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public DbSet<TEntity> EntitySet<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

        public DbConnection GetDbConnection()
        {
            return null;
        }
    }
}
