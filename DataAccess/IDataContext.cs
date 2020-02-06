using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataContext
    {
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> EntitySet<TEntity>() where TEntity : EntityBase;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>Result</returns>
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        System.Data.Common.DbConnection GetDbConnection();
    }
}
