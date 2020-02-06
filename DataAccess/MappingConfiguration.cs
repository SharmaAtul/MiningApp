using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public abstract class MappingConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public abstract void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder);
    }
}
